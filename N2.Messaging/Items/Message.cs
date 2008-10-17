namespace N2.Messaging
{
    using N2.Persistence;
    using N2.Details;
    using N2.Integrity;
    using N2.Edit.Trash;
    using N2.Templates.Items;

    public enum msgType
    {
        letter,
        task,
        announcement
    } ;

    [Definition("Сообщение","Message")]
    [RestrictParents(typeof(MessageStore),typeof(RecycleBin),typeof(DraughtStore))]
    [NotThrowable, NotVersionable]
    public abstract class Message : AbstractContentPage
    {
        #region Properties

        //public override string IconUrl { get { return "~/Lms/UI/Img/04/50.png"; } }

        public override string TemplateUrl { get { return "~/Messaging/UI/Views/Message.aspx"; } }


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
        public bool isRead
        {
            get { return (bool)(GetDetail("isRead") ?? false); }
            set { SetDetail("isRead", value); }
        }

        public string[] Attachments
        {
            get { return GetDetail("Attachments") as string[]; }
            set { SetDetail("Attachments", value); }
        }

        public virtual string TypeOfMessage
        {
            get { return ""; }
        }

        public MailBox mailBox
        {
            get { return GetDetail("mailBox") as MailBox; }
            set { SetDetail("mailBox", value); }
        }
        

        #endregion Properties

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

        public override bool Visible
        {
            get
            {
                return false;
            }
            set
            {
                base.Visible = value;
            }
        }

        #endregion
    }
}
