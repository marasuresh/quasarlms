namespace N2.Lms.Items
{
	using N2.Definitions;
	using N2.Details;
	using N2.Edit.Trash;
	using N2.Installation;
	using N2.Persistence;
	
	[Definition("Course List", "Courses", "", "", 2000, Installer = InstallerHint.NeverRootOrStartPage)]
	[WithEditableTitle("Title", 10)]
	[ItemAuthorizedRoles(Roles = new string[0])]
	[NotThrowable, NotVersionable]
	public class CourseList: ContentItem
	{
		public override string IconUrl { get { return "~/Lms/UI/Img/04/17.png"; } }
	}
}
