namespace N2.ACalendar
{
	using N2.Details;
	using N2.Installation;
	using N2.Integrity;
	using N2.Templates.Items;
	using N2.Web;

	[Definition("ACalendar Container", "ACalendarContainer", "", "", 2000, Installer = InstallerHint.NeverRootOrStartPage)]
	[WithEditableTitle("Title", 10)]
	//[ItemAuthorizedRoles(Roles = new string[0])]
	//[NotThrowable, NotVersionable]
	//[AllowedChildren(typeof(ACalendar))]
	[RestrictParents(typeof(IStructuralPage), typeof(N2.Lms.Items.IStorageItem))]
	public partial class ACalendarContainer : AbstractContentPage
	{
		#region System Properties
		
		public override string IconUrl { get { return "~/Lms/UI/Img/04/calendar.png"; } }
		public override string TemplateUrl { get { return "~/ACalendar/UI/ACalendarList.aspx"; } }
		//hide in menu
		public override bool Visible { get { return false; } }

		#endregion System Properties
	}
}
