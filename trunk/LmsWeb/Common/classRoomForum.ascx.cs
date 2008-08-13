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
	/// Форум
	/// </summary>
	public partial  class classRoomForum: BaseTrainingControl
	{
      public string errMsg = "";
      public string ftID = "";
      public string strError = "";
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string addMsgTxt = Request.Form["newReplyTxt"]+"";
			string newTopicSubj = Request.Form["newTopicSubject"]+"";
			Guid? trID = DCE.Service.TrainingID;
			Guid? stID = CurrentUser.UserID;
			Guid? ftID = GuidService.Parse(Request.QueryString["recordId"]);
			if(!stID.HasValue || !trID.HasValue) {
				this.setError("Login please!");
				return;
			}
			
			Session["Error"]="";
         
			try {
				if(!string.IsNullOrEmpty(addMsgTxt) || (Request.Form["btn1"]+""!="")) { // добавить новый ответ
					string strSQL = "INSERT INTO dbo.ForumReplies(Topic, Author, Message)VALUES("
                     +"'"+ftID+"'"
                     +",'"+stID+"',@msg"
                     +")";
					
					dbData db = dbData.Instance;
					SqlCommand lCommand = db.Connection.CreateCommand();
                  lCommand.CommandText = strSQL;
                  lCommand.Parameters.Add("@msg",addMsgTxt);
                  db.ExecSQL(lCommand);
            }
            if((newTopicSubj!="")||(Request.Form["btnTopicPost"]+""!="")) // если нажалась кнопка новая тема
            {
               string strSQL = "INSERT INTO dbo.ForumTopics( Training, Author, Student, Topic, Message)VALUES("
                  +"'"+trID+"'"
                  +",'"+stID+"'"
                  +",'"+stID+"',@subj,@msg"
                  +")";
               dbData db = new dbData();
               SqlCommand lCommand = db.Connection.CreateCommand();
               lCommand.CommandText = strSQL;
               lCommand.Parameters.Add("@subj",newTopicSubj);
               lCommand.Parameters.Add("@msg",Request.Form["newTopicTxt"]+"");
               db.ExecSQL(lCommand);
            }
				Guid? recordId = ftID;
				if(recordId.HasValue) {

				if(Request.Form["btnT1"]+""!="") {
					delControls();
				}

                  dbData db = new dbData();
                  System.Data.DataSet ds = db.getDataSet("select id from dbo.ForumTopics tp where Blocked=1 and id='"+recordId+"'", "ds", "Topic");
                  System.Data.DataTable dt = ds.Tables["Topic"];
                  if (dt != null && dt.Rows.Count > 0)
                  {
                     this.Controls[0].Controls.Remove(this.Controls[0].FindControl("XmlControl1"));
                  }
                  ds = db.getDataSet("select id from dbo.ForumTopics tp where id='"+recordId.Value+"'", "ds", "Topic");
                  dt = ds.Tables["Topic"];
                  if (dt == null || dt.Rows.Count == 0)
                  {
                     delControls();
                  }
            } else {
               delControls();
            }
         }
         catch(Exception err)
         {
            this.setError(err.Message);
         }
      }
      /// <summary>
      /// Убирает контролы с перепиской по выбранной теме, если она не выбрана
      /// </summary>
      private void delControls()
      {//я расчитываю что форум встраивается не непосредственно в страницу, а в контрол ClassRoom, при изменении убрать "Controls[0]"

         for(int i=0;i<this.Controls[0].Controls.Count;i++)
         {
            string cId = this.Controls[0].Controls[i].ID;
            switch(cId)
            {
               case "XmlControl1":
               case "Dbtablecontrol3":
               case "Dbtablecontrol2":
               {
                  this.Controls[0].Controls.RemoveAt(i);
                  break;
               }
            }
         }
      }
      private void setError(string strError)
      {
      }
	}
}
