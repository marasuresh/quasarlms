using System;
using System.Web.UI;
using System.Diagnostics;

namespace N2.Details
{
	using N2.Web.UI.WebControls;
	using N2.Workflow;

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class WithWorkflowActionAttribute: AbstractEditableAttribute
	{
		protected override Control AddEditor(Control container)
		{
			var _toolBar = new WorkflowActionToolbar { ID = this.Name };
			container.Controls.Add(_toolBar);
			return _toolBar;
		}

		public override void UpdateEditor(ContentItem item, Control editor)
		{
			((WorkflowActionToolbar)editor).CurrentItem = item;
		}

		public override bool UpdateItem(ContentItem item, Control editor)
		{
			WorkflowActionToolbar _tb = editor as WorkflowActionToolbar;

			if (!string.IsNullOrEmpty(_tb.SelectedAction)) {

				TraceSource.TraceInformation("Selected Action: {0}", _tb.SelectedAction);

				item.PerformAction(
					_tb.SelectedAction,
					System.Web.HttpContext.Current.User.Identity.Name,
					_tb.Comment);

				return true;
			} else {
				return false;
			}
		}

		static TraceSource TraceSource = new TraceSource("Workflow");
	}
}
