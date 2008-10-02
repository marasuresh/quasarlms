using System;
using System.Web.UI;
using System.Diagnostics;

namespace N2.Details
{
	using N2.Web.UI;
	using N2.Web.UI.WebControls;
	using N2.Workflow;
	using N2.Workflow.Items;

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class WithWorkflowAuditTrailAttribute: AbstractEditableAttribute
	{
		protected override Control AddEditor(Control container)
		{
			var _toolBar = new AuditTrail { ID = this.Name };
			_toolBar.ParentItem = ItemUtility.FindInParents<IItemEditor>(container).CurrentItem;
			container.Controls.Add(_toolBar);
			return _toolBar;
		}

		public override void UpdateEditor(ContentItem item, Control editor)
		{
			((AuditTrail)editor).ParentItem = item;
		}

		public override bool UpdateItem(ContentItem item, Control editor)
		{
			return false;
		}
	}
}
