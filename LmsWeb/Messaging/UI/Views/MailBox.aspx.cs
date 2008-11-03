using System;
using System.Web;
using System.Web.UI.WebControls;
using N2.Messaging;
using N2.Resources;
using N2.Templates.Web.UI;
using N2.Web;

public partial class Messaging_UI_MailBox : TemplatePage<MailBox>
{
	protected string CurrentUserName
	{
		get { return HttpContext.Current.User.Identity.Name; }
	}

    protected MultiView mvMailBox;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        Register.StyleSheet(Page, "/Messaging/UI/Css/Messaging.css", Media.All);      
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        int msgContainerIndex;

		switch (this.CurrentItem.Folder) {
			case MailBox.C.Folders.Drafts:
				msgContainerIndex = 1;
				break;
			case MailBox.C.Folders.RecyleBin:
				msgContainerIndex = 2;
				break;
			case MailBox.C.Folders.Inbox:
				msgContainerIndex = 0;
				break;
			case MailBox.C.Folders.Outbox:
				msgContainerIndex = 0;
				break;
			default:
				msgContainerIndex = 0;
				break;
		}
		mvMailBox.ActiveViewIndex = msgContainerIndex;

    }
    
    protected void btnEmptyRecBin_Click(object sender, EventArgs e)
    {
        foreach (Message msg in this.CurrentItem.MessageStore.MyRecycled)
        {
            Engine.Persister.Delete(msg);
		}
		Response.Redirect(Url.Parse(CurrentItem.Url).AppendSegment("folder/" + MailBox.C.Folders.RecyleBin).Path);
        
    }

	protected void ds_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
	{
		e.ObjectInstance = this.CurrentItem;
	}

}
