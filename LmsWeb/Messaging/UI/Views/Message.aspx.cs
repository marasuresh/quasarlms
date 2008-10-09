using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using N2.Web.UI;
using N2.Messaging;

public partial class Messaging_UI_Message : N2.Templates.Web.UI.TemplatePage<Message>
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        tbFrom.Text = CurrentItem.From;
        tbSubject.Text = CurrentItem.Subject;
        txtText.Text = CurrentItem.Text;

        string curUser = Context.User.Identity.Name;

        // Если сообщение просматривает пользователь, которому адресовано письмо... 
        if (CurrentItem.To == curUser)
        {
            //Отмечаем сообщение как прочтенное.
            CurrentItem.isRead = true;
            CurrentItem.Save();
        }
        
        
    }

    protected FormView fvMessage;
    protected N2.Web.UI.WebControls.ItemDataSource dsMessage;
    
    protected TextBox tbFrom;
    protected TextBox tbSubject;
    protected N2.Web.UI.WebControls.FreeTextArea txtText;
    

}
