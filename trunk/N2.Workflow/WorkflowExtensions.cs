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
			var _workflowContainer = Find.EnumerateParents(item).OfType<IWorkflowItemContainer>().FirstOrDefault();

			return null != _workflowContainer
				? _workflowContainer.Workflow
				: null;
		}

		public static void AssignCurrentState(this ContentItem item, ItemState state)
		{
			item["_CurrentState"] = state;
		}
	}
}
