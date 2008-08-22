namespace N2.Lms.Items
{
	using System;
	using N2.Details;

	[Definition]
	public class CourseRequest : ContentItem
	{
		public Course Course { get; set; }

		[EditableFreeTextArea("Comments", 10)]
		public string Comments
		{
			get { return this.GetDetail("Comments") as string; }
			set { this.SetDetail<string>("Comments", value); }
		}

		[EditableFreeTextArea("Request Date", 20)]
		public DateTime RequestDate
		{
			get { return (DateTime?)this.GetDetail("RequestDate") ?? DateTime.Now; }
			set { this.SetDetail<DateTime>("RequestDate", value); }
		}

		[EditableFreeTextArea("Start Date", 30)]
		public DateTime StartDate
		{
			get { return (DateTime?)this.GetDetail("StartDate") ?? DateTime.Now; }
			set { this.SetDetail<DateTime>("StartDate", value); }
		}
	}
}
