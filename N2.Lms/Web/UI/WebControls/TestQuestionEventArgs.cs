using System;

namespace N2.Lms.Web.UI.WebControls
{
	using N2.Lms.Items;
	
	public class TestQuestionEventArgs : EventArgs
	{
		public TestQuestion Question { get; set; }
		public string Answer { get; set; }
		public bool Disable { get; set; }
		public bool IsCorrect { get; set; }
	}
}
