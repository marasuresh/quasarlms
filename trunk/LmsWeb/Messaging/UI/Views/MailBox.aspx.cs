using System;
using System.Web;
using System.Web.UI.WebControls;
using N2.Messaging;
using N2.Resources;
using N2.Templates.Web.UI;
using N2.Web;
using System.Linq;

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
		Register.StyleSheet(this.Page, "~/Lms/UI/Css/MyAssignmentList.css");
        Register.JQuery(this.Page);

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
        int subMenuIndex;
        var _folder = string.IsNullOrEmpty(Engine.RequestContext.CurrentPath.Argument) ? 
                      MailBox.C.Folders.Inbox : Engine.RequestContext.CurrentPath.Argument.Split('/')[0];

		switch (_folder) {
			case MailBox.C.Folders.Drafts:
                subMenuIndex = 1;
                msgContainerIndex = 1;
				break;
			case MailBox.C.Folders.RecyleBin:
                subMenuIndex = 2;
                msgContainerIndex = 2;
				break;
			case MailBox.C.Folders.Inbox:
		        subMenuIndex = 0;
				msgContainerIndex = 0;
				break;
			case MailBox.C.Folders.Outbox:
                subMenuIndex = 3;
				msgContainerIndex = 0;
				break;
			default:
                subMenuIndex = 0;
				msgContainerIndex = 0;
				break;
		}
		mvMailBox.ActiveViewIndex = msgContainerIndex;
        Register.JavaScript(Page, @"$(document).ready(function() {
                                        $('ul.topMenu li.current ul a').eq(" + subMenuIndex + @")
                                            .css('text-decoration','underline')
                                            .css('color', '#8996A0');});", ScriptOptions.ScriptTags);
    }
    
    protected void btnEmptyRecBin_Click(object sender, EventArgs e)
    {
        var delMessages = this.CurrentItem.MessageStore.MyRecycled.ToArray();
        foreach (Message msg in delMessages)
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
