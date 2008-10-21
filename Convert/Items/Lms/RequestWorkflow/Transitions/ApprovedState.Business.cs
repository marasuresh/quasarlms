using System.Diagnostics;

namespace N2.Lms.Items.Lms.RequestStates
{
	using System.Linq;
	using N2.Details;
	using N2.Integrity;
	using N2.Collections;
	using N2.Workflow.Items;
	using N2.Lms.Items.TrainingWorkflow;

	partial class ApprovedState
	{
		/// <summary>
		/// The fact of participation in a training.
		/// </summary>
		TrainingTicket m_ticket;
		public TrainingTicket Ticket {
			get { return this.m_ticket ?? (this.m_ticket = this.EnsureTicket()); }
		}

		TrainingTicket EnsureTicket()
		{
			TrainingTicket _ticket = this.GetChild("ticket") as TrainingTicket;

			if (null == _ticket) {

				if (null != this.Training) {
					_ticket = this.GetChildren(new TypeFilter(typeof(TrainingTicket))).Cast<TrainingTicket>().FirstOrDefault();

					if (null == _ticket) {
						_ticket = N2.Context.Definitions.CreateInstance<TrainingTicket>(this);
						_ticket.Name = "ticket";
						_ticket.Title = this.Training.Title;
						N2.Context.Persister.Save(_ticket);
					}
				} else {
					Trace.TraceError("Training is not asigned to state {0}", this.ID);
				}
			}

			return _ticket;
		}
	}
}
