using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace N2.Lms.Items
{
	using Workflow;
	using Workflow.Items;
	
	[DataObject]
	partial class MyAssignmentList
	{
		#region MyPendingRequest
		
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public IEnumerable<Request> FindPendingRequests()
		{
			return this.RequestContainer.MyPendingRequests.ToArray();
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public void CancelRequest(
				string UserName,
				int ID)
		{
			Request _request = N2.Context.Persister.Get<Request>(ID);

			this.WorkflowProvider.PerformAction(_request,
				"Cancel", new {
					SavedBy = UserName,
					Comment = "Canceled",
				});
		}

		#endregion MyPendingRequest

		#region My Courses

		public class CourseViewData
		{
			public int Id { get; internal set; }
			public string Title { get; internal set; }
			public bool IsRequired { get; internal set; }
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public IEnumerable<CourseViewData> FindMyCourses()
		{
			return (
				from _course in this.MyAvailableCourses
				join _curriculumCourse in this.CourseContainer.MyCurriculumCourses
					on _course.ID equals _curriculumCourse.Id
				where _curriculumCourse.Status > 0
				select new CourseViewData {
					Id = _course.ID,
					Title = _course.Title,
					IsRequired = 1 == _curriculumCourse.Status,
				}).ToArray();
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public void InsertRequest(
				int id,
				Object begin,
				Object end,
				string comments)
		{
			Course _course = N2.Context.Persister.Get<Course>(id);

			this.RequestContainer.SubscribeTo(
				_course,
				HttpContext.Current.User.Identity.Name,
				(DateTime?)begin,
				(DateTime?)end,
				comments);
		}
		
		#endregion My Courses

		#region My Trainings

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public IEnumerable<Request> FindApprovedRequests()
		{
			return this.RequestContainer.MyApprovedRequests.ToArray();
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public void SubmitTraining(
				int id,
				string comments)
		{
			Request _request = Context.Persister.Get<Request>(id);

			string user = HttpContext.Current.User.Identity.Name;

			this.WorkflowProvider.PerformAction(_request,
				"Finish", new {
					SavedBy = user,
					Comment = comments,
				});
		}

		#endregion My Trainings

		#region Trainings To Grade

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public IEnumerable<Request> FindRequestsToGrade()
		{
			return this.RequestContainer.MyFinishedAssignments.ToArray();
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public void GradeRequest(
				int id,
				string command,
				string comments,
				int trainingID,
				int grade)
		{
			Request _request = N2.Context.Persister.Get<Request>(id);
			
			string user = HttpContext.Current.User.Identity.Name;

			switch (command) {
				case "Accept":
					this.WorkflowProvider.PerformAction(_request,
						"Accept", new {
							SavedBy = user,
							Comment = comments,
							Grade = grade
						});
					break;
				case "Decline":
					Training _training = N2.Context.Persister.Get<Training>(trainingID);
					this.WorkflowProvider.PerformAction(_request,
						"Decline", new {
								SavedBy = user,
								Comment = comments,
								Training = _training,
						});
					break;
			}
		}

		#endregion Trainings To Grade

		#region Requests To Approve

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public IEnumerable<Request> FindRequestsToApprove()
		{
			return this.RequestContainer.MyPendingRequests.ToArray();
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public void ApproveRequest(
				string command,
				string trainingID,
				string comments,
				int ID)
		{
			Request _request = N2.Context.Persister.Get<Request>(ID);
			Training _training = N2.Context.Persister.Get<Training>(int.Parse(trainingID));

			string user = HttpContext.Current.User.Identity.Name;

			switch (command) {
				case "Accept":
					this.WorkflowProvider.PerformAction(_request,
						"Approve", new {
							SavedBy = user,
							Comment = comments,
							Training = _training,
						});
					break;
				case "Reject":
					this.WorkflowProvider.PerformAction(_request,
						"Cancel", new {
							SavedBy = user,
							Comment = comments,
						});
					break;
			}
			//_request.PerformGenericAction("Cancel", UserName, "Canceled");
		}

		#endregion Requests To Approve

		#region My Graded Trainings

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public IEnumerable<Request> FindMyGradedRequests()
		{
			return this.RequestContainer.MyGradedAssignments.ToArray();
		}

		#endregion My Graded Trainings

		IWorkflowProvider m_wfp;
		IWorkflowProvider WorkflowProvider {
			get { return this.m_wfp ?? (this.m_wfp = Context.Current.Resolve<IWorkflowProvider>()); }
		}
	}
}
