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

public partial class Administration_MultiUserChangeRole : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void setRoleButton_Click(object sender, EventArgs e)
    {
        foreach( Guid userID in GuidListHelpers.GetMultiUserList() )
        {
            //SetUserRole(userID, RoleSelect1.SelectedRole);
        }

        Response.Redirect("User.aspx?id=" + Request["id"]);
    }

    void SetUserRole(Guid userID, Guid? roleID)
    {
        AdminQueriesTableAdapters.QueriesTableAdapter spAdapter = new AdminQueriesTableAdapters.QueriesTableAdapter();
        spAdapter.dcetools_Access_Users_SetUserRole(
            userID,
            roleID);
    }
}
