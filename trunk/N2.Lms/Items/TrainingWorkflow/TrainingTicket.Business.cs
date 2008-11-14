using System.Collections.Generic;
using System.Linq;

namespace N2.Lms.Items.TrainingWorkflow
{
	using N2.Details;
	using N2.Lms.LearningSession;

	partial class TrainingTicket
	{
		public IDictionary<int, TestState> Tests
		{
			get
			{
				return (
					from _ts in this.GetDetailCollection("TestState", true).Cast<ObjectDetail>()
					select new { Name = _ts.Name, Value = _ts.Value as TestState, }
				).ToDictionary(i => int.Parse(i.Name), i => i.Value);
			}
			set
			{
				var _collection = this.GetDetailCollection("TestState", true);
				_collection.Clear();

				_collection.AddRange(
					from i in value
					select new ObjectDetail(this, i.Key.ToString(), i.Value)
				);
			}
		}

		public IEnumerable<HistoryItem> TopicHistory {
			get {
				return this.GetDetail<IEnumerable<HistoryItem>>("TopicHistory", new HistoryItem[0]);
			}
			set {
				this.SetDetail<IEnumerable<HistoryItem>>("TopicHistory", value);
			}
		}
	}
}
