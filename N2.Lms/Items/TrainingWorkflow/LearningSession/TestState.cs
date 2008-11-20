using System;
using System.Collections.Generic;
using System.Linq;

namespace N2.Lms.LearningSession
{
	[Serializable]
	public class TestState
	{
		public bool Enabled { set; get; }
		public int Id { get; set; }
		public IOrderedEnumerable<int> QuestionOrder { get; set; }
		
		public DateTimeOffset? StartedOn { get; set; }
		public DateTimeOffset? FinishedOn { get; set; }

		IDictionary<int, string> m_answers;
		public IDictionary<int, string> Answers {
			get { return this.m_answers ?? (this.m_answers = new Dictionary<int, string>()); }
		}
		
		public int Score { get; set; }
	}
}
