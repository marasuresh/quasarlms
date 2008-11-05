using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using N2.Workflow;

namespace N2.Lms
{
    using N2.Lms.Items;

    [DataObject]
    public class RequestsDAO
    {
        public MyAssignmentList MyAssignmentList { get; private set; }

        public RequestsDAO(MyAssignmentList item)
        {
            this.MyAssignmentList = item;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public IEnumerable<Request> FindAll()
        {
            return this.MyAssignmentList.RequestContainer.MyPendingRequests;
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void GoRequest(
                string command,
                string trainingID,
                string comments,
                int ID)
        {
            Request _request = N2.Context.Persister.Get<Request>(ID);
            Training _training = N2.Context.Persister.Get<Training>(int.Parse(trainingID));

            string user = HttpContext.Current.User.Identity.Name;

            switch (command)
            {
                case "Accept":
                    _request.PerformAction(
                    "Approve",
                    user,
                    comments,
                    new Dictionary<string, object> { { "Training", _training } });
                    break;
                case "Reject":
                    _request.PerformAction(
                    "Cancel",
                    user,
                    comments,
                    null);
                    break;
            }
            //_request.PerformGenericAction("Cancel", UserName, "Canceled");
        }
    }
}


