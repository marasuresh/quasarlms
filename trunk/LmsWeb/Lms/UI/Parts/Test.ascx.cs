using System;

namespace N2.Lms.UI.Parts
{
	using N2.Lms.Web.UI.WebControls;
	using N2.Templates.Items;
	using N2.Templates.Web.UI;
	using N2.Web.UI;
	
	public partial class TestControl : TemplateUserControl<AbstractContentPage, N2.Lms.Items.Test>
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		public int Score {
			get {
				return (int?)this.ViewState["Score"] ?? 0;
			}
			set {
				this.ViewState["Score"] = value;
			}
		}

		protected void question_answerChanged(object sender, TestQuestionEventArgs e)
		{
			e.Disable = true;

			if (e.IsCorrect) {
				this.Score += e.Question.Points;
			}
		}

		protected void zone_AddedItemTemplate(object sender, ControlEventArgs e)
		{
			((TestQuestionControl)e.Control).AnswerChanged += this.question_answerChanged;
		}
	}
}