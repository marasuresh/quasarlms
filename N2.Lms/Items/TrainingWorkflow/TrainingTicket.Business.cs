using System.Collections.Generic;
using System.Linq;

namespace N2.Lms.Items.TrainingWorkflow
{
	using N2.Details;
	using N2.Lms.LearningSession;

	partial class TrainingTicket
	{
		public IDictionary<string, TestState> Tests {
			get {
				return this
					.GetDetailCollection("TestState", true)
					.AsDictionary<TestState>();
			}
		}

		public IList<HistoryItem> TopicHistory {
			get {
				return this
					.GetDetailCollection("TopicHistory", true)
					.AsList<HistoryItem>();
			}
		}
	}
}
