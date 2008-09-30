namespace N2.Lms.Items.Lms.RequestStates
{
	using System.Linq;
	using N2.Details;
	using N2.Integrity;
	using N2.Workflow.Items;
	using N2.Lms.Items.TrainingWorkflow;

	[RestrictParents(typeof(Request))]
	public class ApprovedState: ItemState, IWorkflowItemContainer
	{
		[EditableLink("Assign Training", 1, Required = true)]
		public Training Training {
			get { return this.GetDetail<Training>("Training", null); }
			set { this.SetDetail<Training>("Training", value); }
		}

		TrainingTicket m_ticket;
		public TrainingTicket Ticket {
			get { return this.m_ticket ?? (this.m_ticket = this.GetChildren(
					new N2.Collections
						.TypeFilter(typeof(TrainingTicket))
				).FirstOrDefault() as TrainingTicket);
			}
		}

		#region IWorkflowItemContainer Members

		public Workflow Workflow { get { return this.Training.Workflow; } }

		#endregion

		#region System properties

		public override string IconUrl
		{
			get
			{
				return null != this.Ticket ? this.Ticket.IconUrl : base.IconUrl;
			}
		}
		
		#endregion System properties
	}
}
