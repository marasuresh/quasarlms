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

public partial class Administration_User : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if( PageParameters.ID==null )
            Response.Redirect("UserList.aspx");

        includeToOtherGroupButton.Enabled = GroupManagementPolicy.AllowedForCurrentUser;

        bool isAdministrator = sec.Roles.IsUserInRole("Administrator");

        changeRoleButton.Enabled = isAdministrator;
        irbisSynchronizeButton.Enabled = isAdministrator;
        advancedOfflineButton.Enabled = isAdministrator;
    }
    protected void includeToOtherGroupButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddToGroup.aspx?id=" + PageParameters.ID);
    }
    protected void changeRoleButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserChangeRole.aspx?id=" + PageParameters.ID);
    }
    protected void irbisSynchronizeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Irbis/SyncUserBase.aspx");
    }
    protected void advancedOfflineButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdvancedOfflineChange.aspx?id=" + PageParameters.ID);
    }
}
