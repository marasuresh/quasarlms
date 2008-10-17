using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2.Collections;

namespace N2.Messaging.Collections
{
    public class UserMsgFilter : AccessFilter
    {
        public override bool Match(ContentItem item)
        {
            if (item is Message)
            {
                bool match = ((Message)item).Owner == User.Identity.Name;
                return match; 
            }
            
            return false;
        }

        public static void FilterMsg(IList<ContentItem> items)
        {
            Filter(items, new UserMsgFilter());
        }
    }
}
