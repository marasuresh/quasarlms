using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using N2.Web.UI.WebControls;
using N2.Resources;
using N2.Templates;
using N2.Templates.Web.UI;
using N2.ACalendar;




namespace N2.ACalendar.UI.Parts
{
    public partial class ACalendar_small : TemplateUserControl<ContentItem, N2.ACalendar.ACalendarSmall>
    {

        protected ItemDataSource idsNews;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            var calendar_list = CurrentItem.ACalendarContainer.GetChildren();
            //calendar_list.FindLast() 


        }
    }
}

//namespace N2.Messaging.Messaging.UI.Parts
//{
//    public partial class NewMessageList : TemplateUserControl<ContentItem, N2.Messaging.NewMessageList>
//    {
//        protected ItemDataSource idsNews;

//        protected override void OnInit(EventArgs e)
//        {
//            base.OnInit(e);

//            Register.StyleSheet(Page, "/Messaging/UI/Css/Messaging.css", Media.All);

//            hlMailBox.NavigateUrl = CurrentItem.MailBox.Url;

//            idsNews.CurrentItem = CurrentItem.MessageStore;
//            idsNews.Filtering += idsNews_Filtering;
//        }

//        void idsNews_Filtering(object sender, N2.Collections.ItemListEventArgs e)
//        {
//            CurrentItem.Filter(e.Items);
//        }
//    }
//}



//namespace N2.Templates.UI.Parts
//{
//    public partial class BoxedText : Web.UI.TemplateUserControl<ContentItem, BoxedTextItem>
//    {
//    }
//}