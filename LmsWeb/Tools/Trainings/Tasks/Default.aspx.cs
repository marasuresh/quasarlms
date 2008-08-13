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

public partial class Trainings_Tasks_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if( PageParameters.ID == null )
            Response.Redirect("../");

        bool canWrite = new TrainingQueriesTableAdapters.dcetools_Access_GetCanObjectID_WriteTableAdapter().GetData(
            CurrentUser.Region.ID,
            PageParameters.ID)[0].Column1;

        createButton.Enabled = canWrite;
    }

    protected void createButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Create.aspx?id=" + PageParameters.ID);
    }
}
