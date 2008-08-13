namespace DCE.Common
{
   using System;
   using System.Data;
   using System.Drawing;
   using System.Web;
   using System.Web.UI.WebControls;
   using System.Web.UI.HtmlControls;

   /// <summary>
   /// Практика
   /// </summary>
	public partial  class Practice : DCE.Common.TestWork
	{
		void PreLoadApply()
      {
         Guid? trId = DCE.Service.TrainingID;
         Guid? courseId = DCE.Service.courseId;
         Guid? studentId = CurrentUser.UserID;

         DCE.Service.LoadXmlDoc(this.Page, doc, "TestWork.xml");
         doc.DocumentElement.InnerXml += "<isPractice>true</isPractice>";

         trans.Load(this.Page.MapPath(@"~/xsl/TestWork.xslt"));

         string croot = Settings.getValue("Courses/root");
         if (croot[croot.Length-1] != '/')
            croot += "/";

         Guid? testId = PageParameters.ID;
         if(testId.HasValue && studentId.HasValue) {

            string select = @"SELECT 
                  '"+this.CoursesRootUrl+@"' as cRoot, '"+croot+@"' as PublicRoot, c.DiskFolder,
                  dbo.GetStrContentAlt(t.Name,  '" + LocalisationService.Language + "', '" + LocalisationService.DefaultLanguage + @"') as Theme,
                  dbo.GetStrContentAlt(c.Name,  '" + LocalisationService.Language + "', '" + LocalisationService.DefaultLanguage + @"') as Course
               FROM  Themes t RIGHT OUTER JOIN
                  Courses c INNER JOIN
                  Tests ts ON c.id = dbo.GetTestCourse(ts.id) ON ts.Parent = t.id
               WHERE ts.id = '" + testId + "'";
			DataSet dsCommon = dbData.Instance.getDataSet(select, "dsCommon", "Common");
            System.Data.DataTable tableCommon = dsCommon.Tables["Common"];
            doc.DocumentElement.InnerXml += dsCommon.GetXml();

            select = "SELECT *, dbo.TestPoints(id) as MaxPoints FROM  dbo.Tests where id = '"
               +testId+"'";
			DataSet dsTest = dbData.Instance.getDataSet(select, "dsTest", "Tests");
            System.Data.DataTable tableTest = dsTest.Tables["Tests"];
            if (tableTest != null && tableTest.Rows.Count == 1)
            {
               System.Data.DataRow testRow = tableTest.Rows[0];

               select = "SELECT *, dbo.TestResultPoints(id) as Points FROM  dbo.TestResults where Test = '"
                  +testRow["id"]+"' and Student='"+studentId+"'";
			   DataSet dsTestRes = dbData.Instance.getDataSet(select, "dsRes", "TestResult");
               System.Data.DataTable tableTestRes = dsTestRes.Tables["TestResult"];
               System.Data.DataRow testResRow = null;
               if (tableTestRes.Rows.Count == 0)
               {
                  testResRow = tableTestRes.NewRow();
                  testResRow["id"] = System.Guid.NewGuid();
                  testResRow["Tries"] = 0;
                  testResRow["AllowTries"] = 2;
                  testResRow["Student"] = studentId;
                  testResRow["Test"] = tableTest.Rows[0]["id"];
                  testResRow["Complete"] = false;
                  testResRow["Skipped"] = false;
                  tableTestRes.Rows.Add(testResRow);
               }
               else
               {
                  testResRow = tableTestRes.Rows[0];
               }

               if (this.Request.Form["qwId"] != null)
               {
                  // Принять ответ
                  this.qwAccept(testRow, ref testResRow, true);
				  dbData.Instance.UpdateDataSet(select, "TestResult", ref dsTestRes);
                  testResRow = dsTestRes.Tables["TestResult"].Rows[0];
                  // Следующий вопрос
                  bool complete = this.qwWork(testId, studentId, testRow, ref testResRow, true);
				  dbData.Instance.UpdateDataSet(select, "TestResult", ref dsTestRes);
                  if (complete)
                  {
                     this.Response.Redirect(Resources.PageUrl.PAGE_TRAINING + "?cset=TestStat&trId="
                        +this.Request["trId"]+"&id="+this.Request["id"]);
                  }
               }
               else if (this.Request.Form["formAction"] == "SwitchLang") 
               {  
                  // Переключение языка...
                  this.qwWork(testId, studentId, testRow, ref testResRow, true);
               }
               else
               {
                  this.Session["testId"] = testId;
                  testResRow["TryStart"] = System.DateTime.Now;
                  testResRow["Tries"] = (int)testResRow["Tries"] + 1;
                  testResRow["Complete"] = false;
                  testResRow["CompletionDate"] = System.DateTime.Now;
                  testResRow["Points"] = 0;

				  dbData.Instance.ExecSQL("delete from dbo.TestAnswers where TestResults='"
                     +testResRow["id"].ToString()+"'");

				  dbData.Instance.UpdateDataSet(select, "TestResult", ref dsTestRes);
                  testResRow = dsTestRes.Tables["TestResult"].Rows[0];
                  // Начало теста
                  this.qwWork(testId, studentId, testRow, ref testResRow, true);
				  dbData.Instance.UpdateDataSet(select, "TestResult", ref dsTestRes);
               }

               doc.DocumentElement.InnerXml += dsTest.GetXml();
               doc.DocumentElement.InnerXml += dsTestRes.GetXml();

               // Показывать подсказки
               doc.DocumentElement.InnerXml 
                  += "<hType>"+((DCEAccessLib.HintType)testRow["Hints"]).ToString()+"</hType>";
            }
         }
      }
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.PreLoadApply();
		}
   }
}
