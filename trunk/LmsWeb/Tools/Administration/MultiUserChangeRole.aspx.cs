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

public partial class Administration_MultiUserChangeRole : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if( CurrentUser.Role!=Dce.Roles.Administrator )
            Response.Redirect("Default.aspx");
    }
}