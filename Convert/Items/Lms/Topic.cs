namespace N2.Lms.Items
{
	using System.Collections.Generic;
	using System.Linq;

	using N2.Details;
	using N2.Installation;
	using N2.Integrity;
	using N2.Edit.Trash;

	[Definition("Topic", "Topic", Installer = InstallerHint.NeverRootOrStartPage)]
	[RestrictParents(typeof(TopicList), typeof(Topic))]
	[AllowedChildren(typeof(Topic), typeof(Test))]
	[NotThrowable]
	[WithEditableName("Name (Guid)", 10)]
	[WithEditableTitle("Title", 20)]
	public class Topic: ContentItem, IContinuous
	{
		#region Properties
		
		public override string IconUrl { get { return "~/Lms/UI/Img/04/19.png"; } }
		public override string TemplateUrl { get { return "~/Lms/UI/Topic.aspx"; } }
		
		#endregion Properties

		#region Lms Properties

		IEnumerable<string> Content {
			get {
				return
					from _ld in this.GetDetailCollection("Content", true).OfType<StringDetail>()
					select _ld.StringValue;
			}
		}

/*		[EditableUrl("Content Url", 30)]
		public string ContentUrl {
			get { return (string)this.GetDetail("ContentUrl"); }
			set { this.SetDetail<string>("ContentUrl", value); }
		}
*/
		[EditableCheckBox("Mandatory", 70)]
		public bool Mandatory {
			get { return (bool?)this.GetDetail("Mandatory") ?? true; }
			set { this.SetDetail<bool>("Mandatory", value); }
		}

		[EditableTextBox("Duration", 80)]
		public int Duration
		{
			get { return (int?)this.GetDetail("Duration") ?? 0; }
			set { this.SetDetail<int>("Duration", value); }
		}

		[EditableItem("Practice", 90)]
		public Test Practice
		{
			get { return this.GetDetail("Practice") as Test; }
			set { this.SetDetail<Test>("Practice", value); }
		}

		/*
		public Course Course {
			get {
				ContentItem _result = this.Parent;
				while(_result != null || !(_result is Course)) {
					_result = _result.Parent;
				}
				return _result as Course;
			}
		}
*/
		#endregion Lms Properties
	}
}
