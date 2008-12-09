using System;
using System.Linq;
using System.Collections.Generic;

namespace N2.Web
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class UriTemplateAttribute: Attribute, IPathFinder
	{
		#region Fields

		readonly string templateUrl;
		readonly UriTemplate m_template;
		readonly string action;
		static Uri BaseUrl = new Uri("http://localhost");

		#endregion Fields

		#region Constructors

		public UriTemplateAttribute(string uri, string templateUrl)
			: this(uri, templateUrl, UriPathData.DefaultAction)
		{
		}

		public UriTemplateAttribute(string uri, string templateUrl, string actionName)
		{
			this.templateUrl = templateUrl;
			this.m_template = new UriTemplate(uri);
			this.action = actionName;
		}

		#endregion Constructors

		#region ITemplateReference Members

		public PathData GetPath(ContentItem item, string remainingUrl)
		{
			var _nextSiblings = (
				from _attr in item.GetType().GetCustomAttributes(typeof(UriTemplateAttribute), true).Cast<UriTemplateAttribute>()
				select _attr
			).ToArray();

			if (1 == _nextSiblings.Length) {

				var _firstSibling = _nextSiblings[0];

				var _match = _firstSibling.m_template.Match(
					BaseUrl,
					new Uri(BaseUrl, remainingUrl));

				if (null != _match) {
					return new UriPathData(item, _firstSibling.templateUrl, _match);
				}
			} else {
				var _uriTable = new UriTemplateTable(BaseUrl,
					from _attr in _nextSiblings
					select new KeyValuePair<UriTemplate, object>(
						_attr.m_template,
						_attr)
					);

				var _matches = _uriTable.Match(new Uri(BaseUrl, remainingUrl));

				if (_matches.Any()) {
					var _firstMatch = _matches.First();
					var _matchedAttribute = (UriTemplateAttribute)_firstMatch.Data;

					return new UriPathData(
						item,
						_matchedAttribute.templateUrl,
						_matchedAttribute.action,
						_firstMatch);
				}
			}

			return null;
		}

		#endregion
	}
}
