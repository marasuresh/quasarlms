namespace N2.Lms.Items
{
	using Definitions;
	using Details;
	using Edit.Trash;
	using Installation;
	using Integrity;
	using Persistence;

	[Definition("Topic List", "Topics", "", "", 2000, Installer = InstallerHint.NeverRootOrStartPage)]
	[NotThrowable, NotVersionable]
	[RestrictParents(typeof(Course))]
	[WithEditableTitle]
	[Disable]
	public partial class TopicContainer: ItemContainer<Topic>
	{
		public TopicContainer()
		{
			this.Name = "topics";
			this.Title = N2.Lms.Strings.TopicContainer_Title;
		}

		#region System properties
		
		public override string IconUrl { get { return "~/Lms/UI/Img/01/46.png"; } }
		public override string TemplateUrl { get { return "~/Lms/UI/TopicContainer.ascx"; } }
		public override string ZoneName { get { return "TopicContainer"; } }
		public override bool IsPage { get { return false; } }

		#endregion System properties
	}
}
