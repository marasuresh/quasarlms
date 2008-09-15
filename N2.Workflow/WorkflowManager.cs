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

		ItemState EnsureCurrentState(ContentItem item)
		{
			var _state = item.GetCurrentState();
			
			if (null == _state) {
				ItemState _cs = this.definitions.CreateInstance<ItemState>(item);
				_cs.ToState = item.GetWorkflow().InitialState;
				this.persister.Save(_cs);
			}

			return _state;
		}

		ItemState EnsureCurrentState<C>(C item)
			where C: ContentItem, IWorkflowEnabled
		{
			return this.EnsureCurrentState(item);
		}

		public ItemState PerformAction(
			ContentItem item,
			ActionDefinition action,
			string user,
			string comment)
		{
			//this.EnsureCurrentState(item);
			Workflow _wf = (item.Parent as IWorkflowItemContainer).Workflow;
			
			var _currentState = item.GetCurrentState();

			if (_currentState.ToState.Children.Contains(action)) {
				var _newCS = Context.Current.Definitions.CreateInstance<ItemState>(item);
				_newCS.FromState = _currentState.ToState;
				_newCS.Action = action;
				_newCS.ToState = action.LeadsTo;
				_newCS.Comment = comment;
				_newCS.SavedBy = user;
				_newCS.AddTo(item);
				((ContentItem)item).AssignCurrentState(_newCS);
				//this.persister.Save(item);
			} else {
				throw new ArgumentException("action");
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
				where _act.Name == actionName
				select _act
			).FirstOrDefault();

			return this.PerformAction(item, _action, user, comment);
		}

		#region IStartable Members

		public void Start()
		{
			this.persister.ItemSaving += this.OnItemSaving;
		}

		public void Stop()
		{
			this.persister.ItemSaving -= this.OnItemSaving;
		}

		#endregion

		#region Event handlers

		void OnItemSaving(object sender, CancellableItemEventArgs e)
		{
			if (e.AffectedItem is IWorkflowEnabled) {
				this.EnsureCurrentState(e.AffectedItem);
			}
		}

		#endregion Event handlers

	}
}
