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

public partial class Upgrade_Default : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e)
	{

	}
	protected void Button1_Click(object sender, EventArgs e)
	{
		int _newRoleCount = 0;
		
		foreach(GridViewRow _row in this.GridView1.Rows) {
			string _roleName = _row.Cells[2].Text;
			if(!sec.Roles.RoleExists(_roleName)) {
				sec.Roles.CreateRole(_roleName);
				_newRoleCount++;
			}
		}

		if(_newRoleCount > 0) {
			this.Literal1.Text = string.Format("{0} role(s) created", _newRoleCount);
			this.Literal1.Visible = true;
		}
	}
}
