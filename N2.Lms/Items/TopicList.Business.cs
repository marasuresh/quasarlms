namespace N2.Lms.Items
{
	using System.Collections.Generic;
	using System.Linq;
	using N2.Collections;

	partial class TopicContainer
	{
		internal IEnumerable<Topic> Topics {
			get { return this.GetChildren(
				new TypeFilter(typeof(Topic))).Cast<Topic>(); }
		}
	}
}
