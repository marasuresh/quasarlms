namespace N2.Lms.Items.Lms.RequestStates
{
	using System.Linq;
	using N2.Details;
	using N2.Integrity;
	using N2.Workflow.Items;
	using N2.Lms.Items.TrainingWorkflow;
	using N2.Web;

	[RestrictParents(typeof(Request))]
	public partial class ApprovedState: ItemState, IWorkflowItemContainer, ILink
	{
		[EditableLink("Assign Training", 1, Required = true)]
		public Training Training {
			get { return this.GetDetail<Training>(
				"Training",
				null/*new Training { Title = "Warning: Training is unassigned!" }*/);
			}
			set { this.SetDetail<Training>("Training", value); }
		}

		#region System properties
		
		public override string IconUrl { get { return null == this.Training ? Icons.Error : base.IconUrl; } }
		//public override bool IsPage { get { return true; } }
		public override string TemplateUrl { get { return "~/Lms/UI/Parts/MyAssignmentList/MyTrainings/BeginTraining.ascx"; } }

		#endregion System properties
		
		#region IWorkflowItemContainer Members

		public Workflow Workflow { get { return this.Training.Workflow; } }

		#endregion

		#region ILink Members

		string ILink.Contents { get { return this.Title; } }

		string ILink.Target { get { return string.Empty; } }

		string N2.Web.ILink.ToolTip {
			get { return null == this.Training ? "Training is not set" : string.Empty; }
		}

		string ILink.Url { get { return this.Url; } }

		#endregion
	}
}
