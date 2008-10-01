using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N2.Workflow
{
	using N2.Workflow.Items;

	public static class WorkflowPublicExtensions
	{
		public static string GetIconFromState(this ContentItem item)
		{
			var _state = item.GetCurrentState();
			
			if(null == _state || null == _state.ToState) {
				return null;
			}

			return _state.ToState.IconUrl;
		}

		public static ItemState GetCurrentState(this ContentItem item)
		{
			var _cs = item.GetDetail("_CurrentState") as ItemState;

			if (null != _cs) {
				return _cs;
			} else {
				//try to fix broken item link
				item.Details.Remove("_CurrentState");
				
				return new ItemState {
					Action = null,
					FromState = null,
					ToState = item.GetWorkflow().InitialState,
					Comment = string.Empty,
				};
			}
		}

		public static ItemState PerformAction(
			this ContentItem item,
			ActionDefinition action,
			string userName,
			string comment)
		{
			var _wm = new WorkflowManager(
				Context.Current.Persister,
				Context.Current.Definitions);
			
			return _wm.PerformAction(item, action, userName, comment);
		}

		public static ItemState PerformAction(
			this ContentItem item,
			string action,
			string userName,
			string comment)
		{
			var _wm = new WorkflowManager(
				Context.Current.Persister,
				Context.Current.Definitions);

			return _wm.PerformAction(item, action, userName, comment);
		}
	}
}
