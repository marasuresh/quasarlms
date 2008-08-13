using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Web.SessionState;
using System.Security.Authentication;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;

public static class CurrentUser
{
    private static class CacheKeys
    {
        public static readonly string JustAuthenticated = Guid.NewGuid().ToString();

        public static readonly string UserID = "UserID";
        public static readonly string FullName = Guid.NewGuid().ToString();
        public static readonly string EMail = Guid.NewGuid().ToString();
        public static readonly string Role = Guid.NewGuid().ToString();
        public static readonly string RegionID = "homeRegion";
        public static readonly string Region = Guid.NewGuid().ToString();
    }

    public static bool IsAuthenticated
    {
        get
        {
            if( HttpContext.Current.Items[CacheKeys.JustAuthenticated] != null )
                return true;
            else if( !HttpContext.Current.Request.IsAuthenticated )
                return false;
            else
                return UserID != null;
        }
    }

    public static Guid? UserID
    {
        get {
			Guid? _result = default(Guid?);
			
			MembershipUser _mUser = GetCurrentMembershipUserSafe();
			
			if (null != _mUser) {
				_result = DceAccessLib.DAL.StudentController.GetIdByLogin(_mUser.UserName);
			}
			
			return _result;
		}
    }

    public static string FullName
    {
        get {
			string _result = string.Empty;
			MembershipUser _mUser = Membership.GetUser(false);
			if (null != _mUser) {
				DceUser _dceUser = DceAccessLib.DAL.StudentController.GetByLogin(_mUser.UserName);
				if (null != _dceUser) {
					_result = _dceUser.FirstName + " " + _dceUser.LastName;
				} else {
					_result = _mUser.UserName;
				}
			}
			return _result;
		}
    }

    public static string EMail
    {
        get {
			MembershipUser _mUser = Membership.GetUser(false);
			string _email = null;
			
			if (null != _mUser) {
				_email = _mUser.Email;
			}
			return _email;
		}
    }

    public static Dce.Roles.RoleInfo Role
    {
        get {
			MembershipUser _mUser = Membership.GetUser(false);
			if(null != _mUser) {
				System.Web.Security.Roles.GetRolesForUser();
			}
			Dce.Roles.RoleInfo _role = (Dce.Roles.RoleInfo)GetCachedValue(CacheKeys.Role);
			if (null != _role) {
				return _role;
			} else {
				return Dce.Roles.Student;
			}
		}
    }

    public static Regions.RegionInfo Region {
		get {
			Regions.RegionInfo _region = (Regions.RegionInfo)GetCachedValue(CacheKeys.Region);
			if (null == _region) {
				_region = new Regions.RegionInfo(null, "Default");
			}
			return _region;
		}
    }
	
	[Obsolete("Superceded by a standard Asp.Net membership infrastructure", true)]
    public static void SetAuthenticatedUser(
        Guid userID,
        string login,
        string fullName,
        string email,
        Guid? roleID,
        Guid? regionID)
    {
        if( HttpContext.Current == null )
            return;

        HttpContext.Current.Items[CacheKeys.JustAuthenticated] = true;

        Dce.Roles.RoleInfo role = Dce.Roles.FindByID(roleID);
        Regions.RegionInfo region = Regions.FindByID(regionID);
        if( role.IsGlobal )
        {
            region = Regions.Global;
            regionID = null;
        }

        SetCachedValue(CacheKeys.UserID, userID);
        SetCachedValue(CacheKeys.FullName, fullName);
        SetCachedValue(CacheKeys.EMail, email);
        SetCachedValue(CacheKeys.RegionID, regionID);

        SetCachedValue(CacheKeys.Role, role);
        SetCachedValue(CacheKeys.Region, region );
    }

    static void SetCachedValue(string name, object value)
    {
        if( HttpContext.Current == null )
            return;
        if( HttpContext.Current.Session != null )
            HttpContext.Current.Session[name] = value;
        else
            HttpContext.Current.Items[name] = value;
    }

    static object GetCachedValue(string name)
    {
        if( HttpContext.Current == null )
            return null;
        if( HttpContext.Current.Session != null )
            return HttpContext.Current.Session[name];
        else
            return HttpContext.Current.Items[name];
    }
	
	public static MembershipUser GetCurrentMembershipUserSafe()
	{
		///When using ActiveDirectoryMembershipProvider
		/// and there is no user logged in,
		/// the query for a current user against Membership class
		/// will throw ArgumentException exception.
		/// There's no need for such measures when using SqlMembershipProvider
		MembershipUser _mUser = null;
		if (Membership.Provider is ActiveDirectoryMembershipProvider) {
			try {
				_mUser = Membership.GetUser();
			} catch (ArgumentException) {
			}
		} else {
			_mUser = Membership.GetUser();
		}
		
		return _mUser;
	}
}