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

public partial class Trainings_TrainingDetails : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public bool ReadOnly
    {
        get { return !TrainingDetailsView.Fields[TrainingDetailsView.Fields.Count - 1].Visible; }
        set { TrainingDetailsView.Fields[TrainingDetailsView.Fields.Count - 1].Visible = !value; }
    }

    protected void TrainingDetailsDataSource_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {
        string parameterDump = null;
        foreach( SqlParameter p in e.Command.Parameters )
        {
            parameterDump += p.ParameterName + "=" + p.Value + "\r\n";
        }

        //throw new Exception(parameterDump);
    }
}
