using System.Web.UI.WebControls;

namespace N2.Lms.Web.UI
{
	using Items;
	using Templates.Items;
	using Templates.Web.UI;
	using Items.TrainingWorkflow;
	using Items.Lms.RequestStates;
	using Workflow;
	using Workflow.Items;
	
	public class MyAssignmentListControl: TemplateUserControl<AbstractContentPage, MyAssignmentList>
	{
		protected void ds_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
		{
			e.ObjectInstance = this.CurrentItem ?? this.FallbackCurrentItem;
		}

		/// <summary>
		/// Fix for an obscure bug in N2 which prevents from getting CurrentItem property of control,
		/// which was instantiated not by N2 itself.
		/// </summary>
		protected MyAssignmentList FallbackCurrentItem {
			get {
				return ((TemplateUserControl<AbstractContentPage, MyAssignmentList>)this
					.NamingContainer//Wizard
					.NamingContainer//MyAssignmentList
				).CurrentItem;
			}
		}

		#region Rendering helpers

		protected string GetPlayerUrl(Request request)
		{
			var _attempt = this.WorkflowProvider.GetCurrentState(request) as ApprovedState;
			return this.GetPlayerUrl(_attempt.Ticket);
		}

		public string GetPlayerUrl(TrainingTicket attempt)
		{
			return
				string.Concat(
					"<a target='_blank' href='",
					this.ResolveClientUrl("~/Lms/UI/Player.aspx?id=" + attempt.ID.ToString()),
					"'>",
					attempt.Title,
					"</a>");
		}
		
		#endregion

		protected IItemState GetRequestState(Request request)
		{
			return
				this.WorkflowProvider.GetCurrentState(request);
		}

		IWorkflowProvider m_wfp;
		//Simplified access for a WorkflowProvider instance
		protected IWorkflowProvider WorkflowProvider {
			get {
				return
					this.m_wfp
					?? (this.m_wfp = N2.Context.Current.Resolve<IWorkflowProvider>());
			}
		}
	}
}
