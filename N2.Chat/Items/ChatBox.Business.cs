using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2.Lms.Items;

namespace N2.Templates.Chat.Items
{
    /// <summary>
    /// Бизнес-логика класса ChatBox
    /// </summary>
    partial class ChatBox
    {

        /// <summary>
        /// Содержит все активные тренинги пользователя, содержащиеся в запросах.
        /// (Для администраторов - все активные тренинги, содержащиеся в запросах.)
        /// </summary>
        public IEnumerable<Request> ApprovedRequests
        {
            get { return this.RequestContainer.MyApprovedRequests; }
        }
    }
}
