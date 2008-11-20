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
			private set { this.ViewState["Score"] = value; }
		}
		
		protected DateTimeOffset? StartedOn {
			get { return (DateTimeOffset?)this.ViewState["StartedOn"]; }
			private set { this.ViewState["StartedOn"] = value; }
		}

		protected DateTimeOffset? FinishedOn {
			get { return (DateTimeOffset?)this.ViewState["FinishedOn"]; }
			private set { this.ViewState["FinishedOn"] = value; }
		}

		protected bool InstantCheckEnabled {
			get { return this.CurrentItem.InstantCheckEnabled; }
		}

		#endregion Properties

		#region Rendering helpers

		protected TimeSpan ElapsedTime {
			get {
				return
					this.StartedOn.HasValue
						? (this.FinishedOn ?? DateTimeOffset.Now) - this.StartedOn.Value
						: TimeSpan.MinValue;
			}
		}

		protected TimeSpan AllowedTime {
			get { return TimeSpan.FromMinutes(this.CurrentItem.Duration); }
		}

		#endregion Rendering helpers

		/// <summary>
		/// Either finishes a running session
		///  OR reconfigure previously finished session as such
		/// </summary>
		protected void FinishSession()
		{
			//gather score from each question control
			this.Score = this.CalculateTotalScore();
			
			if (!this.FinishedOn.HasValue) {
				this.FinishedOn = DateTimeOffset.Now;
			}

			foreach (TestQuestionControl _ctl in this.QuestionControls) {
				_ctl.DisplayAnswer = true;
			}
			
			this.phQuestions.Enabled = false;

			if (this.HasExpired) {
				this.phExpired.Visible = true;
			} else if (this.IsScoreReached) {
				this.phComplete.Visible = true;
			}
		}
		
		#region Control state properties

		#endregion Control state properties

		#region Run-time state properties

		/// <summary>
		/// Tells if the time given to complete the test has expired
		/// </summary>
		protected bool HasExpired {
			get {
				return this.ElapsedTime >= this.AllowedTime;
			}
		}

		protected bool IsScoreReached {
			get {
				return this.Score >= this.CurrentItem.Points;
			}
		}

		protected bool IsEnabled {
			get {
				return
					!this.HasExpired
					&& !this.IsScoreReached
					&& !this.HasFinished;
			}
		}

		protected bool HasFinished {
			get {
				return
					this.StartedOn.HasValue
					&& this.FinishedOn.HasValue
					&& this.FinishedOn.Value < DateTimeOffset.Now;
			}
		}

		//Note: an equivalent to a common pattern of using IsPostBack in OnLoad()
		// the major difference is that this control may be showed much later than
		// Load happened, that is: some psot-backs can already occur
		// before it gets showed for the first time
		protected bool IsDisplayedForTheFirstTime {
			get {
				return !this.StartedOn.HasValue;
			}
		}

		#endregion Run-time state properties

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

			if (!_state.FinishedOn.HasValue && this.FinishedOn.HasValue) {
				_modified = true;
				_state.FinishedOn = this.FinishedOn;
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

		protected virtual void RetrieveAttemptState()
		{
			var _testId = this.CurrentItem.ID.ToString();

			if (this.AttemptItem.Tests.ContainsKey(_testId)) {
				var _testState = this.AttemptItem.Tests[_testId];

				if (!this.StartedOn.HasValue && _testState.StartedOn.HasValue) {
					this.StartedOn = _testState.StartedOn;
				}

				if (!this.FinishedOn.HasValue && _testState.FinishedOn.HasValue) {
					this.FinishedOn = _testState.FinishedOn;
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
			if (this.IsDisplayedForTheFirstTime) {
				this.RetrieveAttemptState();
			}
			
			///Trigger Change only if any child has changed
			if(null != this.TestChangedArgumentsInstance) {
				this.OnChanged();
			}

			if ((this.HasExpired || this.IsScoreReached)
					&& this.CurrentItem.AutoFinish || !this.IsEnabled) {
				this.FinishSession();
			} else {
				this.PersistAttemptState();
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

			_ctl.Changed += (_sender, _e) => {
				var _tqc = _sender as TestQuestionControl;
				
				this.TestChangedArguments.NewAnswers.Add(
					_tqc.CurrentItem.ID,
					_tqc.Answer);
			};

			_ctl.InstantCheckEnabled = this.InstantCheckEnabled;
		}

		protected void btnCheck_Click(object sender, EventArgs e)
		{
			this.FinishSession();
			this.PersistAttemptState();
		}

		#endregion Event handlers
	}
}