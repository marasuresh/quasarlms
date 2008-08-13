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

public partial class Tools_Trainings_Tasks_CreateTaskDetailsControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if( PageParameters.ID==null )
            Response.Redirect("../Default.aspx");
    }
    
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx?id=" + PageParameters.ID);
    }

    protected void createButton_Click(object sender, EventArgs e)
    {
        new TrainingQueriesTableAdapters.StoredProcedures().CreateTask(
            CurrentUser.Region.ID,
            PageParameters.ID,
            CurrentUser.UserID,
            nameTextBox.Text,
            contentTextBox.Text);



        Response.Redirect("Default.aspx?id=" + PageParameters.ID);
    }
}
