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

public partial class Forums_CreateTopicControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

	const string VS_TRAINING_ID = "TrainingId";
	public Guid? TrainingId {
		get { return (Guid?)this.ViewState[VS_TRAINING_ID]; }
		set {
			this.ViewState[VS_TRAINING_ID] = value;
		}
	}

    protected void newMessageControl_Click(object sender, EventArgs e)
    {
        NewsQueriesTableAdapters.QueriesTableAdapter spAdapter = new NewsQueriesTableAdapters.QueriesTableAdapter();
        spAdapter.dcetools_Trainings_Forum_CreateTopic(
            CurrentUser.Region.ID,
            this.TrainingId,
            newMessageControl.Title,
            newMessageControl.Message,
            CurrentUser.UserID);
        newMessageControl.Title = "";
        newMessageControl.Message = "";
    }
}
