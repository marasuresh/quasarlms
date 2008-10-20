using System;
using N2.Templates.Web.UI;
using N2.Web.UI.WebControls;

namespace N2.ACalendar.UI.Parts
{
	public partial class ACalendar_small : TemplateUserControl<ContentItem, N2.ACalendar.ACalendarSmall>
	{

		protected ItemDataSource idsNews;
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			var calendar_list = CurrentItem.ACalendarContainer.MyCalendars;
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