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
	/// Отображает фото пользователя/студента с указанным photo id.
	/// </summary>
	public partial class UserPhoto : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			byte[] lOutBuff;
			string lStrPictType;
			
			Guid? _id = GuidService.Parse(Request.QueryString["id"]);
			
			if(_id.HasValue) {
				
				lOutBuff = DCE.dbData.GetPhoto(_id.Value, out lStrPictType);
				if(lOutBuff != null) {
					Response.Clear();
					Response.BinaryWrite(lOutBuff);
					Response.ContentType = lStrPictType;
					//Response.AddHeader("Last-Modified",lStrLastDate);
				} else {
					this.Response.Redirect("~/App_Themes/Default/images/NoPhoto.gif");
				}
			} else {
				this.Response.Redirect("~/App_Themes/Default/images/NoPhoto.gif");
			}
		}
	}
}
