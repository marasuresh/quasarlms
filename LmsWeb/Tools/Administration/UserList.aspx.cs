using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using sec = System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Administration_UserList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        synchronizeButton.Enabled = sec.Roles.IsUserInRole("Administrator");
    }
}