using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

namespace N2.Lms
{
	using N2.Lms.Items;
	using N2.Lms.Items.Lms.RequestStates;
	
	[DataObject]
	public class MyTrainingsDAO
	{
		public MyAssignmentList MyAssignmentList { get; private set; }

		public MyTrainingsDAO(MyAssignmentList item)
		{
			this.MyAssignmentList = item;
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public IEnumerable<ApprovedState> FindAll()
		{
			return this.MyAssignmentList.RequestContainer.MyApprovedApplications;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public void InsertRequest(
				int id,
				Object begin,
                Object end,
				string comments)
		{
			Course _course = N2.Context.Persister.Get<Course>(id);
			
			this.MyAssignmentList.RequestContainer.SubscribeTo(
				_course,
				HttpContext.Current.User.Identity.Name,
				(DateTime?)begin,
                (DateTime?)end,
				comments);
		}
	}
}
