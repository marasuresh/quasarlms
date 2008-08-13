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

public partial class Security_RoleRegionDisplayControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.EnableViewState = false;

		string[] _roles = sec.Roles.GetRolesForUser();

		roleLabel.Text = _roles.Length > 0 ? _roles[0] : string.Empty;
        roleLabel.Visible = true;

        if( CurrentUser.Region != Regions.Global )
        {
            regionLabel.Text = CurrentUser.Region.Name;
            regionLabel.Visible = true;
        }
        else
        {
            regionLabel.Text = null;
            regionLabel.Visible = false;
        }

        if( roleLabel.Visible && regionLabel.Visible )
            roleLabel.Text += ", ";
    }
}