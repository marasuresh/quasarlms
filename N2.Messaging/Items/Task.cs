using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2.Edit.Settings;
using N2.Edit.Trash;
using N2.Integrity;
using N2.Persistence;

namespace N2.Messaging
{
    [Definition("Задание", "Task")]
    [RestrictParents(typeof(MessageStore),typeof(RecycleBin),typeof(DraughtStore))]
    [NotThrowable, NotVersionable]
    public class Task : Message
    {
        #region Properties

        public override string IconUrl { get { return "~/Lms/UI/Img/04/15.png"; } }

        public override string TemplateUrl { get { return "~/Messaging/UI/Views/Message.aspx"; } }

        //Признак выполнения.
        [EditableCheckBox("Выполнено", 10)]
        public bool isDone
        {
            get { return (bool)(GetDetail("isDone") ?? false); }
            set { SetDetail("isDone", value); }
        }

        #endregion
    }
}
