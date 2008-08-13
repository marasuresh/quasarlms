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

public partial class Tools_Trainings_Tasks_TaskList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void tasksGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if( e.CommandName == "ShowTask" )
        {
            Response.Redirect(
                "TaskResults.aspx?id="+PageParameters.ID+"&task="+GridViewHelpers.GetKeyByCommandArgument(
                e.CommandArgument,
                tasksGridView));
        }
    }
}
