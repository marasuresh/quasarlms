namespace N2.Lms.Items
{
	using N2.Definitions;
	using N2.Details;
	using N2.Edit.Trash;
	using N2.Installation;
	using N2.Integrity;
	using N2.Persistence;
	using N2.Templates.Items;
	
	[Definition("Training List", "TrainingList", "", "", 2000, Installer = InstallerHint.NeverRootOrStartPage)]
	[WithEditableTitle("Title", 10)]
	[RestrictParents(typeof(IStructuralPage))]
	public class TrainingList: AbstractContentPage
	{
		public override string IconUrl { get { return "~/Lms/UI/Img/04/20.png"; } }

		public override string TemplateUrl { get { return "~/Lms/UI/TrainingList.aspx"; } }
	}
}
