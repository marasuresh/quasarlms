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
using System.Data.SqlClient;

public partial class Administration_MultiRemoveFromGroupControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AllGroupsControl1.GroupClick += new EventHandler<GuidEventArgs>(AllGroupsControl1_GroupClick);
    }

    void AllGroupsControl1_GroupClick(object sender, GuidEventArgs e)
    {
        foreach( Guid userID in GuidListHelpers.GetMultiUserList() )
        {
            RemoveUserFromGroup(userID, e.Guid);
        }
        Response.Redirect("MultiUsers.aspx?list=" + Request["list"]);
    }

    void RemoveUserFromGroup(Guid userID, Guid groupID)
    {
        try
        {
            AdminQueriesTableAdapters.QueriesTableAdapter spAdapter = new AdminQueriesTableAdapters.QueriesTableAdapter();
            spAdapter.dcetools_Access_Users_RemoveUserFromGroup(
                CurrentUser.Region.ID,
                userID,
                groupID);
        }
        catch( SqlException )
        {
        }
    }
}
