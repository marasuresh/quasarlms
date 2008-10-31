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
    public class TrainingsToGradeDAO
    {
        public MyAssignmentList MyAssignmentList { get; private set; }

        public TrainingsToGradeDAO(MyAssignmentList item)
        {
            this.MyAssignmentList = item;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public IEnumerable<Request> FindAll()
        {
            return this.MyAssignmentList.RequestContainer.MyFinishedAssignments;
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void GoRequest(
                int id,
                string command,
                string comments,
                string trainingID,
                string grade)
        {
            Request _request = N2.Context.Persister.Get<Request>(id);
            Training _training = N2.Context.Persister.Get<Training>(int.Parse(trainingID));

            string user = HttpContext.Current.User.Identity.Name;

            switch (command)
            {
                case "Accept":
                    _request.PerformAction(
                        "Accept",
                        user,
                        comments,
                        new Dictionary<string, object>{{
							"Grade", grade
						}});
                    break;
                case "Decline":
                    _request.PerformAction(
                        "Decline",
                        user,
                        comments,
                        new Dictionary<string, object> { { "Training", _training } });
                    break;
            }
        }
    }
}

