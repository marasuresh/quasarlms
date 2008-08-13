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

public partial class News_Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool canWrite = new TrainingQueriesTableAdapters.dcetools_Access_GetCanObjectID_WriteTableAdapter().GetData(
            CurrentUser.Region.ID,
            PageParameters.ID)[0].Column1;

        if( !canWrite )
        {
            removeButton.Visible = false;
        }

    }
    protected void removeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("RemoveNewsItem.aspx?id=" + PageParameters.ID);
    }
}
