using System.Linq;
using System.Collections.Generic;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N2.Lms.UI.Parts
{
	using N2.Templates.Items;
	using N2.Templates.Web.UI;
	using N2.Lms.Items;
	using N2.Lms.Web.UI.WebControls;

	public class TestQuestionControl : TemplateUserControl<AbstractContentPage, TestQuestion>
	{
		protected Control CreateTextAnswerControl()
		{
			var _ctl = new TextBox {
				ID = "tb",
				TextMode = TextBoxMode.MultiLine,
			};

			var _panel = new Panel();
			var _chk = new Button { Text = "Check", };
			this.PreRender += (sender, e) => _chk.Visible = this.InstantCheckEnabled;

			_chk.Click += (sender, e) => {
				this.OnAnswerChanged(_ctl.Text, _panel);
			};

			this.AnswerMutator = val => _ctl.Text = val;
			this.AnswerAccessor = () => _ctl.Text;

			_panel.Controls.Add(_ctl);
			_panel.Controls.Add(_chk);

			return _panel;
		}

		protected Control CreateCheckBoxAnswerControl(IEnumerable<string> options)
		{
			var _ctl = new ValidatableCheckBoxList { ID = "cbl", };

			foreach (var _option in options) {
				_ctl.Items.Add(new ListItem(_option));
			}

			var _panel = new Panel();
			var _chk = new Button { Text = "Check", };
			this.PreRender += (sender, e) => _chk.Visible = this.InstantCheckEnabled;

			_chk.Click += (sender, e) => {
				var _cbl = _ctl as ValidatableCheckBoxList;
				this.OnAnswerChanged(_cbl.CheckedMask, _panel);
			};

			this.AnswerAccessor = () => _ctl.CheckedMask;
			this.AnswerMutator = val => _ctl.CheckedMask = val;

			_panel.Controls.Add(_ctl);
			_panel.Controls.Add(_chk);

			return _panel;
		}

		protected Control CreateRadioAnswerControl(IEnumerable<string> options)
		{
			var _ctl = new RadioButtonList { ID = "rbl", };
			
			this.PreRender += (sender, e) => _ctl.AutoPostBack = this.InstantCheckEnabled;

			foreach (var _option in options) {
				_ctl.Items.Add(new ListItem(_option));
			}

			_ctl.SelectedIndexChanged += (sender, e) => {
				var _rbl = sender as RadioButtonList;
				this.OnAnswerChanged(_rbl.SelectedValue, _rbl);
			};

			this.AnswerMutator = val => _ctl.SelectedValue = val;
			this.AnswerAccessor = () => _ctl.SelectedValue;

			return _ctl;
		}

		protected override void OnInit(EventArgs e)
		{
			this.EnsureChildControls();
			base.OnInit(e);
		}

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

			Control _answerControl;

			switch (_question.AnswerType) {
				case TestQuestion.AnswerTypeEnum.Text:
					_answerControl = CreateTextAnswerControl();
					break;
				case TestQuestion.AnswerTypeEnum.Multiple:
					_answerControl = CreateCheckBoxAnswerControl(_question.OptionList);
					break;
				case TestQuestion.AnswerTypeEnum.Single:
					_answerControl = CreateRadioAnswerControl(_question.OptionList);
					break;
				default:
					_answerControl = new LiteralControl("Unknown answer type");
					break;
			}

			this.Controls.Add(_answerControl);
		}

		public event EventHandler<TestQuestionEventArgs> AnswerChanged;

		protected virtual void OnAnswerChanged(string answer, WebControl ctl)
		{
			if (null != this.AnswerChanged) {
				var _args = new TestQuestionEventArgs {
					Question = this.CurrentItem,
					Answer = answer,
					IsCorrect = this.CurrentItem.IsAnswerCorrect(answer),
					Disable = this.InstantCheckEnabled,
				};

				//Allow subscriber to alter params..
				this.AnswerChanged(this, _args);

				if (_args.Disable) {
					ctl.Enabled = false;

					ctl.CssClass = string.Join(" ",
						ctl.CssClass.Split(' ').Concat(new[] {
							_args.IsCorrect
								? "correct"
								: "incorrect" }).ToArray());
				}
			}
		}

		#region Properties

		Func<string> AnswerAccessor;//delayed getter of Answer property
		Action<string> AnswerMutator;//ditto, but setter

		public string Answer {
			get {
				return null != this.AnswerAccessor ? this.AnswerAccessor() : null;
			}
			set {
				if (null != this.AnswerMutator) {
					this.PreRender += (sender, e) => {
						if (!string.IsNullOrEmpty(value)) {
							this.AnswerMutator(value);
						}
					};
				}
			}
		}

		public int Score {
			get { return
				this.CurrentItem.IsAnswerCorrect(this.Answer) ? this.CurrentItem.Points : 0; }
		}

		public bool InstantCheckEnabled {
			get { return (bool?)this.ViewState["InstantCheckEnabled"] ?? true; }
			set { this.ViewState["InstantCheckEnabled"] = value; }
		}

		#endregion Properties
	}
}