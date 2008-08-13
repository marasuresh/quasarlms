namespace DCE.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Xml;
	using System.Xml.Xsl;

	/// <summary>
	/// Анкетирование
	/// </summary>
	public partial  class Questionnaire : DCE.BaseWebControl
	{
		Guid? m_qId;
		public Guid? qId {
			get {
				if (!this.m_qId.HasValue) {
					this.m_qId = GuidService.Parse(this.Request["qId"]);
				}
				return this.m_qId;
			}
		}
		
		protected XslTransform trans = new XslTransform();

		protected void Page_Load(object sender, EventArgs e)
		{
			this.Xml1.Document = doc;
			this.Xml1.TransformArgumentList = new XsltArgumentList();
			trans.Load(this.Page.MapPath(@"~/xsl/Questionnaire.xslt"));
			this.Xml1.Transform = trans;
			
			DCE.Service.LoadXmlDoc(this.Page, doc, "TestWork.xml");

			Guid? trId = PageParameters.trId;
			bool sessionIds = false;
			Guid? studentId = CurrentUser.UserID;
			
			if (!studentId.HasValue) {
				studentId = GuidService.Parse(this.Request["studentId"]);
				sessionIds = false;
				DCE.Service.SetTitle("Статистика обучения", this.Page);
			}
			
			if (sessionIds) {
				this.doc.DocumentElement.InnerXml += "<Session>true</Session>";
			}
			
			DataSet dsQ = null;
			DataTable tableQ = null;
			
			if(!qId.HasValue) {
				DataSet dsCommon = Test_SelectCommon();
				DataTable tableCommon = dsCommon.Tables["Common"];
				doc.DocumentElement.InnerXml += dsCommon.GetXml();
				
				if (tableCommon != null && tableCommon.Rows.Count > 0) {
					this.m_qId = (Guid?)tableCommon.Rows[0]["id"];
					dsQ = Test_Select(this.qId);
					tableQ = dsQ.Tables["Tests"];
					doc.DocumentElement.InnerXml += dsQ.GetXml();
					studentId = Guid.Empty;
				}
			} else {
				DataSet dsCommon = Test_SelectByStudent(studentId, this.qId);
				DataTable tableCommon = dsCommon.Tables["Common"];
				doc.DocumentElement.InnerXml += dsCommon.GetXml();
				dsQ = Test_Select(this.qId);
				tableQ = dsQ.Tables["Tests"];
				doc.DocumentElement.InnerXml += dsQ.GetXml();
			}

			if (tableQ != null && tableQ.Rows.Count == 1) {
				DataRow qRow = tableQ.Rows[0];
				string trSelect = string.Format(@"
SELECT	*,
		dbo.TestResultPoints(id) as Points
FROM	dbo.TestResults where Test = '{0}'
		and Student='{1}'",
						qRow["id"],
						studentId);

				DataSet dsTestRes = dbData.Instance.getDataSet(trSelect, "dsRes", "TestResult");
				DataTable tableTestRes = dsTestRes.Tables["TestResult"];
				DataRow testResRow = null;

				if (tableTestRes.Rows.Count == 0
						|| !studentId.HasValue) {
					testResRow = tableTestRes.NewRow();
					testResRow["id"] = Guid.NewGuid();
					testResRow["Student"] = studentId;
					testResRow["Test"] = tableQ.Rows[0]["id"];
					testResRow["Complete"] = false;
					testResRow["Skipped"] = false;
					testResRow["Tries"] = 0;
					tableTestRes.Rows.Add(testResRow);
				} else {
					testResRow = tableTestRes.Rows[0];
				}

				testResRow["TryStart"] = DateTime.Now;
				testResRow["Tries"] = (int)testResRow["Tries"] + 1;
				testResRow["CompletionDate"] = DBNull.Value;

					DataSet dsQw = Test_SelectQuestions(this.qId);
					DataTable tableQw = dsQw.Tables["Qw"];

					doc.DocumentElement.InnerXml += dsQw.GetXml().Replace("&lt;", "<").Replace("&gt;", ">");

					if (this.IsPostBack
							&& this.Request.Form["qForm"] != null
							&& tableQw != null
							&& tableQw.Rows.Count > 0
							&& Request.Url.Authority == Request.Headers["Host"]) {
						Guid? restResid = (Guid?)testResRow["id"];

						if (restResid.HasValue) {
							dbData.Instance.ExecSQL(string.Format(@"
delete from
		dbo.TestAnswers
where TestResults='{0}'", restResid));
						}

						string select = @"
select	*
from	dbo.TestAnswers
where TestResults='"
						 + restResid + "'";
						DataSet dsAw = dbData.Instance.getDataSet(select, "dsAw", "Answers");
						DataTable tableAw = dsAw.Tables["Answers"];

						if (tableAw != null) {
							bool hasAnswer = false;

							foreach (DataRow row in tableQw.Rows) {
								XmlDocument adoc = new XmlDocument();
								adoc.LoadXml(row["Answer"].ToString());
								XmlAttribute aType = adoc.DocumentElement.Attributes["type"];

								if (aType != null) {
									DataRow rowAw = null;
									rowAw = tableAw.NewRow();
									rowAw["Question"] = row["id"];
									rowAw["TestResults"] = testResRow["id"];
									tableAw.Rows.Add(rowAw);
									string answer = string.Empty;

									switch (aType.InnerText) {
										case "single": // Одиночный выбор
											answer = this.Request.Form[row["id"].ToString()];

											for (int i = 0; i < adoc.DocumentElement.ChildNodes.Count; i++) {

												if ((i + 1).ToString() == answer) {
													XmlAttribute selected = adoc.CreateAttribute("selected");
													selected.Value = "true";
													adoc.DocumentElement.ChildNodes[i].Attributes.Append(selected);
													hasAnswer = hasAnswer || true;
													break;
												}
											}
											break;
										case "multiple": // Множественный выбор

											for (int i = 0; i < adoc.DocumentElement.ChildNodes.Count; i++) {
												bool check = this.Request.Form[row["id"].ToString() + (i + 1).ToString()] != null;

												if (check) {
													XmlAttribute selected = adoc.CreateAttribute("selected");
													selected.Value = "true";
													adoc.DocumentElement.ChildNodes[i].Attributes.Append(selected);
													hasAnswer = hasAnswer || true;
												}
											}
											break;
										case "textbox": // Поле для ввода
											answer = this.Request.Form[row["id"].ToString()];
											answer = answer.Trim();
											XmlNode t = adoc.DocumentElement.SelectSingleNode("Answer");

											if (t != null) {
												XmlNode result = adoc.CreateNode(XmlNodeType.Element, "result", string.Empty);
												result.InnerText = answer;
												t.AppendChild(result);
												hasAnswer = hasAnswer || true;
											}
											break;
									}

									rowAw["Answer"] = adoc.InnerXml;
								}
							}

							testResRow["CompletionDate"] = DateTime.Now;
							if (hasAnswer) {
								dbData.Instance.UpdateDataSets(
									new string[] { trSelect, select },
									new string[] { "TestResult", "Answers" },
									new DataSet[] { dsTestRes, dsAw });
							}
							
							string _backUrl = this.Session["Back"] as string;
							
							if (!string.IsNullOrEmpty(_backUrl)) {
								this.Session.Remove("Back");
								this.Response.Redirect(_backUrl);
							}
							doc.DocumentElement.InnerXml += "<thanks>true</thanks>";
						}
					}
				}
		}

		static DataSet Test_Select(Guid? questionId)
		{
			string select0 = string.Format(@"
SELECT	*
FROM	dbo.Tests ts
where	ts.id = '{0}'", questionId);
			
			return dbData.Instance.getDataSet(select0, "dsQ", "Tests");
		}

		static DataSet Test_SelectQuestions(Guid? questionId)
		{
			string select0 = string.Format(@"
select	q.id,
		q.Type as cType,
		q.Points,
		dbo.GetContentAlt(q.Content,'{0}','{1}') as Content,
		dbo.GetTDataContentAlt(q.Answer,'{0}','{1}') as Answer
from	dbo.TestQuestions q 
where	q.Test='{2}'
order by QOrder", LocalisationService.Language,
					LocalisationService.DefaultLanguage,
					questionId);

			return dbData.Instance.getDataSet(select0, "dsQw", "Qw");
		}

		static DataSet Test_SelectByStudent(Guid? studentId, Guid? questionId)
		{
			string select = string.Format(@"
SELECT	dbo.StudentName('{0}',{1}) as Student,
		ts.InternalName as Questionnaire,
		dbo.GetStrContentAlt(c.Name,  '{2}', '{3}') as Course
FROM	Tests ts
	LEFT OUTER JOIN	Courses c
		ON c.StartQuestionnaire = ts.id
		OR c.FinishQuestionnaire = ts.id
WHERE	ts.id = '{4}'",
					studentId,
					LocalisationService.Language == "EN" ? 1 : 0,
					LocalisationService.Language,
					LocalisationService.DefaultLanguage,
					questionId);
			
			return dbData.Instance.getDataSet(select, "dsCommon", "Common");
		}

		static DataSet Test_SelectCommon()
		{
			string select = @"SELECT 
                  ts.id, ts.InternalName as Questionnaire
               FROM  Tests ts, Entities e
               WHERE ts.Type = " + (int)DCEAccessLib.TestType.globalquestionnaire
			   + "and e.id=ts.id and e.Type=" + (int)DCEAccessLib.EntityType.globalquestionnaire;
			return dbData.Instance.getDataSet(select, "dsCommon", "Common");
		}
	}
}
