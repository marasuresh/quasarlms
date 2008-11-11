using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2;
using N2.Details;
using N2.Lms.Items;
using N2.Templates.Items;

namespace N2.Templates.Chat.Items
{
    [Definition("Chat", "Chat", "A chat that is implemented inline", "", 20)]
    public class Chat : AbstractContentPage
    {
        #region Lms properties

        [EditableLink(
            "Request Container", 05,
            Required = true,
            LocalizationClassKey = "Lms.CourseList")]
        public RequestContainer RequestContainer
        {
            get { return this.GetDetail("RequestContainer") as Lms.Items.RequestContainer; }
            set { this.SetDetail<Lms.Items.RequestContainer>("RequestContainer", value); }
        }

        #endregion
        
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
