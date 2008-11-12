namespace N2.Lms.Items
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.UI.WebControls;
	using N2.Details;
	using N2.Integrity;

	[Definition]
	[RestrictParents(typeof(Test))]
	[AllowedChildren]
	[WithEditableTitle(Required = true)]
	public partial class TestQuestion: ContentItem
	{
		#region System properties
		
		public override string IconUrl { get { return "~/Lms/UI/Img/page_tick.gif"; } }
		public override string TemplateUrl { get { return "~/Lms/UI/TestQuestion.ascx"; } }
		public override bool IsPage { get { return false; } }
		public override string ZoneName { get { return "Questions"; } }

		#endregion System properties

		#region Lms properties

		[EditableTextBox("Подсказка", 10)]
		public string ShortHint
		{
			get { return this.GetDetail<string>("ShortHint", null); }
			set { this.SetDetail<string>("ShortHint", value); }
		}

		[EditableFreeTextArea("Длинная подсказка", 15)]
		public string LongHint {
			get { return this.GetDetail<string>("LongHint", null); }
			set { this.SetDetail<string>("LongHint", value); }
		}

		[EditableTextBox(
			"Количество баллов",
			20,
			Required = true,
			ValidationExpression = @"\d+",
			Validate = true,
			DefaultValue = "0")]
		public int Points {
			get { return this.GetDetail<int>("Points", 0); }
			set { this.SetDetail<int>("Points", value); }
		}

		[EditableEnum("Тип ответа", 25, typeof(AnswerTypeEnum))]
		public AnswerTypeEnum AnswerType
		{
			get { return this.GetDetail<AnswerTypeEnum>("AnswerType", AnswerTypeEnum.Text); }
			set { this.SetDetail<AnswerTypeEnum>("AnswerType", value); }
		}

		[EditableEnum(
			"Тип вопроса",
			27,
			typeof(QuestionTypeEnum))]
		public QuestionTypeEnum Type
		{
			get { return this.GetDetail<QuestionTypeEnum>("Type", QuestionTypeEnum.Text); }
			set { this.SetDetail<QuestionTypeEnum>("Type", value); }
		}
		
		[EditableTextBox(
			"Правильный ответ",
			30,
			ValidationExpression = @"[01]*",
			Validate = true,
			Required = true,
			DefaultValue = "",
			HelpTitle = "Правильный ответ в виде битовой маски",
			HelpText = "Укажите правильный ответ (один или несколько, в зависимости от выбранного типа вопроса), в виде маски битов. Порядок цифр соответствует номеру варианта ответа. Цифра 1 указывает на правильность ответа, 0 &mdash; на неправильность.")]
		public string Answers
		{
			get { return this.GetDetail<string>("Answers", string.Empty); }
			set { this.SetDetail<string>("Answers", value); }
		}

		#endregion Lms properties

		#region Lms collection properties

		[EditableTextBox(
			"Варианты ответа",
			30,
			TextMode = TextBoxMode.MultiLine,
			Rows = 9,
			HelpTitle = "Возможные варианты ответа, по одному в каждой строчке")]
		public string Options {
			get { return string.Join("\n", OptionList.ToArray()); }
			set { this.OptionList = value.Split('\n').Select(_line => _line.Trim()).ToList(); }
		}

		internal DetailCollection OptionCollection {
			get { return this.GetDetailCollection("Options", true); }
		}

		public IList<string> OptionList {
			get {
				return this.OptionCollection.Cast<string>().ToList();
			}
			set {
				this.OptionCollection.Clear();
				this.OptionCollection.AddRange(value);
			}
		}

		protected IList<bool> AnswersMask {
			get {
				return (
					from _char in this.Answers
					select _char == '1'
				).ToList().AsReadOnly();
			}
		}

		
		#endregion Lms collection properties

		public enum AnswerTypeEnum
		{
			Text = 0,
			Multiple,
			Single
		}

		public enum QuestionTypeEnum
		{
			Text = 6,
			Url = 5,
			Object = 3
		}
	}
}
