using System.Collections;
using System.Linq;

namespace N2.Messaging
{
    using N2.Persistence;
    using N2.Details;
    using N2.Integrity;
    using N2.Edit.Trash;
    using N2.Templates.Items;

    public enum MessageTypeEnum
    {
        Letter,
        Task,
        Announcement
    }

    [Definition("Сообщение","Message")]
    [RestrictParents(typeof(MessageStore),typeof(RecycleBin),typeof(DraughtStore))]
    [NotThrowable, NotVersionable]
    public class Message : AbstractContentPage
	{
		#region Properties

		public override string IconUrl { get {
			switch(this.MessageType) {
				default:
					return "~/Messaging/UI/Images/email_open.png";
				case MessageTypeEnum.Announcement:
					return "~/Messaging/UI/Images/bell.png";
				case MessageTypeEnum.Task:
					return "~/Messaging/UI/Images/wrench.png"; } } }

		public override bool IsPage { get { return false; } }

        [EditableTextBox("Отправитель", 1)]
        public string From
        {
            get { return GetDetail("From") as string; }
            set { SetDetail("From", value); }
        }

        [EditableTextBox("Получатель", 2)]
        public string To
        {
            get { return GetDetail("To") as string; }
            set { SetDetail("To", value); }
        }

        [EditableTextBox("Владелец сообщения", 2)]
        public string Owner
        {
            get { return GetDetail("Owner") as string; }
            set { SetDetail("Owner", value); }
        }

        [EditableTextBox("Тема", 3)]
        public string Subject
        {
            get { return GetDetail("Subject") as string; }
            set { SetDetail("Subject", value); }
        }

        //Признак прочтения.
        [EditableCheckBox("Прочитано", 10)]
        public bool IsRead
        {
            get { return (bool)(GetDetail("isRead") ?? false); }
            set { SetDetail("isRead", value); }
        }

        public ArrayList Attachments
        {
            get { return GetDetail("Attachments") as ArrayList; }
            set { SetDetail("Attachments", value); }
        }

        public MessageTypeEnum MessageType
        {
            get { return this.GetDetail<MessageTypeEnum>("MessageType", MessageTypeEnum.Letter); }
			set { this.SetDetail<MessageTypeEnum>("MessageType", value); }
        }

        #endregion Properties

		MessageStore m_store;
		public MessageStore Store {
			get {
				return
					this.m_store ?? (
						this.m_store = this.Parent.Parent as MessageStore //speed up for modules
						?? this.Parent as MessageStore
						?? N2.Find.EnumerateParents(this)
							.OfType<MessageStore>()
							.FirstOrDefault());
			}
		}

        #region Methods

        public void Save()
        {
            Context.Persister.Save(this);
        }

        //Срабатывает при создании сообщения. Фильтрует сообщения в обратном порядке.
        public override void AddTo(ContentItem newParent)
        {
            Utility.Insert(this, newParent, "Published DESC");
        }

        public override bool Visible { get { return false; } }

        #endregion
    }
}
