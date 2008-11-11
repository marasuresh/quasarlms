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
	[WithEditableDateRange("Desired time span", 13, "RequestDate", "StartDate")]
	[WithWorkflowAuditTrail(Name = "Audit Trail")]
	public class Request : ContentItem
	{
		public override string IconUrl { get {
			return null == this.Course ? "~/Lms/UI/Img/error.png" : this.GetIconFromState();
		} }
		
//		public override bool IsPage { get { return false; } }

		public override string Title {
			get { return base.Title ?? (base.Title = this.GetDafaultName()); }
			set { base.Title = value; }
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

		public override bool IsAuthorized(System.Security.Principal.IPrincipal user)
		{
			return base.IsAuthorized(user) && this.User == user.Identity.Name;
		}

		#region Lms Properties

		[Editable("User",
			typeof(N2.Web.UI.WebControls.SelectUser),
			"SelectedUser",
			15)]
		public string User
		{
			get { return this.GetDetail<string>("User", null); }
			set { this.SetDetail<string>("User", value); }
		}

		[EditableLink("Course", 17, Required = true)]
		public Course Course {
			get { return this.GetDetail("Course") as Course; }
			set { this.SetDetail<Course>("Course", value); }
		}

		[EditableTextBox("Comments", 10, TextMode = TextBoxMode.MultiLine, Rows=3)]
		public string Comments
		{
			get { return this.GetDetail("Comments") as string; }
			set { this.SetDetail<string>("Comments", value); }
		}

		public DateTime RequestDate
		{
			get { return (DateTime?)this.GetDetail("RequestDate") ?? DateTime.Now; }
			set { this.SetDetail<DateTime>("RequestDate", value); }
		}

		public DateTime StartDate
		{
			get { return (DateTime?)this.GetDetail("StartDate") ?? DateTime.Now; }
			set { this.SetDetail<DateTime>("StartDate", value); }
		}

		#endregion Lms Properties
	}
}
