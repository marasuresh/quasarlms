using System;

namespace N2.Web
{
	public class UriTemplateData: TemplateData
	{
		#region Fields
		
		readonly ContentItem m_item;
		
		#endregion Fields

		#region Properties
		
		public virtual UriTemplateMatch Match { get; protected set; }
		
		#endregion Properties

		#region Constructors
		
		internal UriTemplateData(ContentItem item, string templateUrl, UriTemplateMatch match)
			:base(item, templateUrl)
		{
			this.Match = match;
		}
		
		#endregion Constructors
	}
}
