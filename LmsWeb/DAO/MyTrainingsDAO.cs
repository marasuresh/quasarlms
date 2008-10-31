using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using N2.Workflow;

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
		public IEnumerable<Request> FindAll()
		{
			return this.MyAssignmentList.RequestContainer.MyApprovedRequests;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public void GoRequest(
				int id,
				string comments)
		{
            Request _request = N2.Context.Persister.Get<Request>(id);
            
            string user = HttpContext.Current.User.Identity.Name;

            _request.PerformAction(
                    "Finish",
                    user,
                    comments,
                    null);
		}
	}
}
