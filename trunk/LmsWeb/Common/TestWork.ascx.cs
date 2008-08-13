namespace DCE.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	/// Прохождение теста
	/// </summary>
	public partial  class TestWork : DCE.BaseTrainingControl
	{
		public string AbsoluteViewPath;
		protected System.Xml.Xsl.XslTransform trans = new System.Xml.Xsl.XslTransform();

		void PreLoadApply()
		{
			Guid? trId = DCE.Service.TrainingID;
			Guid? studentId = CurrentUser.UserID;
			
			DCE.Service.LoadXmlDoc(this.Page, doc, "TestWork.xml");
			doc.DocumentElement.InnerXml += "<isPractice>false</isPractice>";
			
			trans.Load(this.Page.MapPath(@"~/xsl/TestStart.xslt"));

			Guid? testId = PageParameters.ID;
			if(trId.HasValue && testId.HasValue && studentId.HasValue) {
				bool _isCourseTest = false;
				
				DataSet dsCommon = Test_SelectResults(studentId, testId);
				DataTable tableCommon = dsCommon.Tables["Common"];
				
				if (tableCommon != null && tableCommon.Rows.Count > 0) {
					_isCourseTest = Convert.IsDBNull(tableCommon.Rows[0]["Theme"]);
				}
				
				doc.DocumentElement.InnerXml += dsCommon.GetXml();
				
				DataSet dsTest = Test_Select(trId, testId);
				DataTable tableTest = dsTest.Tables["Tests"];
				
				if (tableTest != null && tableTest.Rows.Count == 1) {
					DataRow testRow = tableTest.Rows[0];
					Guid? _testId = (Guid?)testRow["id"];
					string select1 = @"
						SELECT tr.*, dbo.TestResultPoints(tr.id) as Points,  
                  (select max(AnswerTime) from testAnswers where TestResults=tr.id) as TestDuration
                  FROM  dbo.TestResults tr where tr.Test = '"
                  +_testId+"' and tr.Student='"+studentId+"'";

					DataSet dsTestRes = dbData.Instance.getDataSet(select1, "dsRes", "TestResult");
					DataTable tableTestRes = dsTestRes.Tables["TestResult"];
					DataRow testResRow = null;
					
					if (tableTestRes.Rows.Count == 0) {
						testResRow = tableTestRes.NewRow();
						testResRow["Tries"] = 0;
						testResRow["AllowTries"] = _isCourseTest ? 1 : 2;
						
						testResRow["Student"] = studentId.Value;
						testResRow["Test"] = tableTest.Rows[0]["id"];
						testResRow["Complete"] = false;
						testResRow["Skipped"] = false;
						tableTestRes.Rows.Add(testResRow);
					} else {
						testResRow = tableTestRes.Rows[0];
					}
					
					if ((bool)testResRow["Complete"]) {
						trans.Load(this.Page.MapPath("~/xsl/TestWork.xslt"));
						doc.DocumentElement.InnerXml += "<testEnd>Complete</testEnd>";
					}
					
					int allowTries = (int)testResRow["AllowTries"];
					
					if(this.Request.Form["SkipButton"] != null) {
						testResRow["Skipped"] = true;
						dbData.Instance.UpdateDataSet(select1, "TestResult", ref dsTestRes);
					}
					
					if (allowTries >= 0) {// Остались попытки
						if (this.Request.Form["StartButton"] != null && allowTries > 0) {
							this.Session["testId"] = testId;
							testResRow["TryStart"] = DateTime.Now;
							testResRow["AllowTries"] = --allowTries;
							testResRow["Tries"] = (int)testResRow["Tries"] + 1;
							testResRow["Complete"] = false;
							testResRow["Points"] = 0;
							testResRow["CompletionDate"] = DBNull.Value;
							
							string restResid = testResRow["id"].ToString();
							
							if (!string.IsNullOrEmpty(restResid)) {
								dbData.Instance.ExecSQL("delete from dbo.TestAnswers where TestResults='"
								   + restResid + "'");
							}

							dbData.Instance.UpdateDataSet(select1, "TestResult", ref dsTestRes);
							testResRow = dsTestRes.Tables["TestResult"].Rows[0];
							// Начало теста
							this.qwWork(testId, studentId, testRow, ref testResRow, false);
							dbData.Instance.UpdateDataSet(select1, "TestResult", ref dsTestRes);
						}
						
						if (this.Request.Form["ContinueButton"] != null) { // Принять ответ
							this.qwAccept(testRow, ref testResRow, false);
							dbData.Instance.UpdateDataSet(select1, "TestResult", ref dsTestRes);
							testResRow = dsTestRes.Tables["TestResult"].Rows[0];
							// Следующий вопрос
							this.qwWork(testId, studentId, testRow, ref testResRow, false);
							dbData.Instance.UpdateDataSet(select1, "TestResult", ref dsTestRes);
						}
						
						if(this.AfterLanguageSwitched()) {
							// Переключение языка...
							int testDuration = (int) testRow["Duration"]*60; //sec
							DateTime tryStart = (DateTime) testResRow["TryStart"];
							TimeSpan timeElapse = DateTime.Now.Subtract(tryStart);
							int timeLeft = testDuration - (int)timeElapse.TotalSeconds;
							
							if (timeLeft > 0) { // Перевыбор вопроса
								this.qwWork(testId, studentId, testRow, ref testResRow, false);
							}
						}
					}
					
					if (allowTries == 0) { // Не доступна кнопка приступить
						System.Xml.XmlNode disabled = doc.CreateNode(System.Xml.XmlNodeType.Element, "disabled", string.Empty);
						disabled.InnerText = "true";
						doc.DocumentElement.AppendChild(disabled);
					}
					
					if (!(bool)testRow["Mandatory"] // Обязательность темы (не обязательна)
							&& !(bool)testResRow["Skipped"] // Не пропущена тренером или студентом
							&& !(bool)testResRow["Complete"]) {// Не сдана
						// Показывать кнопку Пропустить
						System.Xml.XmlNode skip = doc.CreateNode(System.Xml.XmlNodeType.Element, "skip", string.Empty);
						skip.InnerText = "true";
						doc.DocumentElement.AppendChild(skip);
					}
					
					doc.DocumentElement.InnerXml 
                  += "<hType>"+((DCEAccessLib.HintType)testRow["Hints"]).ToString()+"</hType>";
					doc.DocumentElement.InnerXml += dsTest.GetXml();
					doc.DocumentElement.InnerXml += dsTestRes.GetXml();
				}
				
				if (_isCourseTest) {
					doc.DocumentElement.InnerXml += "<isCourseTest>true</isCourseTest>";
				}
				
				DataSet dsTPoints = Test_SelectThemes(studentId, testId);
				doc.DocumentElement.InnerXml += dsTPoints.GetXml();
			}
		}

		static DataSet Test_SelectThemes(Guid? studentId, Guid? testId)
		{
			string select2 = @"
                  SELECT t.id as themeId, dbo.GetStrContentAlt(t.Name,  '" + LocalisationService.Language + "', '" + LocalisationService.DefaultLanguage + @"') as Theme,
                     (select ISNULL(SUM(q.Points),0) from dbo.TestQuestions q where q.Theme = t.id) as ThemeMaxPoints,
                     (select ISNULL(SUM(a.Points),0) from dbo.TestAnswers a, dbo.TestQuestions q, dbo.TestResults tr
                           where a.Question = q.id and q.Theme = t.id and tr.id=a.TestResults and tr.Student='" + studentId + @"') as ThemePoints
                  FROM Themes t 
                  where t.id in (select q.Theme from dbo.TestQuestions q where q.Test = '" + testId + @"')";
			DataSet dsTPoints = dbData.Instance.getDataSet(select2, "dsTPoints", "TPoints");
			return dsTPoints;
		}

		static DataSet Test_Select(Guid? trId, Guid? testId)
		{
			string select0 = @"SELECT *, dbo.TestPoints(id) as MaxPoints, 
                     dbo.IsTestMandatory(id, '" + trId + @"') as Mandatory 
                  FROM  dbo.Tests where id = '" + testId + "'";
			DataSet dsTest = dbData.Instance.getDataSet(select0, "dsTest", "Tests");
			return dsTest;
		}

		DataSet Test_SelectResults(Guid? studentId, Guid? testId)
		{
			string croot = Settings.getValue("Courses/root");
			if (croot[croot.Length - 1] != '/')
				croot += "/";

			string select = string.Format(@"
SELECT '{0}' AS cRoot,
		'{1}' as PublicRoot, 
		dbo.GetStrContentAlt(t.Name,  '{2}', '{3}') as Theme,
		dbo.GetStrContentAlt(c.Name,  '{2}', '{3}') as Course,
		c.DiskFolder,
		c.FinishQuestionnaire as qId,
		qtr.id as qRes
FROM	TestResults qtr
	RIGHT OUTER JOIN Themes t
	RIGHT OUTER JOIN Courses c
	INNER JOIN Tests ts
		ON c.id = dbo.GetTestCourse(ts.id)
		ON ts.Parent = t.id
		on qtr.Test = c.FinishQuestionnaire
			and qtr.Student = '{4}'
WHERE ts.id = '{5}'",
					this.CoursesRootUrl,
					croot,
					LocalisationService.Language,
					LocalisationService.DefaultLanguage,
					studentId,
					testId);
			
			DataSet dsCommon = dbData.Instance.getDataSet(select, "dsCommon", "Common");
			return dsCommon;
		}
		
		bool AfterLanguageSwitched() {
			bool _result = this.IsRedirectAfterLanguageSwitch;
			return _result;
		}

		const string SESSION_LANG_SWITCHED = "_languageSwitched";
		/// <summary>
		/// Holds a boolean flag indicating that a redirect has occured
		/// after a post-back from the language switching menu.
		/// </summary>
		bool IsRedirectAfterLanguageSwitch {
			get {
				object _value = this.Session[SESSION_LANG_SWITCHED];
				return null != _value ? (bool)_value : false;
			}
			set {
				this.Session[SESSION_LANG_SWITCHED] = value;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			bool _result = this.Request.Form["__EVENTTARGET"] == "MainMenuControl1$mnLang";
			
			//Ignore post-back caused by a language-switch menu
			if (_result) {
				this.IsRedirectAfterLanguageSwitch = true;
			} else {
				this.PreLoadApply();
				this.IsRedirectAfterLanguageSwitch = false;
			}
			
         this.Xml1.Document = doc;
         this.Xml1.TransformArgumentList = new System.Xml.Xsl.XsltArgumentList();
         this.Xml1.TransformArgumentList.AddParam("LangPath", string.Empty, 
            DCE.Service.GetLanguagePath(this.Page));
         this.Xml1.Transform = trans;
      }

      /// <summary>
      /// Проверка правильности ответа
      /// </summary>
      /// <param name="testRow"></param>
      /// <param name="testResRow"></param>
      /// <param name="practice">Практическая работа?</param>
		protected void qwAccept(DataRow testRow, ref DataRow testResRow, bool practice)
		{
			int testDuration = (int)testRow["Duration"]*60; //sec
			DateTime tryStart = (DateTime) testResRow["TryStart"];
			TimeSpan timeElapse = DateTime.Now.Subtract(tryStart);
			int timeLeft = testDuration - (int)timeElapse.TotalSeconds;
			
			if (!practice && timeLeft <= 0) { // Время теста истекло
				return;
			}
			
			Guid? qId = GuidService.Parse(this.Request.Form["qwId"]);
			
			if(qId.HasValue) {
				DataSet dsQw = Test_SelectQuestions(qId);
				DataTable tableQw = dsQw.Tables["Qw"];
				
				if (tableQw != null && tableQw.Rows.Count > 0) {
					Guid qwId = (Guid)tableQw.Rows[0]["id"];
					DCEAccessLib.ContentType qType = (DCEAccessLib.ContentType) tableQw.Rows[0]["Type"];
					System.Xml.XmlDocument adoc = new System.Xml.XmlDocument();
					adoc.LoadXml(tableQw.Rows[0]["Answer"].ToString());
					System.Xml.XmlDocument defDoc = new System.Xml.XmlDocument();
					defDoc.LoadXml(tableQw.Rows[0]["defAnswer"].ToString());
					// тип вопроса
					System.Xml.XmlAttribute aType = adoc.DocumentElement.Attributes["type"];
					
					string select0 = string.Format(@"
select *
from dbo.TestAnswers
where TestResults='{0}'
	and Question='{1}'", testResRow["id"], qwId);
					
					DataSet dsAw = dbData.Instance.getDataSet(select0, "dsAw", "Answers");
					DataTable tableAw = dsAw.Tables["Answers"];
					
					if (tableAw != null) {
						DataRow rowAw = null;
						if (tableAw.Rows.Count == 0) {
							rowAw = tableAw.NewRow();
							rowAw["Question"] = qwId;
							rowAw["TestResults"] = testResRow["id"];
							tableAw.Rows.Add(rowAw);
							
							string answer = string.Empty;
							
							bool res = false;
							int persents = 0;
							
							if (qType == DCEAccessLib.ContentType._object) {
								answer = this.Request.Form["objRes"];
								
								if (!string.IsNullOrEmpty(answer)) {
									persents = System.Convert.ToInt32(answer, 10);
									res = persents > 0;
								}
							} else if(aType != null) {
								bool empty = true;
								
								// Если нет текста ответов - выбираем язык курса по-умолчанию
								foreach(System.Xml.XmlNode item in adoc.DocumentElement.SelectNodes("Answer")) {
									if (!string.IsNullOrEmpty(item.InnerText)) {
										empty = false;
									}
								}
								
								if (empty) {
									adoc = defDoc;
								}
								
								switch (aType.InnerText) {
									case "single": // Одиночный выбор
										answer = this.Request.Form["single"];
										
										for (int i=0; i < adoc.DocumentElement.ChildNodes.Count; i++) {
											if ((i+1).ToString() == answer) { // Правильный ответ
												System.Xml.XmlAttribute selected = adoc.CreateAttribute("selected");
												selected.Value = "true";
												adoc.DocumentElement.ChildNodes[i].Attributes.Append(selected);
												res = adoc.DocumentElement.ChildNodes[i].Attributes["right"].Value == "true";
												break;
											}
										}
										break;
									
									case "multiple": // Множественный выбор
										res = true;
										
										for (int i=0; i < adoc.DocumentElement.ChildNodes.Count; i++) {
											bool check = this.Request.Form["multiple" + (i+1).ToString()] != null;
										
											if (check) {
												System.Xml.XmlAttribute selected = adoc.CreateAttribute("selected");
												selected.Value = "true";
												adoc.DocumentElement.ChildNodes[i].Attributes.Append(selected);
											}
											
											res = res && (adoc.DocumentElement.ChildNodes[i].Attributes["right"].Value == "true") == check;
										}
										break;
									case "textbox": // Поле для ввода
										answer = this.Request.Form["textbox"];
										answer = answer.Trim();
										System.Xml.XmlNode t = adoc.DocumentElement.SelectSingleNode("Answer");
										
										if (t != null) {
											System.Xml.XmlNode result = adoc.CreateNode(System.Xml.XmlNodeType.Element, "result", string.Empty);
											result.InnerText = answer;
											string cs = t.Attributes["casesensitive"].Value;
											
											if (cs == "true") { // С учетом регистра
												res = t.InnerText == answer;
											} else {
												res = t.InnerText.ToLower() == answer.ToLower();
											}
											
											t.AppendChild(result);
										}
										break;
								}
							}
							
							System.Xml.XmlAttribute resTrue = adoc.CreateAttribute("result");
							resTrue.Value = "true";
							System.Xml.XmlAttribute resFalse = adoc.CreateAttribute("result");
							resFalse.Value = "false";
							if (res) { // Правильное решение
								rowAw["Points"] =  persents > 0 
										? persents * (int)tableQw.Rows[0]["Points"] / 100
										: tableQw.Rows[0]["Points"];
								
								adoc.DocumentElement.Attributes.Append(resTrue);
							} else {
								adoc.DocumentElement.Attributes.Append(resFalse);
							}
							
							rowAw["Answer"] = adoc.InnerXml;
							rowAw["AnswerTime"] = timeElapse.Seconds;
							dbData.Instance.UpdateDataSet(select0, "Answers", ref dsAw);
						} 
					}
				}
			}
		}

		static DataSet Test_SelectQuestions(Guid? qId)
		{
			string select = string.Format(@"
select	id,
		Type,
		Points,
		dbo.GetStrContentAlt(ShortHint,'{0}','{1}') as ShortHint, 
		dbo.GetStrContentAlt(LongHint,'{0}','{1}') as LongHint, 
		dbo.GetTDataContentAlt(Answer,'{0}','{1}') as Answer,
		dbo.GetTDataContentAlt(Answer,'{0}','{1}') as defAnswer
from	dbo.TestQuestions
where id='{2}'",
					LocalisationService.Language,
					LocalisationService.DefaultLanguage,
					qId);

			DataSet dsQw = dbData.Instance.getDataSet(select, "dsQw", "Qw");
			return dsQw;
		}

      /// <summary>
      /// Генерация следующего вопроса
      /// </summary>
      /// <param name="testId"></param>
      /// <param name="studentId"></param>
      /// <param name="testRow"></param>
      /// <param name="testResRow"></param>
      /// <param name="practice">Практичекая работа</param>
		protected bool qwWork(
				Guid? testId,
				Guid? studentId,
				DataRow testRow,
				ref DataRow testResRow,
				bool practice) {
			trans.Load(this.Page.MapPath(@"~/xsl/TestWork.xslt"));
			
			int testDuration = (int) testRow["Duration"]*60; //sec
			DateTime tryStart = (DateTime) testResRow["TryStart"];
			TimeSpan timeElapse = DateTime.Now.Subtract(tryStart);
			int timeLeft = testDuration - (int)timeElapse.TotalSeconds;
			
			doc.DocumentElement.InnerXml += "<iTimeLeft>"+timeLeft+"</iTimeLeft>";
			doc.DocumentElement.InnerXml += "<iTimeElapse>"+
				(new TimeSpan(timeElapse.Hours, timeElapse.Minutes, timeElapse.Seconds)).ToString()+"</iTimeElapse>";

			// Выбираем все неотвеченные вопросы
			string select = "SELECT * FROM  dbo.TestQuestions q where Test = '"
				+testId+@"' and q.id not in (select Question from dbo.TestAnswers 
                  where TestResults in 
                  (select id from TestResults where Student='"+studentId
            +"' and Test='"+testId+"')) order by q.QOrder";

			DataSet dsQw = dbData.Instance.getDataSet(select, "dsQw", "Qw");
			DataTable tableQw = dsQw.Tables["Qw"];
			if (	tableQw != null
					&& tableQw.Rows.Count > 0
					&& (practice || timeLeft > 0)
					&& !((int)testRow["Points"] <= (int)testResRow["Points"]
						&& (bool)testRow["AutoFinish"])) { // автофиниш
				// Выбираем случайный вопрос из множества если не практика
				int tryQw = 0;
				
				if (!practice) {
					Random rnd = new Random(DateTime.Now.Millisecond);
					tryQw = rnd.Next(tableQw.Rows.Count-1);
				}
				
				object tryQwo = this.Session["LastQw"];
				if (this.AfterLanguageSwitched() && tryQwo != null) {
					tryQw = (int) tryQwo;
				}
				
				this.Session.Add("LastQw", tryQw);
				DataRow row = tableQw.Rows[tryQw];
				
				select = string.Format(@"
SELECT	q.id,
		dbo.GetContentAlt(q.Content, '{0}','{1}') as Content,
		q.Type as cType,
		dbo.GetContentAlt(q.ShortHint, '{0}','{1}') as ShortHint,
		dbo.GetContentAlt(q.LongHint, '{0}','{1}') as LongHint,
		dbo.GetContentAlt(q.Answer, '{0}','{1}') as Answer,
		{2} as Remain,
		dbo.GetContentAlt(q.Answer, '{1}', '{0}') as AnswerDef,
		q.Points from dbo.TestQuestions q
WHERE	q.id = '{3}'",
						LocalisationService.Language,
						LocalisationService.DefaultLanguage,
						tableQw.Rows.Count,
						row["id"]);
				dsQw = dbData.Instance.getDataSet(select, "dsQw", "Qw");
				tableQw = dsQw.Tables["Qw"];
				
				if (tableQw != null && tableQw.Rows.Count == 1) {
					row = tableQw.Rows[0];
					DCEAccessLib.ContentType t = (DCEAccessLib.ContentType) row["cType"];
					doc.DocumentElement.InnerXml += "<cType>"+t.ToString()+"</cType>";

					string dsStr = dsQw.GetXml();
					dsStr = dsStr.Replace("&lt;", "<");
					dsStr = dsStr.Replace("&gt;", ">");
					doc.DocumentElement.InnerXml += dsStr;
					//doc.DocumentElement.InnerXml += "<cRoot>"+CoursesRoot+"</cRoot>";
				}
			} else {
				string testEnd = string.Empty;
				
				if ((int)testRow["Points"] <= (int)testResRow["Points"]) {
					testResRow["Complete"] = true;
					testResRow["CompletionDate"] = System.DateTime.Now;
					testEnd = "<testEnd>Complete</testEnd>";
				} else {
					// Тест закончен
					testEnd = "<testEnd>Failure</testEnd>";
				}
				
				if (practice) {
					testResRow["Complete"] = true;
					testEnd = "<testEnd>PracticeEnd</testEnd>";
				} else if (timeLeft <= 0) {
					// Время теста истекло
					doc.DocumentElement.InnerXml += "<timeOut>true</timeOut>";
				}
				
				doc.DocumentElement.InnerXml += testEnd;
				return true;
			}
			return false;
		}
	}
}
