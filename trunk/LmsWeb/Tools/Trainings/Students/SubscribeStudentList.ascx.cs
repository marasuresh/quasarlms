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

public partial class Trainings_Students_SubscribeStudentList : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        this.PageSize = 10;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public int PageSize
    {
        get { return usersGridView.PageSize; }
        set { usersGridView.PageSize = value; }
    }

    public event EventHandler StudentSubscribed;

    protected void usersGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if( e.CommandName != "SelectUser" )
            return;

        Guid userID = GridViewHelpers.GetKeyByCommandArgument(
            e.CommandArgument,
            usersGridView);

        OnClickSubscribeStudent(userID);
    }


    private void OnClickSubscribeStudent(Guid studentID)
    {
        TrainingQueriesTableAdapters.StoredProcedures spAdapter = new TrainingQueriesTableAdapters.StoredProcedures();
        spAdapter.SubscribeStudent(
            CurrentUser.Region.ID,
            PageParameters.ID,
            studentID);

        Page.DataBind();


        EventHandler temp = StudentSubscribed;
        if( temp != null )
            temp(this,EventArgs.Empty);
    }
}
