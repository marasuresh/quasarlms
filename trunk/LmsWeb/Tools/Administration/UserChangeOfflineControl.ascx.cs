using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Tools_Administration_UserChangeOfflineControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if( PageParameters.ID == null )
        {
            Response.Redirect("Default.aspx");
            return;
        }

        passwordChangedLabel.Visible = false;
        regionChangeLabel.Visible = false;
    }

    protected void changePasswordButton_Click(object sender, EventArgs e)
    {
        DceUserService.SetUserPassword(
            DceUserService.GetUserByID(PageParameters.ID.Value).Login,
            passwordTextBox.Text);

        passwordChangedLabel.Visible = true;
    }

    protected void changeRegionButton_Click(object sender, EventArgs e)
    {
        DceUser user = DceUserService.GetUserByID(PageParameters.ID.Value);
        user.RegionID = RegionEditControl1.RegionGuid;

        DceUserService.UpdateUser(user);

        regionChangeLabel.Visible = true;
    }
}
