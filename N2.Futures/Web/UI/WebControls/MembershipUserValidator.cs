using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N2.Web.UI.WebControls
{
	using System.Web.Security;
	
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:MembershipUserValidator runat=server></{0}:MembershipUserValidator>")]
	public class MembershipUserValidator : BaseValidator
	{
		protected override bool EvaluateIsValid()
		{
			string controlValidationValue = base.GetControlValidationValue(base.ControlToValidate);
			
			if(string.IsNullOrEmpty(controlValidationValue)) {
				return true;
			}

			string[] _users = controlValidationValue.Split(';', ',', ' ');

			return
				Array.TrueForAll(_users,
					(user) => Membership.FindUsersByName(user).Count == 1 || Roles.RoleExists(user)
				);
		}
	}
}
