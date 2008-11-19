using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace N2.Lms.UI.Parts
{
	using N2.Lms.Web.UI.WebControls;
	using N2.Templates.Items;
	using N2.Templates.Web.UI;
	using N2.Web.UI;
	using N2.Lms.LearningSession;
	
	public partial class TestControl : BaseLearningControl<Lms.Items.Test>
	{
		#region Properties

		public int Score {
			get { return (int?)this.ViewState["Score"] ?? 0; }
			set { this.ViewState["Score"] = value; }
		}
		
		protected DateTimeOffset? StartedOn {
			get { return (DateTimeOffset?)this.ViewState["StartedOn"]; }
			set { this.ViewState["StartedOn"] = value; }
		}

		protected bool InstantCheckEnabled {
			get { return false && this.CurrentItem.InstantCheckEnabled; }
		}

		#endregion Properties

		#region Rendering helpers

		TimeSpan ElapsedTime {
			get {
				return
					this.StartedOn.HasValue
						? DateTimeOffset.Now - this.StartedOn.Value
						: TimeSpan.MinValue;
			}
		}

		protected string ElapsedTimeString {
			get {
				var _et = this.ElapsedTime;
				return string.Join(":", new[] {
						_et.Hours,
						_et.Minutes,
						_et.Seconds }
					.Select(i => i.ToString("00"))
					.ToArray());
			}
		}

		#endregion Rendering helpers

		protected void FinishSession()
		{
			this.phQuestions.Enabled = false;
			this.phExpired.Visible = true;
		}

		#region State data management

		protected void PersistAttemptState()
		{
			var _testId = this.CurrentItem.ID.ToString();
			TestState _state = this.AttemptItem.Tests[_testId];

			bool _modified = false;

			if(null == _state) {
				_modified = true;
				this.AttemptItem.Tests.Add(_testId, _state = new TestState());
			}

			if (!_state.StartedOn.HasValue && this.StartedOn.HasValue) {
				_modified = true;
				_state.StartedOn = this.StartedOn;
			}

			//persist answers
			if (null != this.TestChangedArgumentsInstance) {
				foreach (var _kvp in this.TestChangedArgumentsInstance.NewAnswers) {
					_modified = true;
					if (_state.Answers.ContainsKey(_kvp.Key)) {
						_state.Answers[_kvp.Key] = _kvp.Value;
					} else {
						_state.Answers.Add(_kvp.Key, _kvp.Value);
					}
				}
			}

			if (_modified) {
				N2.Context.Persister.Save(this.AttemptItem);
			}
		}

		protected void RetrieveAttemptState()
		{
			var _testId = this.CurrentItem.ID.ToString();

			if (this.AttemptItem.Tests.ContainsKey(_testId)) {
				var _testState = this.AttemptItem.Tests[_testId];

				if (!this.StartedOn.HasValue && _testState.StartedOn.HasValue) {
					this.StartedOn = _testState.StartedOn;
				}

				foreach (var _tqc in this.QuestionControls) {
					var _answerId = _tqc.CurrentItem.ID;

					if (_testState.Answers.ContainsKey(_answerId)) {
						_tqc.Answer = _testState.Answers[_answerId];
					}
				}
			}

			if (!this.StartedOn.HasValue) {
				this.StartedOn = DateTimeOffset.Now;
			}
		}

		#endregion State data management

		protected override void OnPreRender(EventArgs e)
		{
			//occurs on a first display
			//Note: an equivalent to a common pattern of using IsPostBack in OnLoad()
			// the major difference is that this control may be showed much later than
			// Load happened, that is: some psot-backs can already occur
			// before it gets showed for the first time
			if (!this.StartedOn.HasValue) {
				this.RetrieveAttemptState();
			}
			
			this.PersistAttemptState();
			///Trigger Change only if any child has changed
			if(null != this.TestChangedArgumentsInstance) {
				this.OnChanged();
			}

			if (this.ElapsedTime.TotalMinutes >= 1 + 0 * this.CurrentItem.Duration) {
				this.FinishSession();
			}

			this.btnCheck.Visible = !this.InstantCheckEnabled;
			base.OnPreRender(e);
		}

		public virtual IEnumerable<TestQuestionControl> QuestionControls {
			get { return this.qz.Controls.OfType<TestQuestionControl>(); }
		}

		int CalculateTotalScore()
		{
			return this.QuestionControls
					.Select(_q => _q.Score)
					.Sum();
		}

		#region Events

		TestChangedEventArgs TestChangedArgumentsInstance;
		protected TestChangedEventArgs TestChangedArguments {
			get { return this.TestChangedArgumentsInstance ?? (this.TestChangedArgumentsInstance = new TestChangedEventArgs()); }
		}

		public event EventHandler<TestChangedEventArgs> Changed;

		protected virtual void OnChanged()
		{
			if (null != this.Changed) {
				this.Changed(this, this.TestChangedArgumentsInstance);
			}
		}

		#endregion Events

		#region Event handlers

		protected void zone_AddedItemTemplate(object sender, ControlEventArgs e)
		{
			var _ctl = (TestQuestionControl)e.Control;

			_ctl.Submit += (_sender, _e) => {
				if (_e.IsCorrect) {
					if (this.InstantCheckEnabled) {
						this.Score += _e.Question.Points;
					}
				}
			};

			_ctl.Change += (_sender, _e) => {
				var _tqc = _sender as TestQuestionControl;
				
				this.TestChangedArguments.NewAnswers.Add(
					_tqc.CurrentItem.ID,
					_tqc.Answer);
			};

			_ctl.InstantCheckEnabled = this.InstantCheckEnabled;
		}

		protected void btnCheck_Click(object sender, EventArgs e)
		{
			this.Score = this.CalculateTotalScore();
		}

		#endregion Event handlers
	}
}