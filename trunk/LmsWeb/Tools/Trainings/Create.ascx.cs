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

public partial class Trainings_Create : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        DateTime start = DateTime.Today.AddDays(7);
        DateTime end = start.AddDays(7);
		tbStartDate.Text = start.ToString();
		tbEndDate.Text = end.ToString();

        RegionEditControl1.RegionGuid = CurrentUser.Region.ID;

        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    Guid? HomeRegion
    {
        get { return CurrentUser.Region.ID; }
    }

    Guid? SelectedRegion
    {
        get { return (Guid?)RegionEditControl1.RegionGuid; }
    }

    Guid? SelectedCourse {
        get {
			if (string.IsNullOrEmpty(coursDropDownList.SelectedValue)) {
				return null;
			} else {
				return GuidService.Parse(coursDropDownList.SelectedValue);
			}
        }
    }

    public event EventHandler TrainingCreated;

    protected void createTrainingButton_Click(object sender, EventArgs e)
    {
        TrainingQueriesTableAdapters.StoredProcedures spAdapter = new TrainingQueriesTableAdapters.StoredProcedures();
        DateTime _dt;
		spAdapter.CreateTraining(
            HomeRegion,
            nameTextBox.Text,
            codeTextBox.Text,
            SelectedCourse,
			DateTime.TryParse(tbStartDate.Text, out _dt) ? _dt : default(DateTime?),
            DateTime.TryParse(tbEndDate.Text, out _dt) ? _dt : default(DateTime?),
            isPublicCheckBox.Checked,
            isActiveCheckBox.Checked,
            testOnlyCheckBox.Checked,
            expiresCheckBox.Checked,
            SelectedRegion);

        EventHandler temp = TrainingCreated;
        if( temp != null )
            temp(this,EventArgs.Empty);
    }
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}
