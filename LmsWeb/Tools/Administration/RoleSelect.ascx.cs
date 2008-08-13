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

public partial class Administration_RoleSelect : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if(!this.IsPostBack) {
			this.roleDropDownList.DataSource = sec.Roles.GetAllRoles();
			this.roleDropDownList.DataBind();
		}
    }

    public string SelectedRole {
        get {
			if(roleDropDownList.SelectedItem == null) {
				return null;
			} else if(string.IsNullOrEmpty(roleDropDownList.SelectedItem.Value)) {
				return null;
			} else {
				return roleDropDownList.SelectedItem.Value;
			}
        }
        set {
            foreach( ListItem item in roleDropDownList.Items ) {
                string itemValue = string.IsNullOrEmpty(item.Value) ? null : item.Value;
                item.Selected = (itemValue == value);
            }
        }
    }
}
