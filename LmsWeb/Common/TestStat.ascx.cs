namespace DCE.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	/// Статистика теста
	/// </summary>
	public partial  class TestStat : DCE.BaseTrainingControl
	{

      protected System.Xml.Xsl.XslTransform trans = new System.Xml.Xsl.XslTransform();

		protected void Page_Load(object sender, System.EventArgs e)
		{
		string CoursesRoot = DCE.Settings.getCoursesRoot();
         this.Xml1.Document = doc;
         this.Xml1.TransformArgumentList = new System.Xml.Xsl.XsltArgumentList();
         this.Xml1.TransformArgumentList.AddParam("LangPath", "", 
            DCE.Service.GetLanguagePath(this.Page));
         this.Xml1.Transform = trans;

         DCE.Service.LoadXmlDoc(this.Page, doc, "TestWork.xml");
         trans.Load(this.Page.MapPath(@"~/xsl/TestStat.xslt"));

         Guid? trId = PageParameters.trId;
         bool sessionIds = false;
         Guid? studentId = CurrentUser.UserID;
         if(!studentId.HasValue) {
            studentId = GuidService.Parse(this.Request["studentId"]);
            sessionIds = false;
            DCE.Service.SetTitle("Статистика обучения", this.Page);
         }

		 if (sessionIds) {
			 this.doc.DocumentElement.InnerXml += "<Session>true</Session>";
		 }

		Guid? testId = GuidService.Parse(this.Request["testId"]);
		if(!testId.HasValue) {
			object oTestId = this.Session["testId"];
			
			if (oTestId is Guid) {
				testId = (Guid)oTestId;
			}
		}
			
			if(testId.HasValue) {
				DataSet dsTest = Test_Select(testId);
				DataTable tableTest = dsTest.Tables["Tests"];

            if (tableTest != null && tableTest.Rows.Count == 1)
            {
               System.Data.DataRow testRow = tableTest.Rows[0];
               //doc.DocumentElement.InnerXml += "<Course>"+testRow["Course"].ToString()+"</Course>";

				int _type = (int)testRow["Type"];
				System.Data.DataSet dsCommon = Test_SelectThemesByUser(studentId, testId, _type);
               System.Data.DataTable tableCommon = dsCommon.Tables["Common"];
               doc.DocumentElement.InnerXml += dsCommon.GetXml();
               if (tableCommon != null && tableCommon.Rows.Count == 1)
               {
				   Session["CourseLanguage"] = tableCommon.Rows[0]["CourseLanguage"];

                  System.Data.DataSet dsTPoints = Test_SelectPoints(testId);
                  doc.DocumentElement.InnerXml += dsTPoints.GetXml();
               }
				Guid? _testId = (Guid?)testRow["id"];
				System.Data.DataSet dsTestRes = Test_SelectResults(studentId, _testId);
               System.Data.DataTable tableTestRes = dsTestRes.Tables["TestResult"];
               System.Data.DataRow testResRow = null;
               if (tableTestRes.Rows.Count != 0)
               {
                  testResRow = tableTestRes.Rows[0];

                  string testEnd = "";
                  if ((int)testRow["Points"] <= (int)testResRow["Points"])
                  {
                     testResRow["Complete"] = true;
                     testResRow["CompletionDate"] = System.DateTime.Now;
                     testEnd = "<testEnd>Complete</testEnd>";
                  }
                  else
                  {
                     // Тест закончен
                     testEnd = "<testEnd>Failure</testEnd>";
                  }
//                  if (practice)
//                  {
//                     testEnd = "<testEnd>PracticeEnd</testEnd>";
//                  }
                  doc.DocumentElement.InnerXml += testEnd;
                  
				   Guid? testRowId = (Guid?)testResRow["id"];
                  System.Data.DataSet dsQw = Test_SelectRows(testRowId);
                  System.Data.DataTable tableQw = dsQw.Tables["Qw"];
                  if (tableQw != null && tableQw.Rows.Count > 0)
                  {
                     string dsStr = dsQw.GetXml();
                     dsStr = dsStr.Replace("&lt;", "<");
                     dsStr = dsStr.Replace("&gt;", ">");
                     doc.DocumentElement.InnerXml += dsStr;
                     doc.DocumentElement.InnerXml += "<cRoot>"+CoursesRoot+"</cRoot>";
                  }
                  
                  doc.DocumentElement.InnerXml 
                     += "<hType>"+((DCEAccessLib.HintType)testRow["Hints"]).ToString()+"</hType>";
                  doc.DocumentElement.InnerXml += dsTest.GetXml();
                  doc.DocumentElement.InnerXml += dsTestRes.GetXml();
               }
            }
         }
      }

		private System.Data.DataSet Test_SelectRows(Guid? testRowId)
		{
			string selectTestAnswers = @"
                     select q.id, ISNULL(a.Points,0) AS aPoints, q.Points AS qPoints, 
                        dbo.GetContentAlt(q.Content, '" + LocalisationService.Language + "', '" + LocalisationService.DefaultLanguage + @"') as Content,
                        dbo.GetTDataContentAlt(q.Answer, '" + LocalisationService.Language + "', '" + LocalisationService.DefaultLanguage + @"') as Question, 
                        q.Type as cType, a.Answer as Answer
                     FROM  TestAnswers a RIGHT OUTER JOIN
                        TestQuestions q INNER JOIN
                        TestResults tr ON q.Test = tr.Test ON a.Question = q.id 
                        AND a.TestResults = tr.id
                     WHERE (tr.id = '" + testRowId + "') order by q.QOrder";

			DataSet dsQw = dbData.Instance.getDataSet(selectTestAnswers, "dsQw", "Qw");
			return dsQw;
		}

		private System.Data.DataSet Test_SelectResults(Guid? studentId, Guid? testId)
		{
			string selectTestResults = "SELECT *, dbo.TestResultPoints(id) as Points FROM  dbo.TestResults where Test = '"
			   + testId + "' and Student='" + studentId + "'";
			DataSet dsTestRes = dbData.Instance.getDataSet(selectTestResults, "dsRes", "TestResult");
			return dsTestRes;
		}

		private System.Data.DataSet Test_SelectPoints(Guid? testId)
		{
			string selectTheme = @"
                     SELECT t.id as themeId, dbo.GetStrContentAlt(t.Name,  '" + LocalisationService.Language + "', '" + LocalisationService.DefaultLanguage + @"') as Theme,
                        (select SUM(q.Points) from dbo.TestQuestions q where q.Theme = t.id) as ThemeMaxPoints,
                        (select SUM(a.Points) from dbo.TestAnswers a, dbo.TestQuestions q where a.Question = q.id and  q.Theme = t.id) as ThemePoints
                     FROM Themes t 
                     where t.id in (select q.Theme from dbo.TestQuestions q where q.Test = '" + testId + @"')";
			DataSet dsTPoints = dbData.Instance.getDataSet(selectTheme, "dsTPoints", "TPoints");
			return dsTPoints;
		}

		private System.Data.DataSet Test_SelectThemesByUser(Guid? studentId, Guid? testId, int _type)
		{
			string select;
			if (_type > 2) {
				select = @"SELECT 
                        ts.id, ts.Type, c.StartQuestionnaire, c.FinishQuestionnaire,
                        dbo.StudentName('" + studentId + "', " + (LocalisationService.Language.Equals("EN", StringComparison.InvariantCultureIgnoreCase) ? 1 : 0).ToString() + @") as Student,
                        c.DiskFolder, ISNULL(l.Abbr, 'RU') as CourseLanguage,
                        dbo.GetStrContentAlt(c.Name,  '" + LocalisationService.Language + @"', l.Abbr) as Course
                     FROM  
                        Courses c inner join dbo.Languages l on l.id=c.CourseLanguage
                        RIGHT OUTER JOIN
                        Tests ts ON (c.StartQuestionnaire  = ts.id
                                 or  c.FinishQuestionnaire = ts.id)
                     WHERE ts.id = '" + testId + "'";
			} else {
				select = @"SELECT 
                        dbo.StudentName('" + studentId + "', " + (LocalisationService.Language.Equals("EN", StringComparison.InvariantCultureIgnoreCase) ? 1 : 0).ToString() + @") as Student,
                        dbo.GetStrContentAlt(t.Name,  '" + LocalisationService.Language + @"', l.Abbr) as Theme,
                        dbo.GetStrContentAlt(c.Name,  '" + LocalisationService.Language + @"', l.Abbr) as Course,
                        c.DiskFolder, ISNULL(l.Abbr, 'RU') as CourseLanguage,
                        dbo.GetStrContentAlt(t1.Name,  '" + LocalisationService.Language + @"', l.Abbr) as Practice
                     FROM  Themes t RIGHT OUTER JOIN
                        Courses c inner join dbo.Languages l on l.id=c.CourseLanguage
                        RIGHT OUTER JOIN
                        Tests ts ON c.id = dbo.GetTestCourse(ts.id) ON ts.Parent = t.id
                        left join Themes t1 on t1.Practice=ts.id
                     WHERE ts.id = '" + testId + "'";
			}
			DataSet dsCommon = dbData.Instance.getDataSet(select, "dsCommon", "Common");
			return dsCommon;
		}

		private System.Data.DataSet Test_Select(Guid? testId)
		{
			string selectTest = @"SELECT *, dbo.TestPoints(id) as MaxPoints
               FROM  dbo.Tests ts where ts.id = '"
		   + testId + "'";
			DataSet dsTest = dbData.Instance.getDataSet(selectTest, "dsTest", "Tests");
			return dsTest;
		}
	}
}
