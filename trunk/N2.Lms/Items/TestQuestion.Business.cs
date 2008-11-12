namespace N2.Lms.Items
{
	partial class TestQuestion
	{
		public Test Test { get { return this.Parent as Test; } }

		public bool IsAnswerCorrect(string answer)
		{
			bool _result = false;
			
			switch (this.AnswerType) {
				case AnswerTypeEnum.Text:
					_result = string.Equals(this.Answers, answer, System.StringComparison.OrdinalIgnoreCase);
					break;
				case AnswerTypeEnum.Single:
					_result = string.Equals(
						this.OptionList[this.AnswersMask.IndexOf(true)],
						answer,
						System.StringComparison.OrdinalIgnoreCase);
					break;
				case AnswerTypeEnum.Multiple:
					_result = string.Equals(this.Answers, answer);
					break;
			}

			return _result;
		}
	}
}
