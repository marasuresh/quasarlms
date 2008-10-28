using N2.Templates.Items;

namespace N2.Messaging
{
	using N2.Definitions;
	using N2.Details;
	using N2.Edit.Trash;
	using N2.Integrity;
	using N2.Persistence;

	[Definition(
		"Message Store",
		"MessageStore",
		"A list of message. Message items can be added to this page.",
		"",
		150)]
	[WithEditableTitle("Title", 10)]
	[NotThrowable, NotVersionable]
	[AllowedChildren(typeof(Message), typeof(DraughtStore), typeof(RecycleBin))]
	public partial class MessageStore : BaseStore
	{
		public MessageStore()
		{
			this.Name = "MessageStore" + this.ID;
			this.Title = "Хранилище почты";
		}

		public override string IconUrl { get { return "~/Messaging/UI/Images/database.png"; } }
	}
}
