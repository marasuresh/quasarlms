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
using System.Data.Common;

public partial class Trainings_Tests_StudentResultsControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void answersDataSource_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        string debug = e.Command.CommandText;
        foreach( DbParameter p in e.Command.Parameters )
        {
            debug += "\r\n" + p.ParameterName + ":" + p.Value;
        }

        //throw new Exception(debug);
    }

    protected string CreateAnswerList(string answersXml)
    {
        return Server.UrlPathEncode(answersXml);
    }

    protected void answersGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
}
