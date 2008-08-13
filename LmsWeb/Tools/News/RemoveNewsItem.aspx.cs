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

public partial class Tools_News_RemoveNewsItem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void yesButton_Click(object sender, EventArgs e)
    {
        new NewsQueriesTableAdapters.QueriesTableAdapter().dcetools_News_Delete(
            CurrentUser.Region.ID,
            PageParameters.ID);
        Response.Redirect("Default.aspx");
    }

    protected void noButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Details.aspx?id=" + PageParameters.ID);
    }
}
