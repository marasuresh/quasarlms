namespace N2.Lms.Items.Lms.RequestStates
{
	using System.Linq;
	using N2.Details;
	using N2.Integrity;
	using N2.Workflow.Items;
	using N2.Lms.Items.TrainingWorkflow;

	[RestrictParents(typeof(Request))]
	public partial class ApprovedState: ItemState, IWorkflowItemContainer
	{
		[EditableLink("Assign Training", 1, Required = true)]
		public Training Training {
			get { return this.GetDetail<Training>(
				"Training",
				null/*new Training { Title = "Warning: Training is unassigned!" }*/);
			}
			set { this.SetDetail<Training>("Training", value); }
		}

		public override bool IsPage
		{
			get
			{
				return true;
			}
		}

		#region IWorkflowItemContainer Members

		public Workflow Workflow { get { return this.Training.Workflow; } }

		#endregion
	}
}
