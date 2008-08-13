using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using System.Diagnostics;

public static class DceAuthentication
{
    static readonly System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
    /*
    public static bool Login(string username, string password)
    {
        Account ldapAccount = null;

        try
        {
            Trace.WriteLine("Ldap.CheckLogin...", "DceMembershipProvider");
            ldapAccount = LdapService.CheckLogin(username, password);

            if( ldapAccount == null )
                return false;
        }
        catch( System.DirectoryServices.Protocols.LdapException )
        {
        }

        DceUser checkUser = DceUserService.CheckUser(username, password);

        if( checkUser == null && ldapAccount == null )
            return false;

        if( checkUser == null )
        {
            checkUser = DceUserService.GetUserByLogin(username);
            if( checkUser == null )
            {
                DceUserService.CreateUser(username);
                DceUserService.SetUserPassword(username, password);
                checkUser = DceUserService.GetUserByLogin(username);
            }
            else
            {
                DceUserService.SetUserPassword(username, password);
            }


        }

        if( ldapAccount != null )
        {
            bool needChangePush = false;

            if( checkUser.EMail != ldapAccount.EMail )
            {
                checkUser.EMail = ldapAccount.EMail;
                needChangePush = true;
            }

            Guid? regionID = Regions.FindByCode(ldapAccount.RegionCode).ID;
            if( Roles.FindByID(checkUser.RoleID).IsGlobal )
                regionID = null;

            if( checkUser.RegionID != regionID )
            {
                checkUser.RegionID = regionID;
                needChangePush = true;
            }

            if( needChangePush )
                DceUserService.UpdateUser(checkUser);
        }

        string fullName = (checkUser.FirstName + " " + checkUser.Patronymic + " " + checkUser.LastName).Trim();

        CurrentUser.SetAuthenticatedUser(
            checkUser.ID,
            username,
            fullName,
            checkUser.EMail,
            checkUser.RoleID,
            checkUser.RegionID);

        return true;
    }
	*/
    public static void Logoff()
    {
        HttpContext.Current.Session.Clear();
        HttpContext.Current.Session.Abandon();
        FormsAuthentication.SignOut();
    }
}