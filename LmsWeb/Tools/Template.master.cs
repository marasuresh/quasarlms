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
using System.Threading;
using System.Security.Principal;

public partial class Template : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if( Page.Title == "Untitled Page" && SiteMap.CurrentNode != null )
            Page.Title = SiteMap.CurrentNode.Title;            

        titleH2.Text = Page.Title;

        userLabel.Text =
			string.Format("{0} ({1}) {2}",
				CurrentUser.FullName,
				string.Join(", ", sec.Roles.GetRolesForUser()),
				CurrentUser.Region.Name);

		administrationHyperLink.Visible = sec.Roles.IsUserInRole("Administrator");
    }
}
