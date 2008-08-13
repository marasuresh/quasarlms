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

public partial class Trainings_Announcements_CreateMessageControl : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        dateCalendar.SelectedDate = DateTime.Today;
        dateCalendar.VisibleDate = DateTime.Today;

        authorLabel.Text = CurrentUser.FullName;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void createButton_Click(object sender, EventArgs e)
    {
        new TrainingQueriesTableAdapters.StoredProcedures().CreateAnnouncement(
            CurrentUser.Region.ID,
            PageParameters.ID,
            messageTextBox.Text,
            CurrentUser.UserID);

        Response.Redirect("Default.aspx?id=" + PageParameters.ID);
    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx?id=" + PageParameters.ID);
    }
}
