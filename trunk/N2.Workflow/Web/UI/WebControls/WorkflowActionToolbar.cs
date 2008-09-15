using System.Diagnostics;

namespace N2.Web.UI.WebControls
{
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using N2.Web.UI;
	using N2.Workflow;
	using N2.Workflow.Items;
	using System.Linq;
	
	public class WorkflowActionToolbar: Control, IWorkflowActionControl
	{
		public ContentItem CurrentItem { get; set; }

		public string SelectedAction {
			get { return this.m_ctl.SelectedAction; }
		}

		public string Comment {
			get { return this.m_ctl.Comment; }
		}

		IWorkflowActionControl m_ctl;

		protected override void OnInit(System.EventArgs e)
		{
			base.OnInit(e);
			this.EnsureChildControls();
		}

		protected override void CreateChildControls()
		{
			base.CreateChildControls();

			Control _ctl = this.Page.LoadControl("~/Workflow/Web/WorkflowAction.ascx");
			this.m_ctl = _ctl as IWorkflowActionControl;
			this.Controls.Add(_ctl);
		}
	}
}
