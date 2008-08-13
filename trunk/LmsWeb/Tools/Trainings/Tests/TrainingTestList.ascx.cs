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

public partial class Trainings_Tests_TrainingTestList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if( string.IsNullOrEmpty(Request["id"]) )
            Response.Redirect(Resources.PageUrl.PAGE_TOOLS_TRAININGS);
    }

    protected void testResultsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if( e.CommandName != "ShowDetails" )
            return;

        Response.Redirect(
            "StudentResults.aspx?id=" + Request["id"] +
            "&test=" + GridViewHelpers.GetKeyByCommandArgument(e.CommandArgument, testResultsGridView));
    }
}
