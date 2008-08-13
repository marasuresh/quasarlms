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

public partial class StudentReports_DebugDumpReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        using( SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Dce2005ConnectionString"].ConnectionString) )
        {
            using( SqlCommand cmd = new SqlCommand() )
            {
                cmd.Connection = conn;
                cmd.CommandText = "dbo.dcereports_Student_LoadAllRelatedTables";
                cmd.CommandType = CommandType.StoredProcedure;

                if( CurrentUser.IsAuthenticated )
                {
                    if( CurrentUser.Region.ID==null )
                        cmd.Parameters.Add("@homeRegion", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@homeRegion", SqlDbType.UniqueIdentifier).Value = CurrentUser.Region.ID;

                    cmd.Parameters.Add("@studentID", SqlDbType.UniqueIdentifier).Value = CurrentUser.UserID;
                }
                else
                {
                    cmd.Parameters.Add("@homeRegion", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                    cmd.Parameters.Add("@studentID", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                }

                if( !string.IsNullOrEmpty(Request.QueryString["all"]) )
                {
                    cmd.Parameters["@homeRegion"].Value = DBNull.Value;
                    cmd.Parameters["@studentID"].Value = DBNull.Value;
                }

                if( cmd.Parameters["@homeRegion"].Value == DBNull.Value )
                    detailsLabel.Text = "homeRegion IS NULL";
                else
                    detailsLabel.Text = "homeRegion = " + cmd.Parameters["@homeRegion"].Value;

                if( cmd.Parameters["@studentID"].Value == DBNull.Value )
                    detailsLabel.Text += " studentID IS NULL";
                else
                    detailsLabel.Text += " studentID = " + cmd.Parameters["@studentID"].Value;

                try
                {
                    using( SqlDataAdapter adap = new SqlDataAdapter(cmd) )
                    {
                        DataSet ds = new DataSet();
                        adap.Fill(ds);

                        tableCountLabel.Text = ds.Tables.Count.ToString();

                        PlaceHolder1.Controls.Clear();

                        foreach( DataTable tab in ds.Tables )
                        {
                            Label titleLabel = new Label();
                            titleLabel.Text = tab.TableName + "[" + tab.Rows.Count + "]";
                            PlaceHolder1.Controls.Add(titleLabel);

                            GridView grid = new GridView();
                            grid.EnableViewState = false;
                            grid.BorderStyle = BorderStyle.Solid;
                            grid.BorderWidth = 2;
                            grid.DataSource = tab;
                            PlaceHolder1.Controls.Add(grid);
                        }

                        PlaceHolder1.DataBind();
                    }
                }
                catch( SqlException err )
                {
                    throw new Exception(cmd.Parameters[0].ParameterName + " " + cmd.Parameters[0].SqlDbType + " " + cmd.Parameters[0].Value + " ||| " + err.Message);
                }
            }
        }
    }
}