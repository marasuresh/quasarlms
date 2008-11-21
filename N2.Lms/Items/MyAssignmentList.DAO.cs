using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Web;

namespace N2.Lms.Items
{
	using N2.Workflow;
	
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

			_request.PerformGenericAction("Cancel", UserName, "Canceled");
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
		public void GoRequest(
				int id,
				string comments)
		{
			Request _request = Context.Persister.Get<Request>(id);

			string user = HttpContext.Current.User.Identity.Name;

			_request.PerformAction(
					"Finish",
					user,
					comments,
					null);
		}

		#endregion My Trainings

		#region Trainings To Grade

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public IEnumerable<Request> FindRequestsToGrade()
		{
			return this.RequestContainer.MyFinishedAssignments.ToArray();
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

			switch (command) {
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

		#endregion Trainings To Grade

		#region Requests To Approve

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public IEnumerable<Request> FindRequestsToApprove()
		{
			return this.RequestContainer.MyPendingRequests.ToArray();
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

			switch (command) {
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

		#endregion Requests To Approve

		#region My Graded Trainings

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public IEnumerable<Request> FindMyGradedRequests()
		{
			return this.RequestContainer.MyGradedAssignments.ToArray();
		}

		#endregion My Graded Trainings
	}
}
