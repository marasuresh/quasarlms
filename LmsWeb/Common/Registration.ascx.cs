using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;

namespace DCE.Common
{

	/// <summary>
	/// Регистрация нового студента
	/// </summary>
	public partial  class Registration : DCE.BaseWebControl
	{
		protected void createUserWizard_CreatedUser(object sender, EventArgs e)
		{
			string _login = this.createUserWizard.UserName;
			MembershipUser _mUser = Membership.GetUser(_login);
			DceUser _dceUser = DceUserService.GetUserByLogin(_login);
			
			if (null == _dceUser) {
				DceUserService.CreateUser(_login);
			}

			_dceUser = DceUserService.GetUserByLogin(_login);

			System.Web.UI.Control _template = this.createUserWizard.CreateUserStep.ContentTemplateContainer;
			TextBox _tbLastName = _template.FindControl("tbLastName") as TextBox;
			TextBox _tbFirstName = _template.FindControl("tbFirstName") as TextBox;
			TextBox _tbMidName = _template.FindControl("tbMidName") as TextBox;

			_dceUser.LastName = _tbLastName.Text;
			_dceUser.FirstName = _tbFirstName.Text;
			_dceUser.Patronymic = _tbMidName.Text;

			DceUserService.UpdateUser(_dceUser);
		}
	}
}
