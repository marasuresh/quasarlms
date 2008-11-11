namespace N2.Lms.Items
{
	using N2.Edit.Trash;
	using N2.Installation;
	using N2.Persistence;
	using N2.Templates.Items;

	[Definition(
		"Global LMS Storage Container",
		"LmsStorage",
		"",
		"", 9999, Installer = InstallerHint.NeverRootOrStartPage)]
	//[WithEditableTitle("Title", 10)]
	[NotThrowable, NotVersionable]
	public partial class Storage: AbstractContentPage
	{
		public Storage()
		{
			this.Title = "Хранилище данных системы ДО";
		}

		#region System Properties

		public override string IconUrl { get { return "~/Lms/UI/Img/04/17.png"; } }
		public override string TemplateUrl { get { return "~/Templates/Secured/Go.aspx"; } }
		public override bool IsPage { get { return false; } }

		#endregion System Properties
	}
}
