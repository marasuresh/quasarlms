using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2.Collections;

namespace N2.Messaging.Collections
{

    /// <summary>
    /// Фильтр выбора входящих сообщений текущего пользователя.
    /// </summary>
    class UserNewMsgFilter : AccessFilter
    {
        public override bool Match(ContentItem item)
        {
            if (item is Message)
            {
                bool match = ((Message)item).To == User.Identity.Name &&
                             ((Message)item).Owner == User.Identity.Name &&
                             !((Message)item).isRead;
                return match;
            }

            return false;
        }

        public static void FilterMsg(IList<ContentItem> items)
        {
            Filter(items, new UserNewMsgFilter());
        }
    }
}
