


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
    public class MailBox : AbstractContentPage
    {
        public override string TemplateUrl
        {
            get { return string.Equals(Action, "newLetter", System.StringComparison.OrdinalIgnoreCase) ||
                         string.Equals(Action, "newTask", System.StringComparison.OrdinalIgnoreCase) ||
                         string.Equals(Action, "newAnnouncement", System.StringComparison.OrdinalIgnoreCase)
            ? "~/Messaging/UI/Views/NewMessage.aspx"
            : "~/Messaging/UI/Views/MailBox.aspx"; } }

        [EditableLink("Message Store", 1, HelpTitle = "Select an item, which contains all messages.")]
        public MessageStore MessageStore
        {
            get { return this.GetDetail("MessageStore") as MessageStore; }
            set { this.SetDetail<MessageStore>("MessageStore", value); }
        }

        [EditableLink("Recycle Bin", 1, HelpTitle = "Select an item, which contains all delete messages.")]
        public RecycleBin RecycleBin
        {
            get { return this.GetDetail("RecycleBin") as RecycleBin; }
            set { this.SetDetail<RecycleBin>("RecycleBin", value); }
        }

        [EditableLink("Draught Store", 1, HelpTitle = "Select an item, which contains all draught messages.")]
        public DraughtStore DraughtStore
        {
            get { return this.GetDetail("DraughtStore") as DraughtStore; }
            set { this.SetDetail<DraughtStore>("DraughtStore", value); }
        }

        
        
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
