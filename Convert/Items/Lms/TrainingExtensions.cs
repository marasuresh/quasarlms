namespace N2.Lms
{
	using Items;
	using N2.Workflow.Items;
	
	public static class TrainingExtensions
	{
		/// <summary>
		/// Default workflow hierarchy template for a given training.
		/// </summary>
		/// <param name="training"></param>
		/// <returns></returns>
		internal static Workflow GenerateDefaultWorkflow(this Training training)
		{
			int _sortOrder = 0;

			Workflow _wf = Context.Definitions.CreateInstance<Workflow>(training);
			_wf.Name = "workflow";
			_wf.Title = "Workflow";

			StateDefinition _newState = Context.Definitions.CreateInstance<StateDefinition>(_wf);
			_newState.Name = "new";
			_newState.Title = "New";
			_newState.SortOrder = _sortOrder++;
			_newState.AddTo(_wf);

			_wf.InitialState = _newState;

			StateDefinition _lastState = _newState;

			foreach (Topic _topLevelTopic in training.Course.Topics) {
				ScheduledTopic _st = Context.Definitions.CreateInstance<ScheduledTopic>(_wf);
				_st.Name = _topLevelTopic.Name;
				_st.Topic = _topLevelTopic;
				_st.Title = _topLevelTopic.Title;
				_st.SortOrder = _sortOrder++;
				_st.AddTo(_wf);

				ActionDefinition _transition = Context.Definitions.CreateInstance<ActionDefinition>(_lastState);
				_transition.Name = _transition.Title = string.Format("from {0} to {1}", _lastState.Title, _st.Title);
				_transition.LeadsTo = _st;
				_transition.AddTo(_lastState);

				_lastState = _st;
			}

			StateDefinition _finalState = Context.Definitions.CreateInstance<StateDefinition>(_wf);
			_finalState.Name = "finished";
			_finalState.Title = "Finished";
			_finalState.SortOrder = _sortOrder++;
			_finalState.AddTo(_wf);

			ActionDefinition _finishAction = Context.Definitions.CreateInstance<ActionDefinition>(_lastState);
			_finishAction.Name = "finish";
			_finishAction.Title = "Finish";
			_finishAction.LeadsTo = _finalState;
			_finishAction.AddTo(_lastState);

			//_wf.AddTo(training);

			return _wf;
		}
	}
}
