using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public static class GuidListHelpers
{
    public static Guid[] GetMultiUserList()
    {
        return (Guid[])HttpContext.Current.Session[HttpContext.Current.Request["list"]];
    }
}
