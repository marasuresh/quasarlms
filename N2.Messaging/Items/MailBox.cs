using N2.Edit.Trash;
using N2.Persistence;

namespace N2.Messaging
{
    using N2.Integrity;
    using N2.Definitions;
    using N2.Details;
    using N2.Templates;
    using N2.Templates.Items;

    [Definition("Почта", "MailBox")]
    [NotThrowable, NotVersionable]
    [RestrictParents(typeof(IStructuralPage))]
    public partial class MailBox : AbstractContentPage
	{
		#region System properties
		
		public override string TemplateUrl {
            get { return string.Concat("~/Messaging/UI/Views/",
					!string.IsNullOrEmpty(this.Action)
						&& "newletter,newtask,newannouncement".Contains(this.Action.ToLower())
						? "NewMessage" : "MailBox",
					".aspx");
			}
		}

		public override string IconUrl {
			get {
				this.Validate();
				return string.Concat(
					"~/Messaging/UI/Images/",
					this.IsValid ? "email.png" : "email_error.png");
			}
		}

		#endregion System properties

		#region Lms properties
		
		[EditableLink("Message Store", 1,
			HelpTitle = "Select an item, which contains all messages.",
			Required = true)]
        public MessageStore MessageStore
        {
            get { return this.GetDetail("MessageStore") as MessageStore; }
            set { this.SetDetail<MessageStore>("MessageStore", value); }
        }

        [EditableLink("Recycle Bin", 1,
			HelpTitle = "Select an item, which contains all delete messages.",
			Required = true)]
        public RecycleBin RecycleBin
        {
            get { return this.GetDetail("RecycleBin") as RecycleBin; }
            set { this.SetDetail<RecycleBin>("RecycleBin", value); }
        }

        [EditableLink("Draught Store", 1,
			HelpTitle = "Select an item, which contains all draught messages.",
			Required = true)]
        public DraughtStore DraughtStore
        {
            get { return this.GetDetail("DraughtStore") as DraughtStore; }
            set { this.SetDetail<DraughtStore>("DraughtStore", value); }
		}

		#endregion Lms properties

		public string Action { get; set; }

        public override ContentItem GetChild(string childName)
        {
            if(string.Equals(childName, "newLetter", System.StringComparison.OrdinalIgnoreCase))
            {
                this.Action = "newLetter";
                return this;
            }
            else if (string.Equals(childName, "newAnnouncement", System.StringComparison.OrdinalIgnoreCase))
            {
                this.Action = "newAnnouncement";
                return this;
            }
            else if (string.Equals(childName, "newTask", System.StringComparison.OrdinalIgnoreCase))
            {
                this.Action = "newTask";
                return this;
            }
            else if(string.Equals(childName, "inbox", System.StringComparison.OrdinalIgnoreCase))
            {
                this.Action = "inbox";
                return this;
            }
            else if (string.Equals(childName, "MessageStore", System.StringComparison.OrdinalIgnoreCase))
            {
                this.Action = "MessageStore";
                return this;
            }
            else if (string.Equals(childName, "DraughtStore", System.StringComparison.OrdinalIgnoreCase))
            {
                this.Action = "DraughtStore";
                return this;
            }
            else if (string.Equals(childName, "RecycleBin", System.StringComparison.OrdinalIgnoreCase))
            {
                this.Action = "RecycleBin";
                return this;
            }
            else
            {
                this.Action = null;
            }
            return base.GetChild(childName);
        }
    }
}
