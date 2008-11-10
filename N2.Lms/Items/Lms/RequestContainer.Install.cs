using System;
using System.Linq;

namespace N2.Lms.Items
{
	using N2.Lms.Items.Lms.RequestStates;
	using N2.Workflow.Items;

	partial class RequestContainer
	{
		class ActionDeclaration
		{
			public string Name { get; set; }
			public string Destination { get; set; }
			public Type Type { get; set; }
		}

		Workflow InitializeDefaultWorkflow(Workflow workflow)
		{
			int _sortOrder = 0;
			
			var _definition = new[] {
				new {
					Name = "New",
					Icon = "04",
					Actions = new[] {
						new ActionDeclaration {
							Name = "Approve",
							Destination = "Active",
							Type = typeof(ApprovedState) },
						new ActionDeclaration {
							Name = "Cancel",
							Destination = "Closed",
							Type = typeof(ItemState) }}},
				new {
					Name = "Active",
					Icon = "02",
					Actions = new[] {
						new ActionDeclaration {
							Name = "Finish",
							Destination = "Pending Validation",
							Type = typeof(ItemState) }}},
				new {
					Name = "Closed",
					Icon = "10",
					Actions = Enumerable.Empty<ActionDeclaration>().ToArray() },
				new {
					Name = "Pending Validation",
					Icon = "06",
					Actions = new[] {
						new ActionDeclaration {
							Name = "Accept",
							Destination = "Closed",
							Type = typeof(AcceptedState) },
						new ActionDeclaration {
							Name = "Decline",
							Destination = "Active",
							Type = typeof(ApprovedState) },
				}},
			};

			var _states = Array.ConvertAll(_definition,	(s) => {
				var _state = Context.Definitions.CreateInstance<StateDefinition>(workflow);
				_state.Name = _state.Title = s.Name;
				_state.Icon = string.Concat("~/Lms/UI/Img/Workflow/", s.Icon, ".png");
				_state.SortOrder = ++_sortOrder;
				_state.AddTo(workflow);
				return new { State = _state, Definition = s };
			}).ToArray();

			Array.ForEach(_states, state => {
				Array.ForEach(state.Definition.Actions, actionDef => {
					var _action = Context.Definitions.CreateInstance<ActionDefinition>(state.State);
					_action.Name = _action.Title = actionDef.Name;
					_action.LeadsTo = _states.Single(_s => _s.Definition.Name == actionDef.Destination).State;
					_action.StateType = actionDef.Type;
					_action.AddTo(state.State);
				});
			});

			workflow.InitialState = _states.Single(_item => _item.Definition.Name == "New").State;

			return workflow;
		}
	}
}
