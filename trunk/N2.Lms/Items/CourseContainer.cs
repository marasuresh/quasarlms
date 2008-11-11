namespace N2.Lms.Items
{
	using N2.Definitions;
	using N2.Details;
	using N2.Edit.Trash;
	using N2.Installation;
	using N2.Persistence;
	using N2.Templates.Items;
	using N2.Integrity;

	[Definition("Course Container", "CourseContainer", "", "", 2000, Installer = InstallerHint.NeverRootOrStartPage)]
	[RestrictParents(typeof(IStructuralPage), typeof(IStorageItem))]
	[NotThrowable, NotVersionable]
	public class CourseContainer: AbstractContentPage
	{
		public CourseContainer()
		{
			this.Title = "Хранилище курсов";
		}

		public override string IconUrl { get { return "~/Lms/UI/Img/04/17.png"; } }

		public override string TemplateUrl { get { return "~/Templates/Secured/Go.aspx"; } }
	}
}
