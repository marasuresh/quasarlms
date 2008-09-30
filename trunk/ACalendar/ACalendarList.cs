namespace N2.ACalendar
{
    using System;
    using N2.Definitions;
    using N2.Details;
    using N2.Edit.Trash;
    using N2.Installation;
    using N2.Persistence;
    using N2.Integrity;
    using N2.Templates.Items;

    /// <summary>
    /// User-bound academic calendar list for diplaying
    /// </summary>
    [Definition("ACalendar List", "ACalendarList", "", "", 2000, Installer = InstallerHint.NeverRootOrStartPage)]
    //[WithEditableTitle("Title", 10)]
    [RestrictParents(typeof(IStructuralPage))]
    public class ACalendarList : AbstractContentPage
    {
        public override string IconUrl { get { return "~/Lms/UI/Img/04/calendar.png"; } }

        public override string TemplateUrl { get { return "~/ACalendar/UI/ACalendarList.aspx"; } }

        #region Lms Properties

        [EditableLink("ACalendar Container",  1, HelpTitle="Select an item, which contains all calendars.")]
        public ACalendarContainer ACalendarContainer
        {
            get { return this.GetDetail("ACalendarContainer") as ACalendarContainer; }
            set { this.SetDetail<ACalendarContainer>("ACalendarContainer", value); }
        }

        #endregion Lms Properties
    }
}