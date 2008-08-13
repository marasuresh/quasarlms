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

public partial class Trainings_Forum_CreateTopic : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx?id=" + PageParameters.ID);
    }

    protected void createTopicButton_Click(object sender, EventArgs e)
    {
        NewsQueriesTableAdapters.QueriesTableAdapter spAdapter = new NewsQueriesTableAdapters.QueriesTableAdapter();
        spAdapter.dcetools_Trainings_Forum_CreateTopic(
            CurrentUser.Region.ID,
            PageParameters.ID,
            titleTextBox.Text,
            messateTextBox.Text,
            CurrentUser.UserID);

        Response.Redirect("Default.aspx?id="+PageParameters.ID);
    }
}