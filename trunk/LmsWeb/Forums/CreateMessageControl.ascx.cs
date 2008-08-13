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

public partial class Forums_CreateMessageControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

	const string VS_TOPIC_ID = "TopicId";
	public Guid? TopicId {
		get { return (Guid?)this.ViewState[VS_TOPIC_ID]; }
		set {
			this.ViewState[VS_TOPIC_ID] = value;
		}
	}

	const string VS_TRAINING_ID = "TrainingId";
	public Guid? TrainingId {
		get { return (Guid?)this.ViewState[VS_TRAINING_ID]; }
		set {
			this.ViewState[VS_TRAINING_ID] = value;
		}
	}
	
	public event EventHandler MessageCreated;

    protected void sendButton_Click(object sender, EventArgs e)
    {
        TrainingQueriesTableAdapters.StoredProcedures spAdapter = new TrainingQueriesTableAdapters.StoredProcedures();
        spAdapter.PostMessage(
            CurrentUser.Region.ID,
            this.TrainingId,
            this.TopicId,
            CurrentUser.UserID,
            messageTextBox.Text);
        
		messageTextBox.Text = null;

        //Page.DataBind();

        EventHandler temp = MessageCreated;
		if (temp != null) {
			temp(this, EventArgs.Empty);
		}
    }
}