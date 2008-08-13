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

public partial class Trainings_Students_Subscribe : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if( PageParameters.ID == null )
            Response.Redirect("../Default.aspx");
    }

    protected void SubscribeStudentList_StudentSubscribed(object sender, EventArgs e)
    {
        if( !multipleCheckBox.Checked )
            Response.Redirect("Default.aspx?id="+Request["id"]);
    }    
}
