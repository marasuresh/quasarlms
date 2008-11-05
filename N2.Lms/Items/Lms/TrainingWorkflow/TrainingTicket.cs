namespace N2.Lms.Items.TrainingWorkflow
{
	using N2.Integrity;
	using N2.Details;
	using N2.Workflow;
	using N2.Lms.Items.Lms.RequestStates;
	using N2.Persistence;
	using N2.Edit.Trash;

	using N2.Templates.Items;

	[Definition(Description = @"Represent all training information, associated with a given student.")]
	[RestrictParents(typeof(Lms.RequestStates.ApprovedState))]
	[WithWorkflowAction(Name = "Workflow", SortOrder = 3)]
	[NotVersionable, NotThrowable]
	[WithWorkflowAuditTrail(Name = "Audit Trail")]
	public partial class TrainingTicket: AbstractContentPage
	{
		#region System properties

		public override string IconUrl {
			get { return null == this.Training ? Icons.Error : this.GetIconFromState(); }
		}

		public override string TemplateUrl {
			get { return "~/Templates/UI/Parts/Empty.ascx"; }
		}

		public override bool IsPage { get { return false; } }
		
		#endregion System properties

		#region Lms properties

		public Training Training { get { return
			((ApprovedState)this.Parent).Training; }
		}

		#endregion Lms properties
	}
}
