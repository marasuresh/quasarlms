using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using N2.Definitions;
using N2.Details;
using N2.Edit.Trash;
using N2.Integrity;
using N2.Lms.Items;
using N2.Persistence;
using N2.Templates;
using N2.Templates.Items;
using N2.Workflow;


namespace N2.ACalendar.Reporting
{
    [Definition("Report", "Report")]
    [NotThrowable, NotVersionable]
    [RestrictParents(typeof(IStructuralPage))]

    public class Report: AbstractContentPage
    {
        public override string TemplateUrl { get { return "~/Reporting/UI/Views/Report.aspx"; } }


        #region Lms properties

        [EditableLink(
            "Course Container", 03,
            Required = true)]
        public CourseContainer CourseContainer
        {
            get { return this.GetDetail("CourseContainer") as CourseContainer; }
            set { this.SetDetail<CourseContainer>("CourseContainer", value); }
        }

        [EditableLink(
            "Request Container", 05,
            Required = true)]
        public RequestContainer RequestContainer
        {
            get { return this.GetDetail("RequestContainer") as RequestContainer; }
            set { this.SetDetail<RequestContainer>("RequestContainer", value); }
        }

        #endregion Lms properties

        #region Lms collection properties

 
        /// <summary>
        /// All Courses i have access to
        /// </summary>
        internal IEnumerable<Course> AllCourses
        {
            get
            {
                return
                    this.CourseContainer
                        .GetChildren(/*implicit filtering by current user*/)
                        .OfType<Course>();
            }
        }

        #endregion Lms collection properties

    
    }
}
