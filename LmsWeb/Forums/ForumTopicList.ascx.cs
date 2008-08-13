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

public partial class Forums_ForumTopicList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public event EventHandler<GuidEventArgs> TopicClick;

    public int PageSize
    {
        get { return topicListGridView.PageSize; }
        set {
			topicListGridView.PageSize = value;
		}
    }

	const string VS_TRAINING_ID = "TrainingId";
	public Guid? TrainingId {
		get { return (Guid?)this.ViewState[VS_TRAINING_ID]; }
		set {
			this.ViewState[VS_TRAINING_ID] = value;
			this.topicListGridView.DataBind();
		}
	}

    protected void topicListGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if( e.CommandName != "ShowTopic" )
            return;

        Guid topicID = GridViewHelpers.GetKeyByCommandArgument(
            e.CommandArgument,
            topicListGridView);


        EventHandler<GuidEventArgs> temp = TopicClick;
        if( temp != null )
            temp(this, new GuidEventArgs(topicID));
    }

    protected void ForumThreadControl1_MessageCreated(object sender, EventArgs e)
    {
        topicListGridView.PageIndex = 0;
        topicListGridView.EditIndex = 0;
    }
	protected void TopicsDataSource_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
	{
		e.Command.Parameters["@trainingID"].Value = this.TrainingId;
	}
}
