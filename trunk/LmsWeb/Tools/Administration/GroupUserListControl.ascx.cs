using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using sec = System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Tools_Administration_GroupUserListControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!this.IsPostBack) {
			string _role = this.Request.QueryString["group"];
			if (sec.Roles.RoleExists(_role)) {
				this.groupUserGridView.DataSource = sec.Roles.GetUsersInRole(_role);
				this.groupUserGridView.DataBind();
			}
		}
    }
}
