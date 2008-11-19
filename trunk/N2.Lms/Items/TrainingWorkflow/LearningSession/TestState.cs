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

		IDictionary<int, string> m_answers;
		public IDictionary<int, string> Answers {
			get { return this.m_answers ?? (this.m_answers = new Dictionary<int, string>()); }
			set { this.m_answers = value; }
		}
		
		public int Score { get; set; }
	}
}
