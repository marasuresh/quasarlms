using System;
using System.Data;
using System.Configuration;
using System.Web;
using sec = System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public static class GroupManagementPolicy
{
    public static bool AllowedForCurrentUser {
        get {
			return	sec.Roles.IsUserInRole("Administrator")
					|| sec.Roles.IsUserInRole("Tutor")
					|| sec.Roles.IsUserInRole("TutorRegional");
        }
    }
}
