using System;
using System.Linq;
using N2.Collections;
using N2.Messaging;
using System.Web.UI.WebControls;
using System.Web;
using N2.Resources;
using N2.Web;

public partial class Messaging_UI_MailBox : N2.Templates.Web.UI.TemplatePage<MailBox>
{
	protected string CurrentUserName {
		get { return this.Context.User.Identity.Name; }
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
    
    

 //Все сообщения текущего пользователя.
    protected Message[] Messages
    {
        get
        {
            return (from child in CurrentItem.MessageStore.Children.OfType<Message>()
                    where ((string.Equals(child.Owner, CurrentUserName, StringComparison.OrdinalIgnoreCase))                        
                            )
                    select child).ToArray();
        }
    }
    
    
    //Письма.
    protected Letter[] Letters
    {
        get
        {
            return (from child in CurrentItem.MessageStore.Children.OfType<Letter>()
                    where (string.Equals(child.Owner, this.CurrentUserName, StringComparison.OrdinalIgnoreCase))
                    select child).ToArray();
        }
    }
    
    //Задания.
    protected Task[] Tasks
    {
        get
        {
            return (from child in CurrentItem.MessageStore.Children.OfType<Task>()
                    where (string.Equals(child.Owner, this.CurrentUserName, StringComparison.OrdinalIgnoreCase))
                    select child).ToArray();
        }
    }
    
    //Объявления.
    protected Announcement[] Announcements
    {
        get
        {
            return (from child in CurrentItem.MessageStore.Children.OfType<Announcement>()
                    where (string.Equals(child.Owner, this.CurrentUserName, StringComparison.OrdinalIgnoreCase))
                    select child).ToArray();
        }
    }



    //В черновиках.
    protected Message[] inDraughtStore
    {
        get
        {
            return (from child in CurrentItem.DraughtStore.Children.OfType<Message>()
                    where (string.Equals(child.Owner, this.CurrentUserName, StringComparison.OrdinalIgnoreCase))
                    select child).ToArray();
        }
    }

    //В корзине.
    protected Message[] inRecycleBin
    {
        get
        {
            return (from child in CurrentItem.RecycleBin.Children.OfType<Message>()
                    where (string.Equals(child.Owner, this.CurrentUserName, StringComparison.OrdinalIgnoreCase))
                    select child).ToArray();
        }
    }


    private void ShowMsgContainer(int tabIndex)
    {
        mvMailBox.ActiveViewIndex = tabIndex;

        Message[] showingMsg = null;

        switch (tabIndex)
        {
            case 0:

                //Фильтровка сообщений выводимых на экран.
                switch (ddlMsgType.SelectedValue)
                {
                    case "all":
                        showingMsg = Messages;
                        break;
                    case "letters":
                        showingMsg = Letters;
                        break;
                    case "tasks":
                        showingMsg = Tasks;
                        break;
                    case "announcements":
                        showingMsg = Announcements;
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
                showingMsg = inDraughtStore;
                break;

            case 2:
                showingMsg = inRecycleBin;
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
        foreach (Message msg in inRecycleBin)
        {
            Engine.Persister.Delete(msg);
            Response.Redirect(Url.Parse(CurrentItem.Url).AppendSegment("RecycleBin").Path);
        }
    }
}
