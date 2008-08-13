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

public partial class Administration_GroupList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if(!this.IsPostBack) {
			GridView1.DataBind();
			addButton.Enabled = GroupManagementPolicy.AllowedForCurrentUser;
			GridView1.Columns[GridView1.Columns.Count-1].Visible = GroupManagementPolicy.AllowedForCurrentUser;
		}
    }

    protected void addButton_Click(object sender, EventArgs e)
    {
    }
	protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
	{
		e.Cancel = false;
	}
}
