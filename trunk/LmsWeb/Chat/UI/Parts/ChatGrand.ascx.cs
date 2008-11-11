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
using N2.Resources;
using Subgurim.Chat;

public partial class UserControls_Chat_ChatGrand : System.Web.UI.UserControl
{

    protected override void OnPreRender(EventArgs e)
    {
        if (ActiveChannel != null)
        {
            string ClientChannelID = "ChannelID = '" + ActiveChannel.canal + "';";
            Register.JavaScript(this.Page, ClientChannelID, ScriptOptions.ScriptTags);
        }
        base.OnPreRender(e);
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

    /// <summary>
    /// The chat channel set on a server.
    /// </summary>
    public Channel ActiveChannel { get; set; }
    
}
