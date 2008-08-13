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

public partial class Forums_ForumTopicMessageListControl : System.Web.UI.UserControl
{
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

    public int PageSize
    {
        get { return threadMessageListGridView.PageSize; }
        set { threadMessageListGridView.PageSize = value; }
    }
	
	protected void MessageListDataSource_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
	{
		e.Command.Parameters["@topicID"].Value = this.TopicId;
		e.Command.Parameters["@trainingID"].Value = this.TrainingId;
	}
}