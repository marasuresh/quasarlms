


using N2.Edit.Trash;
using N2.Persistence;

namespace N2.Messaging
{
    using N2.Integrity;
    using N2.Definitions;
    using N2.Details;
    using N2.Templates;
    using N2.Templates.Items;

    [Definition("Почта","MailBox")]
    [NotThrowable, NotVersionable]
    [RestrictParents(typeof(IStructuralPage))]
    public class MailBox : AbstractContentPage
    {
        public override string TemplateUrl { get { return "~/Messaging/UI/MailBox.aspx"; } }

        [EditableLink("Message Store", 1, HelpTitle="Select an item, which contains all messages.")]
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
    }
}
