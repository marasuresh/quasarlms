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

public partial class Administration_MultipleUsersModify : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool isAdministrator = (CurrentUser.Role == Dce.Roles.Administrator);

        applyRoleButton.Enabled = isAdministrator;
        addToGroupButton.Enabled = (GroupManagementPolicy.AllowedForCurrentUser);
        removeFromGroupButton.Enabled = (GroupManagementPolicy.AllowedForCurrentUser);
        synchronizeIrbisButton.Enabled = isAdministrator;
    }
    protected void applyRoleButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("MultiUserChangeRole.aspx?list=" + Request["list"]);
    }
    protected void addToGroupButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("MultiAddToGroup.aspx?list=" + Request["list"]);
    }
    protected void removeFromGroupButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("MultiRemoveFromGroup.aspx?list=" + Request["list"]);
    }
    protected void synchronizeIrbisButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Irbis/SyncUserBase.aspx");
    }
}
