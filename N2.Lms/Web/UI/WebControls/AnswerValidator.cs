using System.Web.UI.WebControls;

namespace N2.Lms.Web.UI.WebControls
{
	using N2.Lms.Items;

	/// <summary>
	/// Polymorphically validate control, that renders TestQuestion.
	/// </summary>
	public class AnswerValidator: BaseValidator
	{
		#region Properties
		
		TestQuestion m_question;
		public TestQuestion Question {
			get {
				return
					m_question
					?? (this.ParentId.HasValue
						? N2.Context.Persister.Get<TestQuestion>(this.ParentId.Value)
						: default(TestQuestion));
			}
			set {
				this.m_question = value;
				
				if (null != value) {
					this.ParentId = value.ID;
				}
			}
		}

		protected int? ParentId {
			get { return (int?)this.ViewState["ParentId"]; }
			set { this.ViewState["ParentId"] = value; }
		}

		#endregion Properties

		#region Methods

		protected override bool EvaluateIsValid()
		{
			string _value = this.GetControlValidationValue(base.ControlToValidate);
			
			return
				null != this.Question
					? this.Question.IsAnswerCorrect(_value)
					: false;
		}

		#endregion Methods
	}
}
