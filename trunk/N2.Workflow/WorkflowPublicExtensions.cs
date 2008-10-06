using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N2.Workflow
{
	using System.Diagnostics;
	using N2.Workflow.Items;

	public static class WorkflowPublicExtensions
	{
		/// <summary>
		/// Get Icon for the item depending on it's Workflow state.
		/// Ensure that state is assigned, thus should not throw NullReferenceException.
		/// </summary>
		/// <param name="item"></param>
		/// <returns>String of unresolved URL</returns>
		public static string GetIconFromState(this ContentItem item)
		{
			var _state = item.GetCurrentState();
			
			if(null == _state || null == _state.ToState) {
				return null;
			}

			return _state.ToState.IconUrl;
		}

		public static bool IsFinalState(this ItemState itemState)
		{
			return !itemState.ToState.Actions.Any()
				|| (1 == itemState.ToState.Actions.Count()
					&& itemState.ToState.Actions.First().LeadsTo == ((Workflow)itemState.ToState.Parent).InitialState
					);
		}

		public static bool IsInitialState(this ItemState itemState)
		{
			StateDefinition _currentStateDefinition = itemState.ToState;
			
			return
				_currentStateDefinition == ((Workflow)_currentStateDefinition.Parent).InitialState;
		}

		public static ItemState GetCurrentState(this ContentItem item)
		{
			var _cs = item.GetDetail("_CurrentState") as ItemState;

			if (null != _cs) {
				return _cs;
			} else {
				//try to fix broken item link
				item.Details.Remove("_CurrentState");

				Workflow _wf = item.GetWorkflow();

				if (null != _wf) {

					return new ItemState {
						Action = null,
						FromState = null,
						ToState = item.GetWorkflow().InitialState,
						Comment = string.Empty,
					};
				} else {
					//Item may appear in RecycleBin,
					// thus not having parent of type IWorkflowItemContainer
					return null;
				}
			}
		}

		public static ItemState PerformAction(
			this ContentItem item,
			ActionDefinition action,
			string user,
			string comment)
		{
			var _currentState = item.GetCurrentState();

			if (null != _currentState.ToState.GetChild(action.Name)) {
				var _newCS = Context.Definitions.CreateInstance<ItemState>(item);
				_newCS.FromState = _currentState.ToState;
				_newCS.Action = action;
				_newCS.ToState = action.LeadsTo;
				_newCS.Comment = comment;
				_newCS.SavedBy = user;
				_newCS.AddTo(item);

				//Expire item if it's a final transition
				if (!action.LeadsTo.Actions.Any()) {
					item.Expires = DateTime.Now;
				}

				item.AssignCurrentState(_newCS);
				N2.Context.Persister.Save(item);
			} else {
				throw new ArgumentException(
					string.Format(
						"Requested action '{0}' cannot be performed from state '{1}'",
						action.Name,
						_currentState.ToState.Name),
					"action");
			}

			return item.GetCurrentState();
		}

		public static ItemState PerformAction(
			this ContentItem item,
			string actionName,
			string user,
			string comment)
		{
			Trace.WriteLine("Performing action: " + actionName);

			var _wf = item.GetWorkflow();

			ActionDefinition _action = (
				from _state in _wf.Children.OfType<StateDefinition>()
				from _act in _state.Children.OfType<ActionDefinition>()
				where string.Equals(_act.Name, actionName, StringComparison.OrdinalIgnoreCase)
				select _act
			).FirstOrDefault();

			Trace.WriteLineIf(null != _action, "Action name resolved: " + _action.Name, "Workflow");

			return PerformAction(item, _action, user, comment);
		}

	}
}
