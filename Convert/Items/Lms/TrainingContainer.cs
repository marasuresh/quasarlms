namespace N2.Lms.Items
{
	using N2.Definitions;
	using N2.Details;
	using N2.Edit.Trash;
	using N2.Installation;
	using N2.Integrity;
	using N2.Persistence;
	using N2.Templates.Items;

	[Definition("Training Container", "TrainingContainer", "", "", 2000, Installer = InstallerHint.NeverRootOrStartPage)]
	[WithEditableTitle("Title", 10)]
	[ItemAuthorizedRoles(Roles = new string[0])]
	[RestrictParents(typeof(Course))]
	[AllowedChildren(typeof(Training))]
	public class TrainingContainer: AbstractItem
	{
		public override string IconUrl { get { return "~/Lms/UI/Img/04/20.png"; } }

		public override bool IsPage { get { return false; } }
	}
}
