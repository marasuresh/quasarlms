namespace N2.Lms.Items
{
	using System.Collections.Generic;
	using System.Linq;

	using N2.Details;
	using N2.Installation;
	using N2.Integrity;
	using N2.Edit.Trash;

	[Definition("Course", "Course", Installer = InstallerHint.NeverRootOrStartPage)]
	[RestrictParents(typeof(CourseList))]
	[NotThrowable]
	[WithEditableName("Name (Guid)", 10)]
	[WithEditableTitle("Title", 20)]
	public class Course : ContentItem, IContinuous
	{
		#region Properties

		public override string IconUrl { get { return "~/Lms/UI/Img/04/15.png"; } }
		public override string TemplateUrl { get { return "~/Lms/UI/CourseInfo.aspx"; } }
		public override bool IsPage { get { return true; } }

		#endregion Properties

		#region Methods

		void EnsureTopicList(IList<ContentItem> children)
		{
			if (!children.OfType<TopicList>().Any()) {
				var _tl = Context.Current.Definitions.CreateInstance<TopicList>(this);
				_tl.Name = this.Name + "TopicList";
				_tl.Title = this.Title + " Topics";
				Context.Current.Persister.Save(_tl);
			}
		}

		#endregion Methods

		#region Lms Properties

		[EditableFreeTextArea("Description", 30)]
		public string Description
		{
			get { return this.GetDetail("Description") as string; }
			set { this.SetDetail<string>("Description", value); }
		}

		[EditableTextBox("Version", 40)]
		public string Version {
			get { return this.GetDetail("Version") as string; }
			set { this.SetDetail<string>("Version", value); }
		}

		[EditableCheckBox("Is Public", 50)]
		public bool IsPublic
		{
			get { return (bool?)this.GetDetail("Public") ?? false; }
			set { this.SetDetail<bool>("Public", value); }
		}

		[EditableCheckBox("Is Ready", 55)]
		public bool IsReady
		{
			get { return (bool?)this.GetDetail("IsReady") ?? false; }
			set { this.SetDetail<bool>("IsReady", value); }
		}

		[EditableTextBox("Keywords", 60)]
		public string Keywords
		{
			get { return this.GetDetail("Keywords") as string; }
			set { this.SetDetail<string>("Keywords", value); }
		}

		[EditableTextBox("Type", 70)]
		public string Type
		{
			get { return this.GetDetail("Type") as string; }
			set { this.SetDetail<string>("Type", value); }
		}

		[EditableTextBox("Duration", 80)]
		public int Duration
		{
			get { return (int?)this.GetDetail("Duration") ?? 0; }
			set { this.SetDetail<int>("Duration", value); }
		}

		[EditableTextBox("Cost1", 90)]
		public double Cost1
		{
			get { return (double?)this.GetDetail("Cost1")??0; }
			set { this.SetDetail<double>("Cost1", value); }
		}

		[EditableTextBox("Cost1", 100)]
		public double Cost2
		{
			get { return (double?)this.GetDetail("Cost2") ?? 0; }
			set { this.SetDetail<double>("Cost2", value); }
		}

		[EditableTextBox("Area", 110)]
		public string Area
		{
			get { return this.GetDetail("Area") as string; }
			set { this.SetDetail<string>("Area", value); }
		}

		[EditableTextBox("Disc Folder", 120)]
		public string DiskFolder {
			get { return this.GetDetail("DiskFolder") as string; }
			set { this.SetDetail<string>("DiskFolder", value); }
		}

		[EditableTextBox("Additions", 130)]
		public string Additions {
			get { return this.GetDetail("Additions") as string; }
			set { this.SetDetail<string>("Additions", value); }
		}

		[EditableTextBox("Author", 140)]
		public string Author
		{
			get { return this.GetDetail("Author") as string; }
			set { this.SetDetail<string>("Author", value); }
		}

		[EditableUrl("Description", 150)]
		public string DescriptionUrl
		{
			get { return this.GetDetail("DescriptionUrl") as string; }
			set { this.SetDetail<string>("DescriptionUrl", value); }
		}

		[EditableUrl("Requirements", 160)]
		public string RequirementsUrl
		{
			get { return this.GetDetail("RequirementsUrl") as string; }
			set { this.SetDetail<string>("RequirementsUrl", value); }
		}

		#endregion Lms Properties
	}
}