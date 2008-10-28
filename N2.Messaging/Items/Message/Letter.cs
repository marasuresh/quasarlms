using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2.Edit.Trash;
using N2.Integrity;
using N2.Persistence;

namespace N2.Messaging
{
    [Definition("Письмо", "Letter")]
    [RestrictParents(typeof(MessageStore),typeof(RecycleBin),typeof(DraughtStore))]
    [NotThrowable, NotVersionable]
    public class Letter : Message
    {
        #region Properties

        public override string IconUrl { get { return "~/Messaging/UI/Images/email_open.png"; } }

        public override string TemplateUrl { get { return "~/Messaging/UI/Views/Message.aspx"; } }

        public override string TypeOfMessage { get { return "lettercss"; } }

        #endregion
    }
}
