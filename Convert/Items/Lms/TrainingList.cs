using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace N2.Lms.Items
{
	using N2.Collections;
	using N2.Details;
	using N2.Installation;
	using N2.Integrity;
	using N2.Templates.Items;
	using N2.Workflow;
	using N2.Lms.Items.Lms.RequestStates;
	using N2.Lms.Items.TrainingWorkflow;

	[Definition("Training List", "TrainingList", "", "", 2000, Installer = InstallerHint.NeverRootOrStartPage)]
	[WithEditableTitle("Title", 10)]
	[RestrictParents(typeof(IStructuralPage))]
	public class TrainingList: CourseList
	{
		#region System properties
		
		public override string IconUrl { get { return "~/Lms/UI/Img/04/20.png"; } }
		public override string TemplateUrl { get {
            return this.MyStartedTrainings != null && this.MyStartedTrainings.Any()
				? "~/Lms/UI/Player.aspx"
				: "~/Lms/UI/TrainingList.aspx"; } }
		
		#endregion System properties

		#region Lms list properties

		

		IEnumerable<TrainingTicket> m_myStartedTrainings;
		public IEnumerable<TrainingTicket> MyStartedTrainings {
			get {
				return
					this.m_myStartedTrainings
					?? (this.m_myStartedTrainings = this.GetMyStartedTrainings());
			}
		}

		IEnumerable<TrainingTicket> GetMyStartedTrainings()
		{
			return
				from _approvedApplication in this.RequestContainer.MyApprovedApplications
				let _ticket = _approvedApplication.Ticket
				where _ticket != null
				let _ticketState = _ticket.GetCurrentState()
				where string.Equals(
					_ticketState.ToState.Name,
					"new",
					System.StringComparison.OrdinalIgnoreCase)
				select _ticket;
		}
		
		#endregion

#if LegacyBusinessLogic

		static AccessFilter FindAccessFilter(ItemFilter filter)
		{
			if (filter is CompositeFilter) {
				foreach (var _flt in (filter as CompositeFilter).Filters) {
					var _result = FindAccessFilter(_flt);
					if (_result is AccessFilter) {
						return _result as AccessFilter;
					}
				}
			} else if (filter is AccessFilter) {
				return filter as AccessFilter;
			} 
			return default(AccessFilter);
		}

		public IEnumerable<Lms.RequestStates.ApprovedState> GetApprovedRequests(string userName)
		{
			var _requestQuery =
				from _request in this.RequestContainer.Children.OfType<Request>()
				let _approvedRequest = _request.GetCurrentState() as Lms.RequestStates.ApprovedState
				where _approvedRequest != null
				select _approvedRequest;

			if (!string.IsNullOrEmpty(userName)) {
				_requestQuery = _requestQuery.Where(_r => _r.SavedBy == userName);
			}

			return _requestQuery;
		}

		Training TranslateToCurrentLanguage(Training training)
		{
			return training;
			var _lm = N2.Context.Current.Resolve<N2.Engine.Globalization.ILanguageGateway>();
			var _currentLanguage = _lm.GetLanguage(this);
			return _lm.GetTranslation(training, _currentLanguage) as Training;
		}

		public override ItemList GetChildren(ItemFilter filter)
		{
			Trace.WriteLine("Training List", "Lms");
			
			var items = base.GetChildren(filter);

			if (null == this.RequestContainer) {
				return items;
			}
			
			AccessFilter _af = FindAccessFilter(filter);
			IEnumerable<Training> _trainings = new Training[0];
			
			if (null != _af) {
				Trace.WriteLine("Access filter found: " + _af.User.Identity.Name, "Lms");
				_trainings = GetApprovedRequests(_af.User.Identity.Name).Select(
					_r => this.TranslateToCurrentLanguage(_r.Training));
			} else if(filter is NullFilter) {
				Trace.WriteLine("NullFilter", "Lms");
				_trainings = GetApprovedRequests(null).Select(
					_r => this.TranslateToCurrentLanguage(_r.Training));
			} else if (filter is CompositeFilter) {
				Trace.WriteLine("Composite filter: " +
					string.Join(
						",",
						(filter as CompositeFilter)
							.Filters
							.Select(_f => _f.GetType().Name)
							.ToArray()
					), "Lms");
			} else {
				Trace.WriteLine("Other filter(s): " + filter.GetType().Name, "Lms");
			}
			
			if (_trainings.Any()) {

				Trace.WriteLine("Trainings found: " + _trainings.Count().ToString(), "Lms");

				var virtualItems = _trainings;
				var parent = this;
				var _newItems = new ItemList(virtualItems.Cast<ContentItem>(), filter);

				foreach (ContentItem _i in _newItems) {
					var _virtualItem = _i.Clone(true);
					((Training)_virtualItem).OriginalCourse = ((Training)_i).Course;
					_virtualItem.Parent = parent;
					items.Add(_virtualItem);
				}
			}
			
			return items;
		}
#endif
	}
}