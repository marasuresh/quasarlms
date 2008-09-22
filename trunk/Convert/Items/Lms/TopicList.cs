namespace N2.Lms.Items
{
	using System.Linq;
	using System.Collections.Generic;
	using N2.Definitions;
	using N2.Details;
	using N2.Edit.Trash;
	using N2.Installation;
	using N2.Integrity;
	using N2.Persistence;
	using N2.Collections;
	
	[Definition("Topic List", "Topics", "", "", 2000, Installer = InstallerHint.NeverRootOrStartPage)]
	[NotThrowable, NotVersionable]
	[RestrictParents(typeof(Course))]
	[Disable]
	public class TopicList: ContentItem
	{
		#region System properties
		
		public override string IconUrl { get { return "~/Lms/UI/Img/01/46.png"; } }

		public override bool IsPage { get { return false; } }

		#endregion System properties

		[EditableTextBox("Title", 10)]
		public override string Title {
			get { return base.Title ?? "Topics"; }
			set { base.Title = value; }
		}

		internal IEnumerable<Topic> Topics {
			get { return this.GetChildren(
				new TypeFilter(typeof(Topic))).Cast<Topic>(); }
		}
	}
}
