using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

namespace N2.Lms
{
	using N2.Lms.Items;
	
	[DataObject]
	public class MyCoursesDAO
	{
		public MyAssignmentList MyAssignmentList { get; private set; }

		public MyCoursesDAO(MyAssignmentList item)
		{
			this.MyAssignmentList = item;
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public IEnumerable<Course> FindAll()
		{
			return this.MyAssignmentList.MyAvailableCourses;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public void InsertRequest(
				int courseId,
				DateTime? begin,
				DateTime? end,
				string comments)
		{
			Course _course = N2.Context.Persister.Get<Course>(courseId);
			
			this.MyAssignmentList.RequestContainer.SubscribeTo(
				_course,
				HttpContext.Current.User.Identity.Name,
				begin,
				end,
				comments);
		}
	}
}
