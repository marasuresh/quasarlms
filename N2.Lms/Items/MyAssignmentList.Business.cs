using System;
using System.Collections.Generic;
using System.Linq;

namespace N2.Lms.Items
{
	using N2.Web;

	[UriTemplate("learn/{attempt}", "~/Lms/UI/Player/Player.ascx")]
	[UriTemplate("review/{attempt}", "~/Lms/UI/Player/Player.ascx")]
	partial class MyAssignmentList
	{
		#region Lms collection properties

		/// <summary>
		/// Courses available to me and i'm not currently studying
		/// </summary>
		public IEnumerable<Course> MyAvailableCourses {
			get {
				return this.AllCourses.Except(this.RequestContainer.MyActiveCourses);
			}
		}

		/// <summary>
		/// All Courses i have access to according to Course item permission
		/// </summary>
		internal IEnumerable<Course> AllCourses {
			get {
				return
					this.CourseContainer
						.GetChildren(/*implicit filtering by current user*/)
						.OfType<Course>();
			}
		}

		#endregion Lms collection properties
	}
}
