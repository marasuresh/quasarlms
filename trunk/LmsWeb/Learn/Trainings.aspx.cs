using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace DCE
{
	/// <summary>
	/// Выбор тренинга для обучения
	/// </summary>
	public partial class Trainings : DCE.BaseWebPage
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.leftMenu = this.LeftMenu1;
			this.onLoadCenter();
			Guid? _studentId = CurrentUser.UserID;
			DataSet dsTraining = DceAccessLib.DAL.TrainingController.Select(_studentId.Value);
			DataTable tableTraining = dsTraining.Tables["item"];
			
			if (tableTraining != null && tableTraining.Rows.Count > 0) {
				DCE.Service.TrainingID = (Guid?)tableTraining.Rows[0]["id"];
				this.leftMenu.doc.LoadXml("<xml>" + dsTraining.GetXml() + "</xml>");
			} else {
				this.Response.Redirect(Resources.PageUrl.PAGE_MAIN__WELCOME);
			}
		}

		void onLoadCenter()
		{
			string _cset = this.Request["cset"] as string;
			Control _ctl = string.IsNullOrEmpty(_cset)
					? this.LoadControl(@"~\Common\Welcome.ascx")
					: this.LoadControl(@"~\Common\" + _cset + ".ascx")
						?? this.LoadControl(@"~\Common\Welcome.ascx");
			
			this.PlaceHolder1.Controls.Add(_ctl);
		}
   }
}
