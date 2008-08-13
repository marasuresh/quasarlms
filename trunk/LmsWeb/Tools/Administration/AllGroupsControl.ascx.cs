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

public partial class Administration_AllGroupsControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public event EventHandler<GuidEventArgs> GroupClick;

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if( e.CommandName != "ExecuteCommand" )
            return;

        OnGroupRowClick(GridViewHelpers.GetKeyByCommandArgument(e.CommandArgument, GridView1));
    }

    private void OnGroupRowClick(Guid groupID)
    {
        EventHandler<GuidEventArgs> temp = GroupClick;
        if( temp != null )
            temp(this,new GuidEventArgs(groupID));
    }
}