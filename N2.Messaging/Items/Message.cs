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

        public override string IconUrl { get { return "~/Lms/UI/Img/04/50.png"; } }
        
        public override string TemplateUrl { get { return "~/Messaging/UI/Message.aspx"; } }

        public override bool IsPage
        {
            get
            {
                return false;
            }
        }

        //public override bool IsAuthorized(System.Security.Principal.IPrincipal user)
        //{
        //    string userName = user.Identity.Name;
        //    bool userIsAuthorized = base.IsAuthorized(user);
        //    return true;
        //}

        [EditableTextBox("Отправитель", 5)]
        public string From
        {
            get { return GetDetail("From") as string; }
            set { SetDetail("From", value); }
        }

        [EditableTextBox("Получатель", 5)]
        public string To
        {
            get { return GetDetail("To") as string; }
            set { SetDetail("To", value); }
        }

        ////Тип сообщения.
        //[EditableEnum("Тип сообщения", 8, typeof(msgType))]
        //public msgType typeOfMessage
        //{
        //    get { return (msgType)(GetDetail("typeOfMessage") ?? msgType.letter); }
        //    set { SetDetail("typeOfMessage", value); }
        //}

        //Признак прочтения.
        [EditableCheckBox("Прочитано", 10)]
        public bool isRead
        {
            get { return (bool)(GetDetail("isRead") ?? false); }
            set { SetDetail("isRead", value); }
        }

        
        public string Subject
        {
            get { return Title; }
            set { Title = value; }
        }
        
        //[EditableFreeTextArea("Body", 1)]
        //public string Body
        //{
        //    get { return this.Text; }
        //    set { this.Text = value;}
        //}

        #endregion Properties
    }
}
