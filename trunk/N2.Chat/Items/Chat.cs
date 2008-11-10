using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2;
using N2.Templates.Items;

namespace N2.Templates.Chat.Items
{
    [Definition("Chat", "Chat", "A chat that is implemented inline", "", 20)]
    public class Chat : AbstractContentPage
    {
        #region N2.Properties
        
        public override string TemplateUrl
        {
            get
            {
                return "~/Chat/UI/Views/ChatPage.aspx";
            }
        }

        public override string IconUrl
        {
            get
            {
                return "~/Chat/UI/Images/chat.png";
            }
        }

        #endregion
    }
}
