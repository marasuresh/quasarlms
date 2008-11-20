using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N2.Lms.UI.Parts
{
	using N2.Lms.Items;
	using N2.Lms.Web.UI.WebControls;

	public class TestQuestionControl : BaseLearningControl<TestQuestion>
	{
		#region Polymorphic answer control creation
		/// <summary>
		/// Causes doing post-back on more often just to ensure
		///  the answer value persisted to DB.
		///TODO Make configurable
		/// </summary>
		bool AggressivelyPersistState = true;
		
		protected void CreateTextAnswerControl(Control container)
		{
			var _ctl = new TextBox {
				ID = "tb",
				TextMode = TextBoxMode.MultiLine,
				AutoPostBack = AggressivelyPersistState,
			};

			//Wire up a change event
			_ctl.TextChanged += (sender, e) => this.OnChanged();

			var _chk = new Button { Text = "Check", };
			this.PreRender += (sender, e) => _chk.Visible = this.InstantCheckEnabled;

			_chk.Click += (sender, e) => this.OnSubmit();

			this.AnswerMutator = val => _ctl.Text = val;
			this.AnswerAccessor = () => _ctl.Text;

			container.Controls.Add(_ctl);
			container.Controls.Add(_chk);
		}

		protected void CreateCheckBoxAnswerControl(Control container, IEnumerable<string> options)
		{
			var _ctl = new ValidatableCheckBoxList {
				ID = "cbl",
				AutoPostBack = this.AggressivelyPersistState,
			};

			foreach (var _option in options) {
				_ctl.Items.Add(new ListItem(_option));
			}

			//Wire up a change event
			_ctl.SelectedIndexChanged += (sender, e) => this.OnChanged();

			var _chk = new Button { Text = "Check", };
			this.PreRender += (sender, e) => _chk.Visible = this.InstantCheckEnabled;

			_chk.Click += (sender, e) => this.OnSubmit();

			this.AnswerAccessor = () => _ctl.CheckedMask;
			this.AnswerMutator = val => _ctl.CheckedMask = val;

			container.Controls.Add(_ctl);
			container.Controls.Add(_chk);
		}

		protected void CreateRadioAnswerControl(Control container, IEnumerable<string> options)
		{
			var _ctl = new RadioButtonList {
				ID = "rbl",
				AutoPostBack = this.AggressivelyPersistState,
			};

			if (!this.AggressivelyPersistState) {
				this.PreRender += (sender, e) => _ctl.AutoPostBack = this.InstantCheckEnabled;
			}

			foreach (var _option in options) {
				_ctl.Items.Add(new ListItem(_option));
			}
			
			_ctl.SelectedIndexChanged += (sender, e) => {
				this.OnChanged();
				this.OnSubmit();
			};

			this.AnswerMutator = val => _ctl.SelectedValue = val;
			this.AnswerAccessor = () => _ctl.SelectedValue;

			container.Controls.Add(_ctl);
		}

		#endregion Polymorphic answer control creation
		/*
		protected override void OnInit(EventArgs e)
		{
			this.EnsureChildControls();
			base.OnInit(e);
		}*/

		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.Controls.Clear();
			this.CreateControlHierarchy();
			base.ClearChildViewState();
		}

		protected virtual void CreateControlHierarchy()
		{
			TestQuestion _question = this.CurrentItem;

			Control _questionCotrol;

			switch (_question.Type) {
				case TestQuestion.QuestionTypeEnum.Url:
					_questionCotrol = new LiteralControl(
						string.Concat(
							"<p><iframe src='",
							this.ResolveClientUrl("~/Upload/Courses/" + _question.Title),
							"'></iframe></p>"));
					break;
				default:
					_questionCotrol = new LiteralControl(
						string.Concat(
							"<p><strong>",
							_question.Title,
							"</strong></p>"));
					break;
			}
			
			this.Controls.Add(_questionCotrol);

			if (this.CurrentItem.Test.HintType == Test.HintTypeEnum.Single
				|| this.CurrentItem.Test.HintType == Test.HintTypeEnum.Both) {

				if (!string.IsNullOrEmpty(this.CurrentItem.ShortHint)) {
					this.Controls.Add(new LiteralControl(string.Format(
	@"<p>{0}</p>", this.CurrentItem.ShortHint)));
				}

				if (this.CurrentItem.Test.HintType == Test.HintTypeEnum.Both) {
					if (!string.IsNullOrEmpty(this.CurrentItem.LongHint)) {
						this.Controls.Add(new LiteralControl(string.Format(
	@"<p>{0}</p>", this.CurrentItem.LongHint)));
					}
				}
			}

			this.Controls.Add(this.QuestionContainer = new Panel());

			switch (_question.AnswerType) {
				case TestQuestion.AnswerTypeEnum.Text:
					this.CreateTextAnswerControl(this.QuestionContainer);
					break;
				case TestQuestion.AnswerTypeEnum.Multiple:
					this.CreateCheckBoxAnswerControl(this.QuestionContainer, _question.OptionList);
					break;
				case TestQuestion.AnswerTypeEnum.Single:
					this.CreateRadioAnswerControl(this.QuestionContainer, _question.OptionList);
					break;
				default:
					this.QuestionContainer.Controls.Add(
						new LiteralControl("Unknown answer type"));
					break;
			}
		}

		#region Event handlers

		protected override void OnPreRender(EventArgs e)
		{
			if (this.DisplayAnswer) {
				this.EnsureTestQuestionEventArgs();
				this.RenderAnswerInformation();
			}
			
			base.OnPreRender(e);
		}

		protected void RenderAnswerInformation()
		{
			this.QuestionContainer.Enabled = false;

			//consider if Answer is null then question was not attempted,
			// so skip it
			if (!string.IsNullOrEmpty(this.Answer)) {
				this.QuestionContainer.CssClass = string.Join(" ",
					this.QuestionContainer.CssClass.Split(' ').Concat(new[] {
							this.TestQuestionEventArgsInstance.IsCorrect
								? "correct"
								: "incorrect" }).ToArray());
			}
		}

		#endregion Event handlers

		#region Events

		/// <summary>
		/// Occurs after the most recent post-back if control value has changed
		/// </summary>
		public event EventHandler Changed;

		/// <summary>
		/// Triggers Changed event
		/// </summary>
		protected virtual void OnChanged()
		{
			if (null != this.Changed) {
				this.Changed(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Occurs when User submitted an answer
		/// </summary>
		public event EventHandler<TestQuestionEventArgs> Submit;

		/// <summary>
		/// Triggers Submit event
		/// </summary>
		protected virtual void OnSubmit()
		{
			if (null != this.Submit) {
				this.EnsureTestQuestionEventArgs();

				//Allow subscriber to alter params..
				this.Submit(this, this.TestQuestionEventArgsInstance);

				if (this.TestQuestionEventArgsInstance.Disable) {
					this.DisplayAnswer = true;
				}
			}
		}

		#endregion Events

		#region Fields

		/// <summary>
		/// Holds event arguments for Submit event.
		/// They are useful elsewhere during page lifecycle,
		///  so holding it in an instance (rather then a local) vairable
		/// </summary>
		TestQuestionEventArgs TestQuestionEventArgsInstance;
		
		/// <summary>
		/// Should be called before attempting to access <see cref="TestQuestionEventArgsInstance"/>
		/// </summary>
		protected void EnsureTestQuestionEventArgs()
		{
			if (null == this.TestQuestionEventArgsInstance) {
				this.TestQuestionEventArgsInstance = new TestQuestionEventArgs {
					Question = this.CurrentItem,
					Answer = this.Answer,
					IsCorrect = this.CurrentItem.IsAnswerCorrect(this.Answer),
					Disable = this.InstantCheckEnabled,
				};
			}
		}

		protected WebControl QuestionContainer;
		
		#endregion Fields

		#region Properties

		/// <summary>
		/// Both delegates are needed to abstract Answer property from
		/// a dependency on any particular web control holding the value.
		/// </summary>
		Func<string> AnswerAccessor;//delayed getter of Answer property
		Action<string> AnswerMutator;//ditto, but setter

		public string Answer {
			get {
				return null != this.AnswerAccessor ? this.AnswerAccessor() : null;
			}
			set {
				//will indirectly set AnswerMutator
				this.EnsureChildControls();
				
				if (null != this.AnswerMutator) {
					if (!string.IsNullOrEmpty(value)) {
						if (this.ChildControlsCreated) {
							this.AnswerMutator(value);
						} else {
							//delay assiginig value to control until it is instantiated
							this.Load += (sender, e) => {
								this.AnswerMutator(value);
							};
						}
					}
				}
			}
		}

		public int Score {
			get { return
				this.CurrentItem.IsAnswerCorrect(this.Answer)
					? this.CurrentItem.Points
					: 0; }
		}

		public bool InstantCheckEnabled {
			get { return (bool?)this.ViewState["InstantCheckEnabled"] ?? true; }
			set { this.ViewState["InstantCheckEnabled"] = value; }
		}

		/// <summary>
		/// Determines whether to display answer correctness to the user.
		/// This property causes action to be taken on pre-render.
		/// </summary>
		public bool DisplayAnswer {
			get {
				return (bool?)this.ViewState["DisplayAnswer"] ?? false;
			}
			set { this.ViewState["DisplayAnswer"] = value; }
		}
		
		#endregion Properties
	}
}