namespace N2.Lms.Items.TrainingWorkflow
{
	using N2.Integrity;
	using N2.Details;
	using N2.Workflow;
	using N2.Lms.Items.Lms.RequestStates;
	
	[Definition(Description = @"Represent all training information, associated with a given student.")]
	[RestrictParents(typeof(Lms.RequestStates.ApprovedState))]
	[WithWorkflowAction(Name = "Workflow", SortOrder = 3)]
	[N2.Persistence.NotVersionable]
	public class TrainingTicket: ContentItem
	{
		#region System properties

		public override string IconUrl {
			get { return this.GetIconFromState(); }
		}
		
		#endregion System properties

		#region Lms properties

		public Training Training { get { return ((ApprovedState)this.Parent).Training; } }

		#endregion Lms properties
	}
}
