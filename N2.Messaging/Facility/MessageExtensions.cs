using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace N2.Messaging
{
	internal static class MessageExtensions
	{
		static string CurrentUserName { get { return HttpContext.Current.User.Identity.Name; } }

		public static IEnumerable<Message> GetMyMessages(
			this ContentItem item)
		{
			return
				from child in item.Children.OfType<Message>()
				where ((string.Equals(child.Owner, CurrentUserName, StringComparison.OrdinalIgnoreCase)))
				select child;
		}
	}
}
