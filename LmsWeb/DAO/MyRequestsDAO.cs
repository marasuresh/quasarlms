using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using N2.Workflow;

namespace N2.Lms
{
    using N2.Lms.Items;

    [DataObject]
    public class MyRequestsDAO
    {
        public MyAssignmentList MyAssignmentList { get; private set; }

        public MyRequestsDAO(MyAssignmentList item)
        {
            this.MyAssignmentList = item;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public IEnumerable<Request> FindAll()
        {
            return this.MyAssignmentList.RequestContainer.MyPendingRequests;
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void CancelRequest(
                string UserName,
                int ID)
        {
            Request _request = N2.Context.Persister.Get<Request>(ID);

            _request.PerformGenericAction("Cancel", UserName, "Canceled");
        }
    }
}

