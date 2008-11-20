using System;

namespace N2.Web
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class UriTemplateAttribute: Attribute, ITemplateReference
	{
		#region Fields

		readonly string templateUrl;
		readonly UriTemplate m_template;
		static Uri BaseUrl = new Uri("http://localhost");

		#endregion Fields

		#region Constructors

		public UriTemplateAttribute(string uri, string templateUrl)
		{
			this.templateUrl = templateUrl;
			this.m_template = new UriTemplate(uri);
		}

		#endregion Constructors

		#region ITemplateReference Members

		public TemplateData GetTemplate(ContentItem item, string remainingUrl)
		{
			var _match = this.m_template.Match(BaseUrl, new Uri(BaseUrl, remainingUrl));

			if (null != _match) {
				return new UriTemplateData(item, templateUrl, _match);
			}

			return null;
		}

		#endregion
	}
}
