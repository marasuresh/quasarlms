using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
    using N2.Details;
    using N2.Installation;
    using N2.Integrity;
    using N2.Templates.Items;
    using N2.Web;
    using N2.Lms.Items;


namespace N2.ACalendar
{


    /// <summary>
    /// User-bound academic calendar list for diplaying
    /// </summary>
    [Definition("Curriculum", "Curriculum", "", "", 2000, Installer = InstallerHint.NeverRootOrStartPage)]
//    [RestrictParents(typeof(IStructuralPage))]
//    [AllowedChildren(typeof(ACalendar))]
    public class Curriculum : AbstractContentPage, ILink
    {
        #region System properties

        public override string IconUrl { get { return this.CourseContainer == null ? Icons.CalendarError : Icons.Calendar; } }

        public override string TemplateUrl { get { return "~/Curriculum/UI/Views/CurriculumList.aspx"; } }

        #endregion System properties

        #region Lms Properties

        [EditableLink("Course Container", 1,
            HelpTitle = "Select an item, which contains all Courses.",
            Required = true)]
        public CourseContainer CourseContainer
        {
            get { return this.GetDetail("CourseContainer") as CourseContainer; }
            set { this.SetDetail<CourseContainer>("CourseContainer", value); }
        }

        #endregion Lms Properties


        #region ILink Members

        string ILink.Contents { get { return this.Title; } }

        string ILink.Target { get { return string.Empty; } }

        string ILink.ToolTip
        {
            get
            {
                return this.CourseContainer == null
                  ? "AcalendarContainer is not set"
                  : string.Empty;
            }
        }

        string ILink.Url { get { return this.Url; } }

        #endregion
    }
}