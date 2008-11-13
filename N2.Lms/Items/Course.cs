namespace N2.Lms.Items
{
	using N2.Definitions;
	using N2.Details;
	using N2.Installation;
	using N2.Integrity;
	using N2.Serialization;
	using N2.Web.UI;

	[Definition("Course", "Course", Installer = InstallerHint.NeverRootOrStartPage)]
	[RestrictParents(typeof(CourseContainer))]
	//[WithEditableTitle("Title", 20)]
	[TabPanel("lms", "LMS", 200)]
	[	//EnsureChild(Course.TopicContainerName, typeof(TopicContainer)),
		EnsureChild(Course.TrainingContainerName, typeof(TrainingContainer))]
	[WithEditableTitle]
	public partial class Course : ContentItem
	{
		public Course()
		{
		}

		#region Constants
		
		public const string TopicContainerName = "Topics";
		public const string TrainingContainerName = "Trainings";

		#endregion Constants

		#region Properties

		public override string IconUrl { get { return "~/Lms/UI/Img/04/15.png"; } }
		public override string TemplateUrl { get { return "~/Lms/UI/CourseInfo.aspx"; } }
		public override string ZoneName { get { return "Content"; } }
		public override bool IsPage { get { return false; } }

		#endregion Properties

		#region Lms Properties

		[EditableFreeTextArea("Text", 345, ContainerName = "lms", LocalizationClassKey = "Course")]
		public string Text { get { return (string)this.GetDetail("Text"); } set { this.SetDetail<string>("Text", value); } }

		[EditableCheckBox("Is Public", 350, ContainerName = "lms", LocalizationClassKey = "Course")]
		public bool IsPublic
		{
			get { return (bool?)this.GetDetail("Public") ?? false; }
			set { this.SetDetail<bool>("Public", value); }
		}

		[EditableCheckBox("Is Ready", 355, ContainerName = "lms")]
		public bool IsReady
		{
			get { return (bool?)this.GetDetail("IsReady") ?? false; }
			set { this.SetDetail<bool>("IsReady", value); }
		}

		[EditableTextBox("Keywords", 360, ContainerName = "lms")]
		public string Keywords
		{
			get { return this.GetDetail("Keywords") as string; }
			set { this.SetDetail<string>("Keywords", value); }
		}

		[EditableTextBox("Type", 370, ContainerName = "lms")]
		public string Type
		{
			get { return this.GetDetail("Type") as string; }
			set { this.SetDetail<string>("Type", value); }
		}

		[EditableTextBox("Duration, <i>days</i>", 380, ContainerName = "lms")]
		public int Duration
		{
			get { return (int?)this.GetDetail("Duration") ?? 0; }
			set { this.SetDetail<int>("Duration", value); }
		}

		[EditableTextBox("Price, <i>UAH</i>", 390, ContainerName = "lms")]
		public double Cost1
		{
			get { return (double?)this.GetDetail("Cost1")??0; }
			set { this.SetDetail<double>("Cost1", value); }
		}

		[EditableTextBox("Price, <i>USD</i>", 400, ContainerName = "lms")]
		public double Cost2
		{
			get { return (double?)this.GetDetail("Cost2") ?? 0; }
			set { this.SetDetail<double>("Cost2", value); }
		}

		[EditableTextBox("Area", 410, ContainerName = "lms")]
		public string Area
		{
			get { return this.GetDetail("Area") as string; }
			set { this.SetDetail<string>("Area", value); }
		}

		[EditableUrl("Disc Folder", 420, ContainerName = "lms")]
		public string DiskFolder {
			get { return this.GetDetail("DiskFolder") as string; }
			set { this.SetDetail<string>("DiskFolder", value); }
		}

		[EditableTextBox("Author", 440, ContainerName = "lms")]
		public string Author
		{
			get { return this.GetDetail("Author") as string; }
			set { this.SetDetail<string>("Author", value); }
		}

		[EditableUrl("Description Url", 450, ContainerName = "lms")]
		[FileAttachment]
		public string DescriptionUrl
		{
			get { return this.GetDetail("DescriptionUrl") as string; }
			set { this.SetDetail<string>("DescriptionUrl", value); }
		}

		[EditableUrl("Requirements", 460, ContainerName = "lms")]
		[FileAttachment]
		public string RequirementsUrl
		{
			get { return this.GetDetail("RequirementsUrl") as string; }
			set { this.SetDetail<string>("RequirementsUrl", value); }
		}

		#endregion Lms Properties
	}
}