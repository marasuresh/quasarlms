using System.Collections.Generic;

namespace N2.Lms.Items
{
	partial class ScheduledTopic
	{
		public IEnumerable<Topic> FlatHierarchy
		{
			get { return this.Topic.FlattenChildHierarchy(); }
		}

		public bool HasTest { get { return null != this.Topic.Practice; } }
	}
}
