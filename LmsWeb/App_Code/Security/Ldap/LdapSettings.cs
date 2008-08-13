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

public static class LdapSettings
{
    public static readonly string LdapServer = ConfigurationManager.AppSettings["LdapServer"];

    public static readonly string SearchAccountDistinguishedName = ConfigurationManager.AppSettings["LdapSearchAccountDn"];
    public static readonly string SearchAccountPassword = ConfigurationManager.AppSettings["LdapSearchAccountPassword"];
    public static readonly string SearchBase = ConfigurationManager.AppSettings["LdapSearchBase"];

    public static readonly string Filter = ConfigurationManager.AppSettings["LdapFilter"];

    public static readonly TimeSpan ListTimeout = GetListTimeoutWithDefault();
    static readonly TimeSpan DefaultListTimeout = TimeSpan.FromMinutes(4);

    static TimeSpan GetListTimeoutWithDefault()
    {
        string configListTimeoutString = ConfigurationManager.AppSettings["LdapListTimeout"];
        if( string.IsNullOrEmpty(configListTimeoutString) )
            return DefaultListTimeout;
        else
            return TimeSpan.Parse(configListTimeoutString);
    }
}