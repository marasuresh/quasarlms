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

public partial class Tools_Trainings_Announcements_DeleteMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void noButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Message.aspx?id=" + PageParameters.ID + "&msg=" + PageParameters.Msg);
    }

    protected void yesButton_Click(object sender, EventArgs e)
    {
        new TrainingQueriesTableAdapters.StoredProcedures().DeleteAnnouncement(
            CurrentUser.Region.ID,
            PageParameters.Msg);
        Response.Redirect("Default.aspx?id=" + PageParameters.ID);
    }
}
