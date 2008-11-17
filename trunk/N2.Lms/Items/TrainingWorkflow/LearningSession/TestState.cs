using System;
using System.Collections.Generic;
using System.Linq;

namespace N2.Lms.LearningSession
{
	public class TestState
	{
		public bool Enabled { set; get; }
		public int Id { get; set; }
		public IOrderedEnumerable<int> QuestionOrder { get; set; }
		public DateTimeOffset StartedOn { get; set; }
		public IDictionary<int, string> Answers { get; set; }
		public int Score { get; set; }
	}
}
