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
	public class WithWorkflowActionAttribute: AbstractEditableAttribute
	{
		public WithWorkflowActionAttribute()
		{
			this.HelpTitle = "Select action to execute and click button";
			this.HelpText = @"This control allows you to execute a workflow action, aplicable for the current state of this item.
<ol><li>Select available action to execute, click button &mdash; action edit form will appear.</li>
	<li>Edit action properties</li>
	<li>The action will be executed when you click Save button for the whole item.</li></ol>
If you want to cancel action if item was not yet saved, press red button &mdash; action edit form will disappear.";
		}
		
		protected override Control AddEditor(Control container)
		{
			var _toolBar = new WorkflowActionToolbar { ID = this.Name };
			_toolBar.ParentItem = ItemUtility.FindInParents<IItemEditor>(container).CurrentItem;
			container.Controls.Add(_toolBar);
			return _toolBar;
		}

		public override void UpdateEditor(ContentItem item, Control editor)
		{
			((WorkflowActionToolbar)editor).ParentItem = item;
		}

		public override bool UpdateItem(ContentItem item, Control editor)
		{
			var _tb = editor as WorkflowActionToolbar;

			if (_tb.WorkflowActionPerformed) {
				ItemEditor childEditor = _tb.ItemEditor;

				childEditor.Update();
				IItemEditor parentEditor = ItemUtility.FindInParents<IItemEditor>(editor.Parent);

				//Perform *after* parent was saved
				parentEditor.Saved += (sender, e) => {
					//get just saved concrete child
					ItemState _state = childEditor.Save() as ItemState;
					//if child is not equal current parent's state..
					if (_state != e.AffectedItem.GetCurrentState()) {
						//push new state to Details collection
						e.AffectedItem.AssignCurrentState(_state);
						//persist parent second time with state detail updated
						Engine.Persister.Save(e.AffectedItem);
					}
				};
				//notify parent was updated
				return true;
			} else {
				return false;
			}
		}

		static TraceSource TraceSource = new TraceSource("Workflow");
	}
}
