using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public class DceUser
{
    public Guid ID;
    public string Login;

    public string FirstName;
    public string Patronymic;
    public string LastName;

    public string EMail;

    public string JobPosition;

    public string Comments;

    public DateTime CreateDate;
    public DateTime LastModify;

    public Guid? RegionID;
    public string RegionName;

    public Guid? RoleID;
    public string RoleName;
}