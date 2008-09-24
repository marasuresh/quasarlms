namespace N2.Lms.Items
{
	using System;
	using System.Linq;
	using N2.Details;
	using N2.Integrity;
	using System.Web.UI.WebControls;

	[Definition]
	[RestrictParents(typeof(Test))]
	[AllowedChildren]
	[WithEditableTitle(Required = true)]
	[WithEditable(
		"Подсказка",
		typeof(TextBox),
		"Text",
		10,
		"ShortHint")]
	[WithEditable(
		"Длинная подсказка",
		typeof(TextBox),
		"Text", 15, "LongHint")]
	public class TestQuestion: ContentItem
	{
		#region System properties
		
		public override string IconUrl { get { return "~/Lms/UI/Img/page_tick.gif"; } }
		
		#endregion System properties

		#region Lms properties

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

		[EditableTextBox(
			"Тип вопроса",
			25,
			ValidationExpression = @"\d+",
			Validate = true,
			Required = true,
			DefaultValue = "1")]
		public int Type {
			get { return this.GetDetail<int>("Type", 1); }
			set { this.SetDetail<int>("Type", value); }
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
			get {
				return
					string.Join(
						"\n",
						this.GetDetailCollection("Options", true).Cast<string>().ToArray());
			}
			set {
				var _col = this.GetDetailCollection("Options", true);
				_col.Clear();
				_col.AddRange(
					from _line in value.Split('\n')
					select _line.Trim()
				);
			}
		}
		
		#endregion Lms collection properties
	}
}
