using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

namespace N2.Lms
{
    using N2.Lms.Items;

    [DataObject]
    public class MyGradedTrainingsDAO
    {
        public MyAssignmentList MyAssignmentList { get; private set; }

        public MyGradedTrainingsDAO(MyAssignmentList item)
		{
			this.MyAssignmentList = item;
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public IEnumerable<Request> FindAll()
		{
			return this.MyAssignmentList.RequestContainer.MyGradedAssignments;
		}
    }
}
