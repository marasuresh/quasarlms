using N2.Details;
using N2.Integrity;
using N2.Templates;
using N2.Templates.Items;

namespace N2.ACalendar
{
	using N2.Lms;
	using N2.Web;
	
	[Definition("ACalendarSmall", "ACalendarSmall", "Маленький календарь", "", 160)]
	[WithEditableTitle("Title", 10, Required = false)]
	[AllowedZones(Zones.RecursiveRight, Zones.Right, Zones.SiteRight)]
	[RestrictParents(typeof(IStructuralPage))]
	public class ACalendarSmall : SidebarItem, ILink
	{
		#region System properties
		
		public override string TemplateUrl { get { return "~/ACalendar/UI/Parts/ACalendar_small.ascx"; } }

		public override string IconUrl { get { return this.ACalendarContainer == null ? Lms.Icons.Error : Icons.Calendar2; } }

		public override bool IsPage { get { return false; } }

		#endregion System properties

		#region Lms properties

		[EditableTextBox("Max messages", 120)]
		public int MaxNews
		{
			get { return (int)(GetDetail("MaxMessages") ?? 3); }
			set { SetDetail("MaxMessages", value, 3); }
		}

		[EditableLink("ACalendar Container", 1,
			HelpTitle = "Select an item, which contains all calendars.",
			Required = true)]
		public ACalendarContainer ACalendarContainer
		{
			get { return this.GetDetail("ACalendarContainer") as ACalendarContainer; }
			set { this.SetDetail<ACalendarContainer>("ACalendarContainer", value); }
		}
		
		#endregion Lms properties

		#region ILink Members

		string ILink.Contents { get { return this.Title; } }

		string ILink.Target { get { return string.Empty; } }

		string ILink.ToolTip {
			get { return this.ACalendarContainer == null
					? "ACalendar Container is not set"
					: this.IconUrl;
			}
		}

		string ILink.Url { get { return this.Url; } }

		#endregion
	}
}
//namespace N2.Messaging
//{
//    [Definition("NewMessage List", "NewMessageList", "Список новых сообщений.", "", 160)]
//    [WithEditableTitle("Title", 10, Required = false)]
//    [AllowedZones( Zones.RecursiveRight, Zones.Right, Zones.SiteRight)]
//    [RestrictParents(typeof(IStructuralPage))]
//    public class NewMessageList : SidebarItem
//    {
//        public override string TemplateUrl { get { return "~/Messaging/UI/Parts/NewMessageList.ascx"; } }

//        //public enum HeadingLevel
//        //{
//        //    One = 1,
//        //    Two = 2,
//        //    Three = 3,
//        //    Four = 4
//        //}

//        //[EditableEnum("Title heading level", 90, typeof(HeadingLevel))]
//        //public virtual int TitleLevel
//        //{
//        //    get { return (int)(GetDetail("TitleLevel") ?? 3); }
//        //    set { SetDetail("TitleLevel", value, 3); }
//        //}

//        [EditableLink("Message Store", 150, HelpTitle = "Select an item, which contains all messages.", Required = true)]
//        public MessageStore MessageStore
//        {
//            get { return GetDetail("MessageStore") as MessageStore; }
//            set { SetDetail("MessageStore", value); }
//        }

//        [EditableLink("Mail Box", 150, HelpTitle = "Select an item, which show all messages.", Required = true)]
//        public MailBox MailBox
//        {
//            get { return GetDetail("MailBox") as MailBox; }
//            set { SetDetail("MailBox", value); }
//        }

//        [EditableTextBox("Max messages", 120)]
//        public virtual int MaxNews
//        {
//            get { return (int)(GetDetail("MaxMessages") ?? 3); }
//            set { SetDetail("MaxMessages", value, 3); }
//        }

//        public virtual void Filter(ItemList items)
//        {
//            TypeFilter.Filter(items, typeof(Message));
//            UserNewMsgFilter.FilterMsg(items);
//            CountFilter.Filter(items, 0, MaxNews);
//        }
//    }
//}
