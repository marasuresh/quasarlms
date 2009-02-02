namespace N2.Lms.Items
{
	using Definitions;
	using Details;
	using Installation;
	using Integrity;

	[Definition("Training Container", "TrainingContainer", "", "", 2000, Installer = InstallerHint.NeverRootOrStartPage)]
	[WithEditableTitle("Title", 10)]
	[ItemAuthorizedRoles(Roles = new string[0])]
	[RestrictParents(typeof(Course))]
	[AllowedChildren(typeof(Training))]
	[Disable]
	public class TrainingContainer: ItemContainer<Training>
	{
		public TrainingContainer()
		{
			this.Title = "Trainings";
		}

		#region System properties
		
		public override string IconUrl { get { return "~/Lms/UI/Img/04/20.png"; } }
		public override bool IsPage { get { return false; } }

		#endregion System properties
	}
}
