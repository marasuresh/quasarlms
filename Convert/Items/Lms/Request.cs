namespace N2.Lms.Items
{
	using System;
	using System.Web.UI.WebControls;
	using N2.Details;
	using N2.Integrity;
	using N2.Web.UI.WebControls;
	using N2.Workflow;
	using N2.Workflow.Items;
	
	[Definition("Request", "CourseRequest")]
	[RestrictParents(typeof(RequestContainer))]
	[AllowedChildren(Types = new Type[0])]
	[N2.Persistence.NotVersionable]
	[WithWorkflowAction(Name="Workflow", SortOrder = 150)]
	public class Request : ContentItem
	{
		public override string IconUrl {
			get {
				return
					this.GetCurrentState().ToState.Icon;
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

		#endregion Lms Properties
	}
}
