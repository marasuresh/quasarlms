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

public partial class Administration_MultiUserListItem : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    Guid? m_UserID;

    public Guid? UserID
    {
        get { return m_UserID; }
        set
        {
            if( value == this.UserID )
                return;
            m_UserID = value;
            UpdateFromUserID();
        }
    }

    private void UpdateFromUserID()
    {
        if( UserID == null )
        {
            userHyperLink.NavigateUrl = null;
            userHyperLink.ToolTip = null;
            userHyperLink.Text = "(none)";
        }
        else
        {
            userHyperLink.NavigateUrl = "User.aspx?id=" + UserID;

            AdminQueriesTableAdapters.dcetools_Access_Users_UserTitleTableAdapter getUserAdapter = new AdminQueriesTableAdapters.dcetools_Access_Users_UserTitleTableAdapter();
            AdminQueries.dcetools_Access_Users_UserTitleRow userRow = getUserAdapter.GetData(CurrentUser.Region.ID, UserID)[0];

            userHyperLink.Text =
                userRow.FullName +
                (userRow.IsRegionNameNull() ?
                "" :
                " (" + userRow.RegionName + ")");

            userHyperLink.ToolTip =
                userRow.JobPosition + " " + userRow.Comments + " " + userRow.EMail;
        }
    }
}
