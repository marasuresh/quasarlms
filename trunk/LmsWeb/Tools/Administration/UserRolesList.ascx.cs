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

public partial class Administration_UserRolesList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if( e.CommandName != "Remove" )
            return;

        OnGroupRowClick(GridViewHelpers.GetKeyByCommandArgument(e.CommandArgument, GridView1));
    }

    private void OnGroupRowClick(Guid groupID)
    {
        AdminQueriesTableAdapters.QueriesTableAdapter spAdapter = new AdminQueriesTableAdapters.QueriesTableAdapter();
        spAdapter.dcetools_Access_Users_RemoveUserFromGroup(
            CurrentUser.Region.ID,
            PageParameters.ID,
            groupID);
        Response.Redirect("User.aspx?id=" + PageParameters.ID);
    }
}
