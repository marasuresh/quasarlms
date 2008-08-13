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

public partial class Administration_UserChangeRole : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if( PageParameters.ID == null )
        {
            Response.Redirect("Users.aspx");
            return;
        }

        if( !IsPostBack )
        {
			string[] _roles = sec.Roles.GetAllRoles();
            RoleSelect1.SelectedRole = _roles.Length > 0 ? _roles[0] : null;;
			ViewState["RoleID"] = _roles[0];
        }
    }

    protected void setRoleButton_Click(object sender, EventArgs e)
    {
		sec.MembershipUser _mUser = sec.Membership.GetUser(PageParameters.ID);

		if(null == _mUser) {
			
			DceUser _dceUser = DceUserService.GetUserByID(PageParameters.ID.GetValueOrDefault(Guid.Empty));
			
			string _username = null != _dceUser ? _dceUser.Login : string.Empty;
			
			_mUser = sec.Membership.GetUser(_username);
			
			if(null != _mUser) {
				sec.Roles.AddUserToRole(_mUser.UserName, RoleSelect1.SelectedRole);
				Response.Redirect("User.aspx?id=" + Request["id"]);
			}
		}
    }
}