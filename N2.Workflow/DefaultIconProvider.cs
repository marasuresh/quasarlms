namespace N2.Workflow
{
	using System.Web;
	using System.Web.UI;
	using Items;
	using System.Diagnostics;
	
	internal static class DefaultIconProvider
	{
		public static string GetIconUrl(
			this StateDefinition item)
		{
			return GetIconUrl((item.SortOrder + 4) % 10 + 1);
		}

		public static string GetIconUrl(int position)
		{
			string _result = null;
			
			if (System.Web.Hosting.HostingEnvironment.IsHosted) {
				Page _page = HttpContext.Current.Handler as Page;

				if (null != _page) {
					string _res = string.Format("N2.Workflow.Images.{0:00}.png", position);
					
					Trace.WriteLine("Resource: "+_res, "Workflow");
					_result = _page.ClientScript.GetWebResourceUrl(typeof(Workflow), _res);
				}
			}

			return _result;
		}
	}
}
