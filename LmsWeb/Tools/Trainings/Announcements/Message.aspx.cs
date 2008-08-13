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

public partial class Trainings_Announcements_Message : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if( PageParameters.Msg == null )
            Response.Redirect("Default.aspx?id=" + PageParameters.ID);
    }

    protected void deleteButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("DeleteMessage.aspx?id=" + PageParameters.ID + "&msg=" + PageParameters.Msg);
    }
}
