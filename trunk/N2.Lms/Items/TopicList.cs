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
	using N2.Templates.Items;
	
	[Definition("Topic List", "Topics", "", "", 2000, Installer = InstallerHint.NeverRootOrStartPage)]
	[NotThrowable, NotVersionable]
	[RestrictParents(typeof(Course))]
	[WithEditableTitle("Title", 10)]
	[Disable]
	public partial class TopicContainer: ContentItem
	{
		public TopicContainer()
		{
			this.Title = "Topics";
		}

		#region System properties
		
		public override string IconUrl { get { return "~/Lms/UI/Img/01/46.png"; } }
		public override string TemplateUrl { get { return "~/Lms/UI/TopicContainer.ascx"; } }
		public override string ZoneName { get { return "TopicContainer"; } }
		public override bool IsPage { get { return false; } }

		#endregion System properties
	}
}
