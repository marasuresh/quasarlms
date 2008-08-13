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
using System.Collections.Generic;

public partial class Administration_UserList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public int PageSize
    {
        get { return searchUsersGridView.PageSize; }
        set { searchUsersGridView.PageSize = value; }
    }

    protected void editMiltipleUsersButton_Click(object sender, EventArgs e)
    {
        List<Guid> selectedUsers = new List<Guid>();

        foreach( GridViewRow row in searchUsersGridView.Rows )
        {
            if( row == searchUsersGridView.HeaderRow )
            {
                continue;
            }

            if( row.Cells.Count != searchUsersGridView.Columns.Count )
            {
                continue;
            }

            TableCell cell = row.Cells[row.Cells.Count - 1];
            CheckBox selectCheckBox = null;
            foreach( Control ctl in cell.Controls )
            {
                selectCheckBox = ctl as CheckBox;
                if( selectCheckBox!=null )
                    break;
            }

            if( selectCheckBox==null )
            {
                continue;
            }

            if( !selectCheckBox.Checked )
            {
                continue;
            }

            Guid rowUserID = (Guid)searchUsersGridView.DataKeys[row.DataItemIndex].Value;

            selectedUsers.Add(rowUserID);
        }

        if( selectedUsers.Count == 0 )
            return;

        Guid userListKey = Guid.NewGuid();
        Session[userListKey.ToString()] = selectedUsers.ToArray();
        Response.Redirect("MultiUsers.aspx?list=" + userListKey);
    }
}
