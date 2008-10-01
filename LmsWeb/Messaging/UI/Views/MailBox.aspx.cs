using System;
using System.Linq;
using N2.Collections;
using N2.Messaging;

public partial class Messaging_UI_MailBox : N2.Templates.Web.UI.TemplatePage<MailBox>
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Message[] showingMsg = null;

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
                showingMsg = msgFilter.GetIncomingMsg(showingMsg, Profile.UserName);
                break;
            case "sent":
                showingMsg = msgFilter.GetSentMsg(showingMsg, Profile.UserName);
                break;
        }

        if (cboxOnlyNew.Checked) showingMsg = msgFilter.GetNotReadMsg(showingMsg);

        //Вывод отфильтрованных сообщений на экран.
        gvMailBox.DataSource = showingMsg;
        gvMailBox.DataBind();
    }

    
    //Все сообщения текущего пользователя.
    protected Message[] Messages
    {
        get
        {
            return (from child in CurrentItem.MessageStore.Children.OfType<Message>()
                    where ((string.Equals(child.From, Profile.UserName, StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(child.To, Profile.UserName, StringComparison.OrdinalIgnoreCase))                        
                            )
                    select child).ToArray();
            //ItemList allUserMessages = (CurrentItem.MessageStore.GetChildren());
            //return allUserMessages.ToArray() as Message[];
        }
    }
    
    //Письма.
    protected Letter[] Letters
    {
        get
        {
            return (from child in CurrentItem.MessageStore.Children.OfType<Letter>()
                    where (string.Equals(child.From, Profile.UserName, StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(child.To, Profile.UserName, StringComparison.OrdinalIgnoreCase))
                    select child).ToArray();
        }
    }
    
    //Задания.
    protected Task[] Tasks
    {
        get
        {
            return (from child in CurrentItem.MessageStore.Children.OfType<Task>()
                    where (string.Equals(child.From, Profile.UserName, StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(child.To, Profile.UserName, StringComparison.OrdinalIgnoreCase))
                    select child).ToArray();
        }
    }
    
    //Объявления.
    protected Announcement[] Announcements
    {
        get
        {
            return (from child in CurrentItem.MessageStore.Children.OfType<Announcement>()
                    where (string.Equals(child.From, Profile.UserName, StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(child.To, Profile.UserName, StringComparison.OrdinalIgnoreCase))
                    select child).ToArray();
        }
    }




    //В черновиках.
    protected Message[] inDraughtStore
    {
        get
        {
            return (from child in CurrentItem.DraughtStore.Children.OfType<Message>()
                    where ((string.Equals(child.From, Profile.UserName, StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(child.To, Profile.UserName, StringComparison.OrdinalIgnoreCase)))
                    select child).ToArray();
        }
    }

    //В корзине.
    protected Message[] inRecycleBin
    {
        get
        {
            return (from child in CurrentItem.RecycleBin.Children.OfType<Message>()
                    where ((string.Equals(child.From, Profile.UserName, StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(child.To, Profile.UserName, StringComparison.OrdinalIgnoreCase)))
                    select child).ToArray();
        }
    }
}
