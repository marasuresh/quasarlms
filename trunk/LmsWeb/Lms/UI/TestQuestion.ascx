<%@ Control
		Language="c#"
		Inherits="N2.Templates.Web.UI.TemplateUserControl`2[[N2.Templates.Items.AbstractContentPage, N2.Templates], [N2.Lms.Items.TestQuestion, N2.Lms]], N2.Templates" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="N2.Lms.Items" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Collections.Generic" %>

<script runat="server">
	protected Control CreateTextAnswerControl()
	{
		Control _ctl = new TextBox {
			ID = "tb",
			TextMode = TextBoxMode.MultiLine
		};
		
		return _ctl;
	}

	protected Control CreateCheckBoxAnswerControl(IEnumerable<string> options)
	{
		var _ctl = new CheckBoxList {
			ID = "cbl",
			AutoPostBack = true,
		};

		foreach (var _option in options) {
			_ctl.Items.Add(new ListItem(_option));
		}

		return _ctl;
	}

	protected Control CreateRadioAnswerControl(IEnumerable<string> options)
	{
		var _ctl = new RadioButtonList {
			ID = "rbl", AutoPostBack = true,
		};

		foreach (var _option in options) {
			_ctl.Items.Add(new ListItem(_option));
		}

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
</script>
