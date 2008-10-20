namespace N2.ACalendar
{
	using N2.Details;
	using N2.Installation;
	using N2.Integrity;
	using N2.Templates.Items;
	using N2.Web;

	/// <summary>
	/// User-bound academic calendar list for diplaying
	/// </summary>
	[Definition("ACalendar List", "ACalendarList", "", "", 2000, Installer = InstallerHint.NeverRootOrStartPage)]
	[RestrictParents(typeof(IStructuralPage))]
	[AllowedChildren(typeof(ACalendar))]
	public class ACalendarList : AbstractContentPage, ILink
	{
		#region System properties

		public override string IconUrl { get { return this.ACalendarContainer == null ? Icons.CalendarError : Icons.Calendar; } }
		
		public override string TemplateUrl { get { return "~/ACalendar/UI/ACalendarList.aspx"; } }

		#endregion System properties

		#region Lms Properties

		[EditableLink("ACalendar Container", 1,
			HelpTitle = "Select an item, which contains all calendars.",
			Required = true)]
		public ACalendarContainer ACalendarContainer {
			get { return this.GetDetail("ACalendarContainer") as ACalendarContainer; }
			set { this.SetDetail<ACalendarContainer>("ACalendarContainer", value); }
		}

		#endregion Lms Properties


		#region ILink Members

		string ILink.Contents { get { return this.Title; } }

		string ILink.Target { get { return string.Empty; } }

		string ILink.ToolTip {
			get { return this.ACalendarContainer == null
					? "AcalendarContainer is not set"
					: string.Empty;
			}
		}

		string ILink.Url { get { return this.Url; } }

		#endregion
	}
}