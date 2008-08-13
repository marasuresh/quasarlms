using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class News_NewsDetails : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool canWrite = new TrainingQueriesTableAdapters.dcetools_Access_GetCanObjectID_WriteTableAdapter().GetData(
            CurrentUser.Region.ID,
            PageParameters.ID)[0].Column1;

        if( !canWrite )
        {
            NewsDetailsView.Fields[NewsDetailsView.Fields.Count - 1].Visible = false;
        }
    }

    public bool AllowEdit
    {
        get { return NewsDetailsView.Fields[NewsDetailsView.Fields.Count-1].Visible; }
        set { NewsDetailsView.Fields[NewsDetailsView.Fields.Count-1].Visible = value; }
    }

    protected void NewsDetailsDataSource_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {
        string parameterDump=null;
        foreach( SqlParameter p in e.Command.Parameters )
        {
            parameterDump += p.ParameterName + "=" + p.Value + "\r\n";
        }

        //throw new Exception(parameterDump);
    }
    protected void regionsDropDownList_DataBinding(object sender, EventArgs e)
    {

    }
    protected void removeButton_Click(object sender, EventArgs e)
    {

    }
}
