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

        public override string IconUrl { get { return "~/Lms/UI/Img/04/50.png"; } }

        public override string TemplateUrl { get { return "~/Messaging/UI/Message.aspx"; } }

        #endregion
    }
}
