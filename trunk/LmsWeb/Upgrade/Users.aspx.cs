using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using sec = System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Upgrade_Users : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e)
	{
		this.Literal1.Visible = false;
	}
	protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		if(e.CommandName.Equals("Import")) {
			Guid _id = (Guid)((System.Web.UI.WebControls.GridView)(e.CommandSource)).DataKeys[Int32.Parse((string)e.CommandArgument)].Value;
			DceUser _dceUser = DceUserService.GetUserByID(_id);
			sec.MembershipUser _mUser = sec.Membership.GetUser(_dceUser.Login);
			if(null == _mUser) {
				StringBuilder _s = new StringBuilder();
				
				
				
				try {
					_mUser = sec.Membership.CreateUser(
							_dceUser.Login,
							DceUserService.GetOldPlainTextPassword(_id),
							_dceUser.EMail);
					_s.AppendLine(string.Format("Creating user {0} with an old password failed", _dceUser.Login));
				} catch(sec.MembershipCreateUserException) {
					
					sec.SqlMembershipProvider _provider = (sec.SqlMembershipProvider)sec.Membership.Provider;
					string _autoPassword = _provider.GeneratePassword();
					
					_mUser = sec.Membership.CreateUser(
							_dceUser.Login,
							_autoPassword,
							_dceUser.EMail);

					_s.AppendLine(string.Format("Autogenerated password {0}", _autoPassword));
				}
				
				if(null != _mUser) {
					_s.AppendLine(string.Format("Membership user {0} created", _mUser.UserName));
					
					string _role = DceUserService.GetRoleCodeNameById(_dceUser.RoleID.GetValueOrDefault(Guid.Empty));
					if(!string.IsNullOrEmpty(_role)) {
						if(!sec.Roles.RoleExists(_role)) {
							sec.Roles.CreateRole(_role);
							_s.AppendLine(string.Format("Created role {0}", _role));
						}
						
						sec.Roles.AddUserToRole(_mUser.UserName, _role);
						_s.AppendLine(string.Format("Added user {1} to role {0}", _role, _mUser.UserName));
					}
					
					this.Literal1.Text = _s.ToString();
					this.Literal1.Visible = true;
				}
			} else {
				this.Literal1.Text = string.Format("Membership user {0} already exists", _mUser.UserName);
				this.Literal1.Visible = true;
			}
		}
	}
}
