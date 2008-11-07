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

public partial class UserControls_Chat_ChatGrand : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected string nombreUsuario()
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            return HttpContext.Current.User.Identity.Name;
        }
        else
        {
            return "anonymous";
        }
    }
}
