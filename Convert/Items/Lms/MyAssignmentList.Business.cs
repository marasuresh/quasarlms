using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N2.Lms.Items
{
	using N2.Templates.Items;
	using N2.Integrity;
	using N2.Details;

	partial class MyAssignmentList
	{
		#region Lms collection properties

		/// <summary>
		/// Courses, i'm not currently studying
		/// </summary>
		public IEnumerable<Course> MyAvailableCourses {
			get {
				return this.AllCourses.Except(this.RequestContainer.MyActiveCourses);
			}
		}

		/// <summary>
		/// All Courses i have access to
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
