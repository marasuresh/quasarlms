using System.Diagnostics;
using System.Linq;

namespace N2.Lms.Items.Lms.RequestStates
{
	using Collections;
	using TrainingWorkflow;

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
						_ticket = Context.Definitions.CreateInstance<TrainingTicket>(this);
						_ticket.Name = "ticket";
						_ticket.Title = this.Training.Title;
						Context.Persister.Save(_ticket);
					}
				} else {
					Trace.TraceError("Training is not asigned to state {0}", this.ID);
				}
			}

			return _ticket;
		}
	}
}
