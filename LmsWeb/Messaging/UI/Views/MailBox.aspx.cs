using System;
using System.Linq;
using N2.Collections;
using N2.Messaging;
using System.Web.UI.WebControls;
using System.Web;
using N2.Resources;
using N2.Web;
using N2.Templates.Web.UI;
using System.Collections;
using System.Collections.Generic;

public partial class Messaging_UI_MailBox : TemplatePage<MailBox>
{
	protected string CurrentUserName
	{
		get { return HttpContext.Current.User.Identity.Name; }
	}

    protected DropDownList ddlMsgType;
    protected DropDownList ddlDirection;
    protected CheckBox cboxOnlyNew;
    protected GridView gvMailBox;
    protected Menu mnuMailBox;
    protected MultiView mvMailBox;
    protected View Tab1;
    protected View Tab2;
    protected View Tab3;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        Register.StyleSheet(Page, "/Messaging/UI/Css/Messaging.css", Media.All);      

        if (CurrentItem.Action == "inbox")
        {
            ddlDirection.SelectedIndex = 2;    //Отобразить входящие сообщения.
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        int msgContainerIndex;

        if (CurrentItem.Action == "DraughtStore")
            msgContainerIndex = 1;
        else if (CurrentItem.Action == "RecycleBin")
            msgContainerIndex = 2;
        else
            msgContainerIndex = 0;
        
        ShowMsgContainer(msgContainerIndex);
    }
    
    private void ShowMsgContainer(int tabIndex)
    {
        mvMailBox.ActiveViewIndex = tabIndex;

        IEnumerable<Message> showingMsg = new Message[0];

        switch (tabIndex)
        {
            case 0:

                //Фильтровка сообщений выводимых на экран.
                switch (ddlMsgType.SelectedValue)
                {
                    case "all":
						showingMsg = this.CurrentItem.MessageStore.MyMessages;
                        break;
                    case "letters":
						showingMsg = (IEnumerable<Message>)this.CurrentItem.MessageStore.MyLetters;
                        break;
                    case "tasks":
						showingMsg = (IEnumerable<Message>)this.CurrentItem.MessageStore.MyTasks;
                        break;
                    case "announcements":
						showingMsg = (IEnumerable<Message>)this.CurrentItem.MessageStore.MyAnnouncements;
                        break;
                }

                switch (ddlDirection.SelectedValue)
                {
                    case "incoming":
                        showingMsg = msgFilter.GetIncomingMsg(showingMsg, this.CurrentUserName);
                        break;
                    case "sent":
                        showingMsg = msgFilter.GetSentMsg(showingMsg, this.CurrentUserName);
                        break;
                }

                if (cboxOnlyNew.Checked) showingMsg = msgFilter.GetNotReadMsg(showingMsg);

                break;
            
            case 1:
                showingMsg = this.CurrentItem.MessageStore.MyDrafts;
                break;

            case 2:
                showingMsg = this.CurrentItem.MessageStore.MyRecycled;
                break;
        }

        //Вывод отфильтрованных сообщений на экран.
        gvMailBox.DataSource = showingMsg;
        gvMailBox.DataBind();
    }


    protected void mnuMailBox_MenuItemClick(object sender, MenuEventArgs e)
    {
        Int32 tabIndex = Int32.Parse(e.Item.Value);

        string msgContainer;

        if (tabIndex == 1)
            msgContainer = "DraughtStore";
        else if (tabIndex == 2)
            msgContainer = "RecycleBin";
        else
            msgContainer = "MessageStore";

        Response.Redirect(Url.Parse(CurrentItem.Url).AppendSegment(msgContainer).Path);
    }
    
    protected void btnEmptyRecBin_Click(object sender, EventArgs e)
    {
        foreach (Message msg in this.CurrentItem.MessageStore.MyRecycled)
        {
            Engine.Persister.Delete(msg);
            Response.Redirect(Url.Parse(CurrentItem.Url).AppendSegment("RecycleBin").Path);
        }
    }
}
