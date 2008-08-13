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

public partial class Administration_MultiUserList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        foreach( Guid selectedUser in GuidListHelpers.GetMultiUserList() )
        {
            Administration_MultiUserListItem userItemControl = (Administration_MultiUserListItem)LoadControl("MultiUserListItem.ascx");
            this.Controls.Add(userItemControl);

            userItemControl.UserID = selectedUser;
        }
    }
}
