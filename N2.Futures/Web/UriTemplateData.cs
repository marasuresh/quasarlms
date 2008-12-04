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
		
		internal UriTemplateData(
				ContentItem item,
				string templateUrl,
			UriTemplateMatch match)
			:this(item, templateUrl, DefaultAction, match)
		{
		}

		internal UriTemplateData(
				ContentItem item,
				string templateUrl,
				string action,
				UriTemplateMatch match)
			: base(item, templateUrl, action, string.Empty)
		{
			this.Match = match;
		}
		
		#endregion Constructors
	}
}
