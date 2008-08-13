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

public partial class Orders_SubscribeAvailableTrainingList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if( string.IsNullOrEmpty(Request["id"]) )
            Response.Redirect("~/Tools/Orders");
    }

    protected void availableTrainingsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if( e.CommandName != "Subscribe" )
            return;

        OnClickSubscribeToTraining(
            GridViewHelpers.GetKeyByCommandArgument(
                    e.CommandArgument,
                    availableTrainingsGridView));
    }

    private void OnClickSubscribeToTraining(Guid trainingID)
    {
        OrdersQueriesTableAdapters.QueriesTableAdapter spAdapter = new OrdersQueriesTableAdapters.QueriesTableAdapter();
        Guid studentID = (Guid)(spAdapter.dcetools_Fn_Orders_GetStudentID(PageParameters.ID));

        spAdapter.dcetools_Orders_AcceptOrder(
            CurrentUser.Region.ID,
            PageParameters.ID,
            trainingID);

        MailNotificationServices.SendSubscribeNotificationToSudent(
            studentID,
            trainingID);

        Response.Redirect("~/Tools/Trainings/Students/Default.aspx?id=" + trainingID);
    }
}
