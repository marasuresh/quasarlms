// © 2005 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Web;
using System.Web.Services;
using System.Web.Security;


[WebService(Namespace="http://SecurityServices",Description="Implements the <a href=\"IUserManager.asmx\">IUserManager</a> interface. Wraps with a web service the ASP.NET provider model. This web service should be accessed over https.")]
class UserManager : IUserManager
{
   public bool Authenticate(string userName,string password)
   {
      if(HttpContext.Current.Request.IsSecureConnection == false)
      {
         HttpContext.Current.Trace.Warn("You should use HTTPS to avoid sending passwords in clear text");
      }
      return Membership.ValidateUser(userName,password);
   }
   public bool IsInRole(string userName,string role)
   {
      return Roles.IsUserInRole(userName,role);
   }
   public string[] GetRoles(string userName)
   {
      return Roles.GetRolesForUser(userName);
   }

	public Guid GetId(string userName)
	{
		return DceAccessLib.DAL.StudentController.GetIdByLogin(userName).GetValueOrDefault();
	}
}  