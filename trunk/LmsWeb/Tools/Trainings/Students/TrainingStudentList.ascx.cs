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

public partial class Trainings_Students_TrainingStudentList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public int PageSize
    {
        get { return StudentsGridView.PageSize; }
        set { StudentsGridView.PageSize = value; }
    }

    protected void StudentsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Guid? studentID = null;
        if( e.CommandArgument is string )
        {
            int rowIndex;
            if( int.TryParse((string)e.CommandArgument, out rowIndex) )
            {
                studentID = (Guid)(
                    StudentsGridView.DataKeys[
                    StudentsGridView.Rows[rowIndex].DataItemIndex].Value);
            }
        }

        if( studentID != null )
        {
            if( e.CommandName == "Unsubscribe" )
            {
                OnRemoveStudent(studentID.Value);
            }
        }
    }

    private void OnRemoveStudent(Guid studentID)
    {
        TrainingQueriesTableAdapters.StoredProcedures spAdapter = new TrainingQueriesTableAdapters.StoredProcedures();
        spAdapter.UnsubscribeStudent(
            CurrentUser.Region.ID,
            PageParameters.ID,
            studentID);
        Response.Redirect(Request.Url.ToString());
    }
}