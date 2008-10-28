using System.Collections.Generic;
using System.Linq;

namespace N2.Messaging
{
	using N2.Web;
	
	partial class MailBox: ILink
	{
		IList<string> ValidationMessages = new List<string>();

		bool IsValid = false;

		void Validate()
		{
			this.IsValid = true;
			
			if (null == this.MessageStore) {
				this.ValidationMessages.Add("Message store is not assigned");
				this.IsValid = false;
			}
		}

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
	}
}
