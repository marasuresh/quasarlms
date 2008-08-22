namespace N2.Lms.Items
{
	using N2.Definitions;
	using N2.Details;
	using N2.Edit.Trash;
	using N2.Installation;
	using N2.Integrity;
	
	[Definition("Training List", "Trainings", "", "", 2000, Installer = InstallerHint.NeverRootOrStartPage)]
	[WithEditableTitle("Title", 10)]
	[RestrictParents(typeof(Course))]
	[AllowedChildren(typeof(Training))]
	[NotThrowable]
	public class TrainingList: ContentItem
	{
		public override string IconUrl { get { return "~/Lms/UI/Img/04/20.png"; } }

		public override bool IsPage { get { return true; } }
	}
}
