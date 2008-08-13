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

public partial class Forums_NewMessageControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public event EventHandler SendClick;

    public string Title
    {
        get { return topicTitleTextBox.Text; }
        set { topicTitleTextBox.Text = value; }
    }

    public string Message
    {
        get { return messageTextBox.Text; }
        set { messageTextBox.Text = value; }
    }

    protected void sendButton_Click(object sender, EventArgs e)
    {
        EventHandler temp = SendClick;
		if (temp != null) {
			temp(this, EventArgs.Empty);
		}

        this.Title = "";
        this.Message = "";
        //this.Page.DataBind();
    }
}
