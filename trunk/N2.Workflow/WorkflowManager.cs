using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace N2.Workflow
{
	using N2.Workflow.Items;
	using N2.Persistence;
	using N2.Definitions;

	public class WorkflowManager
	{
		readonly IPersister persister;
		readonly IDefinitionManager definitions;

		public WorkflowManager(IPersister persister, IDefinitionManager definitions)
		{
			this.persister = persister;
			this.definitions = definitions;
		}

		public ItemState PerformAction(
			ContentItem item,
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
				//this.persister.Save(item);
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

		public ItemState PerformAction(
			ContentItem item,
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

			return this.PerformAction(item, _action, user, comment);
		}
	}
}
