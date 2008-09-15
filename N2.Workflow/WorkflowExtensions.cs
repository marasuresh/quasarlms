using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N2.Workflow
{
	using Items;
	
	public static class WorkflowExtensions
	{
		public static Workflow GetWorkflow(this ContentItem item)
		{
			if (item is IWorkflowItemContainer) {
				return ((IWorkflowItemContainer)item).Workflow;
			} else if (item.Parent != null && item.Parent is IWorkflowItemContainer) {
				return ((IWorkflowItemContainer)item.Parent).Workflow;
			} else {
				return null;
			}
		}

		public static void AssignCurrentState(this ContentItem item, ItemState state)
		{
			item["_CurrentState"] = state;
		}
	}
}
