namespace N2.Lms.Items
{
	using System;
	using N2.Definitions;
	using N2.Details;
	using N2.Edit.Trash;
	using N2.Installation;
	using N2.Persistence;
	using N2.Integrity;
	using N2.Templates.Items;
	
	/// <summary>
	/// User-bound course list for diplaying courses and user-specific details.
	/// </summary>
	[Definition("Course List", "CourseList", "", "", 2000, Installer = InstallerHint.NeverRootOrStartPage)]
	//[WithEditableTitle("Title", 10)]
	[RestrictParents(typeof(IStructuralPage))]
	public class CourseList: AbstractContentPage
	{
		public override string IconUrl { get { return "~/Lms/UI/Img/04/16.png"; } }

		public override string TemplateUrl { get { return "~/Lms/UI/CourseList.aspx"; } }

		#region Lms Properties

		[EditableLink("Course Container", 73)]
		public CourseContainer CourseContainer {
			get { return this.GetDetail("CourseContainer") as CourseContainer; }
			set { this.SetDetail<CourseContainer>("CourseContainer", value); }
		}

		[EditableLink("Request Container", 75)]
		public RequestContainer RequestContainer {
			get { return this.GetDetail("RequestContainer") as RequestContainer; }
			set { this.SetDetail<RequestContainer>("RequestContainer", value); }
		}

		#endregion Lms Properties
	}
}
