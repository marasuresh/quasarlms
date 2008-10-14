using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using N2.Resources;
using N2.Templates;
using N2.Templates.Web.UI;
using N2.Web.UI.WebControls;

namespace N2.Messaging.Messaging.UI.Parts
{
    public partial class NewMessageList : TemplateUserControl<ContentItem, N2.Messaging.MailBox>
    {
        protected ItemDataSource idsNews;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            
            Register.StyleSheet(Page, "/Messaging/UI/Css/Messaging.css", Media.All);

            hlMailBox.NavigateUrl = CurrentItem.MailBox.Url;

            idsNews.CurrentItem = CurrentItem.MessageStore;
            idsNews.Filtering += idsNews_Filtering;
        }

        void idsNews_Filtering(object sender, N2.Collections.ItemListEventArgs e)
        {
            CurrentItem.Filter(e.Items);
        }
    }
}