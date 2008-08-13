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
using System.Data.Common;

public partial class Trainings_Forum_TopicList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public int PageSize
    {
        get { return TopicsGridView.PageSize; }
        set { TopicsGridView.PageSize = value; }
    }
    protected void TopicsDataSource_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        //e.Command.Connection.Open();
        //int count = 0;
        //using( DbDataReader reader = e.Command.ExecuteReader() )
        //{
        //    while( reader.Read() )
        //    {
        //        count++;
        //    }
        //}
        //e.Command.Connection.Close();

        //throw new Exception(count+" "+ e.Command.Connection.State+" "+ e.Command.CommandText + "[" + e.Command.Parameters.Count + "] "+e.Command.Parameters[0].Value+", "+e.Command.Parameters[1].Value);
    }
    protected void TopicsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if( e.CommandName != "ShowTopic" )
            return;

        Guid? topicID;
        if( e.CommandArgument is Guid ) {
            topicID = (Guid)e.CommandArgument;
        } else {
            topicID = GuidService.Parse((string)e.CommandArgument);
        }
        
        Response.Redirect("Topic.aspx?id=" + PageParameters.ID + "&topic=" + topicID);
    }
}
