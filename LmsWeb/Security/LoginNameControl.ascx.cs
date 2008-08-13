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

public partial class Security_LoginNameControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        usernameLabel.Text = CurrentUser.FullName;
    }

    protected void logoutLinkButton_Click(object sender, EventArgs e)
    {
        DceAuthentication.Logoff();
        Response.Redirect("~/Login.aspx");
    }
}
