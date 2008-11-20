using System;
using System.Linq;

namespace N2.Lms.Web.UI
{
	public static class HtmlExtensions
	{
		public static string ToHtmlString(this TimeSpan span)
		{
			return
				string.Join(":", new[] {
						span.Hours,
						span.Minutes,
						span.Seconds }
					.Select(i => i.ToString("00"))
					.ToArray());
		}
	}
}
