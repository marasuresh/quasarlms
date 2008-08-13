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

public partial class Administration_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        createUserButton.Visible = (CurrentUser.Role == Dce.Roles.Administrator);
    }

    protected void synchronizeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Irbis/SyncUserBase.aspx");
    }
    protected void createUserButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateUser.aspx");
    }
}
