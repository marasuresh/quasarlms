namespace N2.ACalendar
{
    using N2.Definitions;
    using N2.Details;
    using N2.Edit.Trash;
    using N2.Installation;
    using N2.Persistence;
    using N2.Integrity;
    using N2.Templates.Items;

    [Definition("ACalendar Container", "ACalendarContainer", "", "", 2000, Installer = InstallerHint.NeverRootOrStartPage)]
    [WithEditableTitle("Title", 10)]
    //[ItemAuthorizedRoles(Roles = new string[0])]
    [NotThrowable, NotVersionable]
    [AllowedChildren(typeof(ACalendar))]
    [RestrictParents(typeof(IStructuralPage))]
    public class ACalendarContainer : ContentItem
    {
        public override string IconUrl { get { return "~/Lms/UI/Img/04/calendar.png"; } }
        public override bool IsPage { get { return false; } }
    }
}
