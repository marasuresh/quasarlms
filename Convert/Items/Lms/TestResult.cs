namespace N2.Lms.Items
{
	using System;
	using N2.Details;

	[Definition]
	public class TestResult: N2.ContentItem
	{
		public int Points { get; set; }

		public Test Test { get; set; }

		public int AllowedAttempts { get; set; }

		public bool IsComplete { get; set; }

		public bool IsSkipped { get; set; }

		public int AttemptsCount { get; set; }

		public string Theme { get; set; }

		public DateTime CompletedOn { get; set; }

		public DateTime StartedOn { get; set; }
	}
}
