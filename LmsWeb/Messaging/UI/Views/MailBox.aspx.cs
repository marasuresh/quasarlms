using System;
using System.Collections.Generic;
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
    protected ListView lv;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        Register.StyleSheet(Page, "~/Messaging/UI/Css/Messaging.css", Media.All);
        
        lv.DataBound += new EventHandler(lv_DataBound);
    }

    void lv_DataBound(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            int _msg;
            if (int.TryParse(this.Request["msg"], out _msg))
            {
                int i = 0;
                foreach (DataKey dKey in lv.DataKeys)
                {

                    if ((int)dKey.Value == _msg)
                    {
                        this.lv.EditIndex = i;
                        
                    }
                    i++;
                }
            }

        }
        int editIndex = ((ListView)sender).EditIndex;

        if (editIndex > -1)
        {
            int selID = (int)((ListView)sender).DataKeys[editIndex].Value;



            var msg = N2.Context.Persister.Get<Message>(selID);

            //Помечаем сообщение как прочтенное.
            if (!msg.IsRead)
            {
                msg.IsRead = true;
                msg.Save();
                this.Page.DataBind();
            }
        }
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
        var delMsgs = new Dictionary<int,Message>();
        int index = 0;

        foreach (Message msg in this.CurrentItem.MessageStore.MyRecycled)
        {
            delMsgs.Add(index, msg);
            index++;
            
		}

        for (int i = 0; i < delMsgs.Count; i++ )
        {
            Message msg;
            delMsgs.TryGetValue(i, out msg);
            Engine.Persister.Delete(msg);
        }

		Response.Redirect(Url.Parse(CurrentItem.Url).AppendSegment("folder/" + MailBox.C.Folders.RecyleBin).Path);
        
    }

	protected void ds_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
	{
		e.ObjectInstance = this.CurrentItem;
	}

}
