using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N2.Lms.Items
{
	using N2.Workflow;
	using N2.Workflow.Items;
	using Lms.RequestStates;
	using TrainingWorkflow;
	
	partial class RequestContainer
	{
		#region Methods

		public Request SubscribeTo(Course course, string user, DateTime? begin, DateTime? end, string comment)
		{
			if (null == course) {
				throw new ArgumentException("course");
			}

			if (string.IsNullOrEmpty(user)) {
				throw new ArgumentException("user");
			}

			//var _that = N2.Context.Persister.Get<RequestContainer>(this.ID);

			if (this.MyActiveCourses.Any(_course => _course.ID == course.ID)) {
				throw new ArgumentException("You're already participating in course " + course.Title, "course");
			}

//TODO check if user is eligible for this course

			Request _request = N2.Context.Definitions.CreateInstance<Request>(this);
			
			_request.User = _request.SavedBy = user;
			_request.Title = _request.Name;
			_request.Course = course;
			
			_request.StartDate = begin ?? DateTime.Now;
			_request.RequestDate = end ?? DateTime.Now.AddDays(7);
			
			_request.Comments = comment;
			N2.Context.Persister.Save(_request);
			
			return _request;
		}
		
		#endregion Methods

		public virtual IEnumerable<Request> MyRequests { get { return GetChildren().OfType<Request>(); } }

        public IEnumerable<Request> MyApprovedRequests
        {
            get
            {
                return
                    from _request in this.MyRequests
                    let _currentState = _request.GetCurrentState() as ApprovedState
                    where null != _currentState
                    select _request;
            }
        }
        
        public IEnumerable<ApprovedState> MyApprovedApplications {
			get {
				return
					from _req in this.MyRequests
					let _currentState = _req.GetCurrentState() as ApprovedState
					where null != _currentState
					select _currentState;
			}
		}

		public IEnumerable<Request> MyPendingRequests {
			get {
				return
					from _req in this.MyRequests
					where _req.GetCurrentState().IsInitialState()
					select _req;
			}
		}

		/// <summary>
		/// Training sessions i am currently studying
		/// </summary>
		public IEnumerable<TrainingTicket> MyActiveTrainingTickets {
			get {
				return
					from _approvedState in this.MyApprovedApplications
					where null != _approvedState.Ticket
					select _approvedState.Ticket;
			}
		}

		/// <summary>
		/// Courses i'm currently involved in in any form
		/// </summary>
		internal IEnumerable<Course> MyActiveCourses {
			get {
				return (
					from _req in this.MyRequests
					let _currentState = _req.GetCurrentState()
					where
						"new,active,pending validation".Contains(_currentState.ToState.Title.ToLower())
						&& null != _req.Course
					select _req.Course
				).Distinct();
			}
		}

		/// <summary>
		/// Courses i've finished, awaiting grading by instructor
		/// </summary>
		public IEnumerable<Request> MyFinishedAssignments {
			get {
				return
					from _request in this.MyRequests
					let _currentState = _request.GetCurrentState()
					where string.Equals(_currentState.ToState.Title, "Pending Validation", StringComparison.InvariantCultureIgnoreCase)
					select _request;
			}
		}

        /// <summary>
        /// Courses i've finished and grading by instructor
        /// </summary>
        public IEnumerable<Request> MyGradedAssignments
        {
            get
            {
                return
                    from _request in this.MyRequests
                    let _currentState = _request.GetCurrentState()
                    where _currentState is AcceptedState && ((AcceptedState)_currentState).Grade != 1
                    select _request;
            }
        }
	}
}
