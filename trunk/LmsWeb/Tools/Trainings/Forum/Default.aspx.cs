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

public partial class Trainings_Forum_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if( PageParameters.ID == null )
            Response.Redirect("../");
    }

    protected void createTopicButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateTopic.aspx?id="+PageParameters.ID);
    }
}
