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
using System.ComponentModel;

public partial class Tools_Administration_GroupDetails : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!this.IsPostBack) {
			string _role = this.Request.QueryString["Group"];
			if (sec.Roles.RoleExists(_role)) {
				this.DetailsView1.DataSource = new string[] { _role };
				this.DetailsView1.DataBind();
			}
		}
    }
}
