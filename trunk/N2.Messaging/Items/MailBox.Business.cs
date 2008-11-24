using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace N2.Messaging
{
	using N2.Web;
	
	partial class MailBox: ILink
	{
		#region Validation
		
		IList<string> ValidationMessages = new List<string>();

		bool IsValid = false;

		void Validate()
		{
			this.IsValid = true;
			
			if (!(this is FakeMailBox) && null == this.MessageStore) {
				this.ValidationMessages.Add("Message store is not assigned");
				this.IsValid = false;
			}
		}

		#endregion Validation

		#region ILink Members

		string ILink.Contents { get { return this.Title; } }

		string ILink.Target { get { return string.Empty; } }

		string ILink.ToolTip {
			get {
				this.Validate();
				return string.Join(System.Environment.NewLine, this.ValidationMessages.ToArray());
			}
		}

		string ILink.Url { get { return this.Url; } }

		#endregion

		static IEnumerable<Message> GetFilteredMessages(IEnumerable<Message> list, string filter)
		{
			switch(filter) {
				case C.Filter.Tasks:
					return list.Where(_m => _m.MessageType == MessageTypeEnum.Task);
				case C.Filter.Letters:
					return list.Where(_m => _m.MessageType == MessageTypeEnum.Letter);
				case C.Filter.Announcements:
					return list.Where(_m => _m.MessageType == MessageTypeEnum.Announcement);
				default:
					return list;
			}
		}

		protected string CurrentUser
		{
			get { return HttpContext.Current.User.Identity.Name; }
		}
	}
}
