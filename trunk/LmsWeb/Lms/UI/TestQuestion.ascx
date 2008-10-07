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
		};

		foreach (var _option in options) {
			_ctl.Items.Add(new ListItem(_option));
		}

		return _ctl;
	}

	protected Control CreateRadioAnswerControl(IEnumerable<string> options)
	{
		var _ctl = new RadioButtonList {
			ID = "rbl"
		};

		foreach (var _option in options) {
			_ctl.Items.Add(new ListItem(_option));
		}

		return _ctl;
	}

	protected override void CreateChildControls()
	{
		base.CreateChildControls();
		this.Controls.Clear();
		this.CreateControlHierarchy();
	}
	
	protected virtual void CreateControlHierarchy()
	{
		TestQuestion _question = this.CurrentItem;
		this.Controls.Add(new LiteralControl(string.Format(
@"<p><strong>{1}</strong> ({2}/{3})</p>",
				null,
				_question.Title,
				_question.Type,
				_question.AnswerType)));
		
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
