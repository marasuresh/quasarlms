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

public partial class Trainings_TrainingLink : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
		Guid? trainingID = PageParameters.ID;

        trainingHyperLink.NavigateUrl = "~/Tools/Trainings/Details.aspx?id=" + trainingID;

        TrainingQueriesTableAdapters.StoredProcedures spAdapter = new TrainingQueriesTableAdapters.StoredProcedures();
        trainingHyperLink.Text = spAdapter.GetTrainingName(trainingID.Value);
    }

    public string NavigateUrl
    {
        get { return trainingHyperLink.NavigateUrl; }
    }
}
