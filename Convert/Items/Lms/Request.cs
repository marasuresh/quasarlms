namespace N2.Lms.Items
{
	using System;
	using N2.Details;
	using N2.Integrity;
	
	[Definition("Request", "CourseRequest")]
	[RestrictParents(typeof(RequestList))]
	[AllowedChildren(Types = new Type[0])]
	public class Request : ContentItem
	{
		public override string IconUrl { get { return "~/Lms/UI/Img/04/49.png"; } }
		
		public override bool IsPage { get { return false; } }

		#region Lms Properties

		public Course Course { get; set; }

		[EditableFreeTextArea("Comments", 10)]
		public string Comments
		{
			get { return this.GetDetail("Comments") as string; }
			set { this.SetDetail<string>("Comments", value); }
		}

		[Editable("Request Date", typeof(System.Web.UI.WebControls.Calendar), "SelectedDate", 20)]
		public DateTime RequestDate
		{
			get { return (DateTime?)this.GetDetail("RequestDate") ?? DateTime.Now; }
			set { this.SetDetail<DateTime>("RequestDate", value); }
		}

		[Editable("Request Date", typeof(System.Web.UI.WebControls.Calendar), "SelectedDate", 30)]
		public DateTime StartDate
		{
			get { return (DateTime?)this.GetDetail("StartDate") ?? DateTime.Now; }
			set { this.SetDetail<DateTime>("StartDate", value); }
		}

		#endregion Lms Properties
	}
}
