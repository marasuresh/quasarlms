namespace N2.Lms.Items
{
	using System.Collections.Generic;
	using System.Linq;

	using N2.Details;
	using N2.Installation;
	using N2.Integrity;
	using N2.Edit.Trash;
	using N2.Templates.Items;
	using N2.Definitions;

	[Definition("Topic", "Topic", Installer = InstallerHint.NeverRootOrStartPage)]
	[RestrictParents(typeof(TopicList), typeof(Topic))]
	[WithEditableName("Name (Guid)", 03)]
	[WithEditableTitle("Title", 05)]
	[WithEditablePublishedRange("Published between", 07)]
	[ReplaceDefinitions(typeof(AbstractContentPage), typeof(ContentItem))]
	public class Topic : AbstractItem
	{
		#region System properties

		public override string IconUrl { get { return "~/Lms/UI/Img/04/19.png"; } }
		public override string TemplateUrl { get { return "~/Lms/UI/Topic.ascx"; } }
		public override string ZoneName {
			get { return "Topics"; }
			set { base.ZoneName = value; }
		}
		public override bool IsPage { get { return false; } }

		#endregion System properties

		#region Lms Properties
		
		public DetailCollection Content {
			get {
				return
					this.GetDetailCollection("Content", true);
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

		//[EditableItem("Practice", 90, Required = false)]
		public Test Practice
		{
			get { return this.GetDetail("Practice") as Test; }
			set { this.SetDetail<Test>("Practice", value); }
		}

		
		public Course Course {
			get {
				return N2.Find.EnumerateParents(this).OfType<Course>().FirstOrDefault() ?? this.Training.Course;
			}
		}

		public Training Training {
			get {
				return N2.Find.EnumerateParents(this).OfType<Training>().FirstOrDefault();
			}
		}
		
		#endregion Lms Properties
	}
}
