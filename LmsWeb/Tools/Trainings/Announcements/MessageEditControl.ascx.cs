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
using System.Data.Common;

public partial class Trainings_Announcements_MessageEditControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public bool AllowEdit
    {
        get { return announcementDetailsView.Fields[announcementDetailsView.Fields.Count - 1].Visible; }
        set { announcementDetailsView.Fields[announcementDetailsView.Fields.Count - 1].Visible = value; }
    }

    protected void AnnouncementDetailsDataSource_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {
    }

    protected void deleteButton_Click(object sender, EventArgs e)
    {

    }
}
