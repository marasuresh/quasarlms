namespace N2.Lms.Items
{
	using System;
	using System.Web.UI.WebControls;
	using N2.Details;
	using N2.Integrity;
	using N2.Web.UI.WebControls;
	
	[Definition("Request", "CourseRequest")]
	[RestrictParents(typeof(RequestContainer))]
	[AllowedChildren(Types = new Type[0])]
	public class Request : ContentItem
	{
		public override string IconUrl {
			get {
				return
					string.Format(
						"~/Lms/UI/Img/04/{0:00}.png", (int)this.State);
			}
		}
		
		public override bool IsPage { get { return false; } }

		public override string Name {
			get {
				return
					string.IsNullOrEmpty(base.Name)
						? this.GetDafaultName()
						: base.Name;
			}
			set { base.Name = value; }
		}

		string GetDafaultName()
		{
			return
				(this.SavedBy
					+ (this.Course != null
						? " " + this.Course.Title
						: string.Empty)
					+ " " + this.RequestDate.ToShortDateString());
		}

		#region Lms Properties

		public Course Course { get; set; }

		[EditableTextBox("Comments", 10, TextMode = TextBoxMode.MultiLine, Rows=3)]
		public string Comments
		{
			get { return this.GetDetail("Comments") as string; }
			set { this.SetDetail<string>("Comments", value); }
		}

		[Editable("Request Date", typeof(DatePicker), "SelectedDate", 20)]
		public DateTime RequestDate
		{
			get { return (DateTime?)this.GetDetail("RequestDate") ?? DateTime.Now; }
			set { this.SetDetail<DateTime>("RequestDate", value); }
		}

		[Editable("Start Date", typeof(DatePicker), "SelectedDate", 30)]
		public DateTime StartDate
		{
			get { return (DateTime?)this.GetDetail("StartDate") ?? DateTime.Now; }
			set { this.SetDetail<DateTime>("StartDate", value); }
		}

		[EditableEnum("State", 14, typeof(RequestState))]
		public RequestState State
		{
			get { return (RequestState?)this.GetDetail("Workflow.State")
				?? RequestState.New; }
			set { this.SetDetail<RequestState>("Workflow.State", value); }
		}

		#endregion Lms Properties
	}

	public enum RequestState
	{
		New = 10,
		Approved = 2,
		Denied = 1,
		Cancelled = 4,
		Finished = 3
	}
}
