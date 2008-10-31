﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2.Edit.Trash;
using N2.Integrity;
using N2.Persistence;

namespace N2.Messaging
{
    [Definition("Объявление", "Announcement")]
    [RestrictParents(typeof(MessageStore), typeof(RecycleBin), typeof(DraughtStore))]
    [NotThrowable, NotVersionable]
    public class Announcement : Message
    {
        #region Properties

        public override string IconUrl { get { return "~/Messaging/UI/Images/bell.png"; } }

        public override string TemplateUrl { get { return "~/Messaging/UI/Views/Message.aspx"; } }

        public override string TypeOfMessage { get { return "announcementcss"; } }

        #endregion
    }
}