using System;

namespace N2.Lms.LearningSession
{
	public class HistoryItem
	{
		public int ItemId { get; set; }
		public DateTimeOffset StartedOn { get; set; }
		public DateTimeOffset FinishedOn { get; set; }
	}
}
