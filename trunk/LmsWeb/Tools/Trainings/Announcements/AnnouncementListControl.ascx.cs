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

public partial class Trainings_Announcements_AnnouncementListControl : System.Web.UI.UserControl
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(Request["id"])) {
			Response.Redirect(Resources.PageUrl.PAGE_TRAININGS);
		}
	}

    public int PageSize
    {
        get { return announcementsGridView.PageSize; }
        set { announcementsGridView.PageSize = value; }
    }

    protected void announcementsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if( e.CommandName != "ShowMessage" )
            return;

        Response.Redirect(
            "Message.aspx?id=" + Request["id"] +
            "&msg=" + GridViewHelpers.GetKeyByCommandArgument(e.CommandArgument, announcementsGridView));
    }
}