namespace DCE
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
   using System.Data.SqlClient;
   using System.Xml;
   using System.Xml.XPath;
   using System.Xml.Xsl;

	/// <summary>
	/// Задачи
	/// </summary>
	public partial  class classRoomTask : BaseTrainingControl
	{
      public string errMsg = "";
      public string strError = "";
      public dbTableControl editControl;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Guid? trID = DCE.Service.TrainingID;
			Guid? stID = CurrentUser.UserID;
			
			if (!stID.HasValue || !trID.HasValue) {
				return;
			}
			Session["xmlError"]="";
			
			Guid? recordId = GuidService.Parse(Request.QueryString["recordId"]);
			
			if(recordId.HasValue && !this.IsPostBack) {
               //this.Session["rId"] = recordId;
               this.editControl = (dbTableControl)this.LoadControl("dbTableControl.ascx");
               this.editControl.Index = "2";
               this.editControl.InputSQL = "ClassRoomTask";
               this.editControl.InputXSL = "ClassRoomTask.xslt";
               this.editControl.langXML = "classRoom";
               this.editControl.InputVariables = "dceLanguage;dceDefLang";
               PlaceHolder1.Controls.Add(this.editControl);
            }

			if (this.IsPostBack) {
				string EditTaskTxt = Request.Form["EditTaskTxt"];
				bool _isNewAnswer = !string.IsNullOrEmpty(EditTaskTxt);
				bool _isEditTaskButton = !string.IsNullOrEmpty(Request.Form["btnEditTask"]);
				
				if (_isNewAnswer) { // добавить новый ответ

					try {
						if (_isEditTaskButton) {
							string strSQL = @"
UPDATE	dbo.TaskSolutions
SET		Solution=@msg,
		Complete=0,
		SDate={fn NOW()}
where id='" + recordId + "'";

							dbData db = dbData.Instance;
							SqlCommand lCommand = db.Connection.CreateCommand();
							lCommand.CommandText = strSQL;
							lCommand.Parameters.Add("@msg", EditTaskTxt);
							db.ExecSQL(lCommand);
						}
					} catch (Exception err) {
						this.strError = err.Message;
						Response.Write("<script language=javascript>");
						Response.Write("alert('" + this.strError.Replace("'", "`") + "');");
						Response.Write("</script>");
						Session["xmlError"] = "<xml><Error>" + this.strError + "</Error></xml>";
					}
				}
			}
		}
	}
   /*
[12:29:29] Рябчун Александр:
в задачах нужно втулить пометку - проверено ли решение инструктором - и если проверено - то отметку инструктора - правильно ли решена задача
    
[12:34:06] Стасенко Игорь Геннадиевич:
TaskSolutions
.Solution = NULL - нет решения

.Solution != NULL
: Complete = 0     - не проверена
: Complete = 1     - решение правильное
: Complete = 2     - решение неправильное

[13:18:08] Стасенко Игорь Геннадиевич:
Complete -> 0 
всегда когда студент постит (или правит) решение.

[13:14:22] Рябчун Александр:
в табличке после "срок" добавь колонку - "решение"
возможные значения:
 "нет решения"
 "не проверено"
 "правильно"
 "не правильно"

    */
}
