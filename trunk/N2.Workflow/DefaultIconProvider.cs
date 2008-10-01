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
			Trace.WriteLine("Get icon url: " + position.ToString(), "Workflow");
			string _result = null;
			
			if(null != ClientScript) {

				string _res = string.Format("N2.Workflow.Images.{0:00}.png", position);

				Trace.WriteLine("Resource: " + _res, "Workflow");
				_result = ClientScript.GetWebResourceUrl(typeof(Workflow), _res);
			}

			return _result;
		}

		static ClientScriptManager s_cs;
		static ClientScriptManager ClientScript {
			get
			{
				if(!System.Web.Hosting.HostingEnvironment.IsHosted) {
					return null;
				}
				
				if(null == s_cs) {
					Page _page = HttpContext.Current.Handler as Page;

					if(null == _page) {
						_page = new Page();
						((IHttpHandler)_page).ProcessRequest(HttpContext.Current);
					}

					s_cs = _page.ClientScript;
				}
				return s_cs;
			}
		}
	}
}
