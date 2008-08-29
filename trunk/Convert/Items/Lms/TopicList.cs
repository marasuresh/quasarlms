namespace N2.Lms.Items
{
	using N2.Definitions;
	using N2.Details;
	using N2.Edit.Trash;
	using N2.Installation;
	using N2.Integrity;
	using N2.Persistence;
	
	[Definition("Topic List", "Topics", "", "", 2000, Installer = InstallerHint.NeverRootOrStartPage)]
	[NotThrowable, NotVersionable]
	[RestrictParents(typeof(Course))]
	public class TopicList: ContentItem
	{
		public override string IconUrl { get { return "~/Lms/UI/Img/01/46.png"; } }

		[EditableTextBox("Title", 10)]
		public override string Title {
			get { return base.Title ?? "Topics"; }
			set { base.Title = value; }
		}
	}
}
