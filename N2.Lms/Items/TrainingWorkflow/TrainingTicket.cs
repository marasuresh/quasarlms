namespace N2.Lms.Items.TrainingWorkflow
{
	using Edit.Trash;
	using Integrity;
	using Lms.RequestStates;
	using Persistence;
	using Templates.Items;
	using Workflow;

	[Definition(Description = @"Represent all training information, associated with a given student.")]
	[RestrictParents(typeof(Lms.RequestStates.ApprovedState))]
	[NotVersionable, NotThrowable]
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
