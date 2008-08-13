using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public class GuidEventArgs : EventArgs
{
    readonly Guid m_Guid;

    public GuidEventArgs(Guid guid)
    {
        this.m_Guid = guid;
    }

    public Guid Guid
    {
        get { return m_Guid; }
    }
}