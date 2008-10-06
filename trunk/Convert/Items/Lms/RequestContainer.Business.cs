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
		public IEnumerable<ApprovedState> MyApprovedApplications {
			get {
				return
					from _req in this.GetChildren(/*filtered by current user*/).OfType<Request>()
					let _currentState = _req.GetCurrentState() as ApprovedState
					where null != _currentState
					select _currentState;
			}
		}

		public IEnumerable<Request> MyPendingRequests {
			get {
				return
					from _req in this.GetChildren(/*filtered by current user*/).OfType<Request>()
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
					from _request in this.GetChildren(/*filtered by current user*/).OfType<Request>()
					let _currentState = _request.GetCurrentState() as ApprovedState
					where _currentState != null
					select _currentState.Ticket;
			}
		}

		/// <summary>
		/// Courses i'm currently involved in in any form
		/// </summary>
		internal IEnumerable<Course> MyActiveCourses {
			get {
				return (
					from _req in this.GetChildren(/*filtered by current user*/).OfType<Request>()
					let _currentState = _req.GetCurrentState()
					where !_currentState.IsFinalState()
					select _req.Course
				).Distinct();
			}
		}
	}
}
