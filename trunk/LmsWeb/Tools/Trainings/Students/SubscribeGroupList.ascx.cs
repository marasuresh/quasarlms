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

public partial class Trainings_Students_SubscribeGroupList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if( e.CommandName=="Subscribe" )
            OnSubscribeGroup(GridViewHelpers.GetKeyByCommandArgument(e.CommandArgument,GridView1));
        else if( e.CommandName == "Unsubscribe" )
            OnUnsubscribeGroup(GridViewHelpers.GetKeyByCommandArgument(e.CommandArgument, GridView1));
    }

    private void OnSubscribeGroup(Guid groupID)
    {
        ChangeSubscription(groupID, true);
    }

    private void OnUnsubscribeGroup(Guid groupID)
    {
        ChangeSubscription(groupID, false);
    }

    void ChangeSubscription(Guid groupID, bool subscribe)
    {
        bool checkRegion = !CurrentUser.Role.IsGlobal;

        foreach( AdminQueries.UserListRow userRow in new AdminQueriesTableAdapters.UserList().GetDataByRegionGroup(CurrentUser.Region.ID, groupID) )
        {
            if( checkRegion )
            {
                Guid? userRegionID = DceUserService.GetUserByID(userRow.ID).RegionID;
                if( !object.Equals(userRegionID,CurrentUser.Region.ID) )
                    continue;
            }

            if( subscribe )
                new TrainingQueriesTableAdapters.StoredProcedures().SubscribeStudent(
                    CurrentUser.Region.ID,
                    PageParameters.ID,
                    userRow.ID);
            else
                new TrainingQueriesTableAdapters.StoredProcedures().UnsubscribeStudent(
                    CurrentUser.Region.ID,
                    PageParameters.ID,
                    userRow.ID);
        }

        Response.Redirect("Default.aspx?id=" + PageParameters.ID);
    }
}
