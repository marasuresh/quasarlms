namespace N2.Lms.Items
{
	using Details;
	using Edit.Trash;
	using Installation;
	using Persistence;

	[Definition(
		"Global LMS Storage Container",
		"LmsStorage",
		"",
		"", 9999, Installer = InstallerHint.NeverRootOrStartPage)]
	[WithEditableTitle("Title", 1)]
	[WithEditableName("Name", 2)]
	[NotThrowable, NotVersionable]
	public partial class Storage: ContentItem
	{
		public Storage()
		{
			this.Name = "storage";
			this.Title = "Данные";
		}

		#region System Properties

		public override string IconUrl { get { return "~/Messaging/UI/Images/database.png"; } }
		public override string TemplateUrl { get { return "~/Templates/Secured/Go.aspx"; } }
		public override bool IsPage { get { return false; } }

		#endregion System Properties
	}
}
