using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N2.Lms.Items
{
	using N2.Templates.Items;
	using N2.Integrity;
	using N2.Details;

	[Definition]
	[AllowedZones("Content", "Right", "Left")]
	[WithEditableName]
	[WithEditableTitle]
	public class MyRequests: AbstractItem
	{
		#region Lms properties

		[EditableLink("Request container", 103)]
		public RequestContainer RequestContainer {
			get { return this.GetDetail<RequestContainer>("RequestContainer", null); }
			set { this.SetDetail<RequestContainer>("RequestContainer", value); }
		}

		#endregion Lms properties

		#region System properties

		public override string TemplateUrl { get { return "~/Lms/UI/Parts/MyRequests.ascx"; } }
		
		public override bool IsPage { get { return false; } }
		
		#endregion System properties
	}
}
