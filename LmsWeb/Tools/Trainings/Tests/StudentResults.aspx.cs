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

public partial class Trainings_Tests_StudentResults : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void allowRetryButton_Click(object sender, EventArgs e)
    {

    }

    protected void allowRetryButton_Click1(object sender, EventArgs e)
    {
		Guid testResultID;
		
		if(GuidService.TryParse(Request.QueryString["test"], out testResultID)) {
			new TrainingQueriesTableAdapters.StoredProcedures().AllowRetryTest(
			CurrentUser.Region.ID,
			testResultID);

			Response.Redirect("Default.aspx?id="+PageParameters.ID);
		}
	}
}
