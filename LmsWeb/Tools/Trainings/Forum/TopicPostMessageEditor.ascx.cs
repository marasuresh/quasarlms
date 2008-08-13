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

public partial class Trainings_Forum_TopicPostMessageEditor : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    Guid? HomeRegion
    {
        get { return Session["homeRegion"] == null ? (Guid?)null : (Guid)Session["homeRegion"]; }
    }

    Guid UserID {
        get { return (Guid)Session["userID"]; }
    }

    Guid? Training {
        get { return PageParameters.ID; }
    }

    Guid? Topic {
        get { return GuidService.Parse(Request["topic"]); }
    }

    protected void postButton_Click(object sender, EventArgs e)
    {
        TrainingQueriesTableAdapters.StoredProcedures spAdapter = new TrainingQueriesTableAdapters.StoredProcedures();
        spAdapter.PostMessage(
            HomeRegion,
            Training,
            Topic,
            UserID,
            messageTextBox.Text);

        Response.Redirect("Default.aspx?id=" + PageParameters.ID);
    }
}
