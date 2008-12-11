using N2.Edit.Trash;
using N2.Persistence;

namespace N2.Messaging
{
    using N2.Integrity;
	using N2.Details;
	using N2.Templates.Items;
	using N2.Definitions;
	
	[Definition("Почта", "MailBox")]
    [NotThrowable, NotVersionable]
    [RestrictParents(typeof(IStructuralPage))]
	//[ItemAuthorizedRoles("Everyone")]
    public partial class MailBox : AbstractContentPage
	{
		public MailBox()
		{
			this.Title = "Почта";
		}

		#region System properties
		
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

		#endregion Lms properties
	}
}
