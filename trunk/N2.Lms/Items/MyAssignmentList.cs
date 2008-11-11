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
	public partial class MyAssignmentList: AbstractItem
	{
		#region Lms properties

		[EditableLink(
			"Course Container", 03,
			Required = true,
			LocalizationClassKey = "Lms.CourseList")]
		public CourseContainer CourseContainer
		{
			get { return this.GetDetail("CourseContainer") as CourseContainer; }
			set { this.SetDetail<CourseContainer>("CourseContainer", value); }
		}

		[EditableLink(
			"Request Container", 05,
			Required = true,
			LocalizationClassKey = "Lms.CourseList")]
		public RequestContainer RequestContainer
		{
			get { return this.GetDetail("RequestContainer") as RequestContainer; }
			set { this.SetDetail<RequestContainer>("RequestContainer", value); }
		}
		
		#endregion Lms properties
		
		#region System properties
		
		public override string TemplateUrl { get { return "~/Lms/UI/Parts/MyAssignmentList.ascx"; } }
		
		public override bool IsPage { get { return false; } }
		
		#endregion System properties
	}
}
