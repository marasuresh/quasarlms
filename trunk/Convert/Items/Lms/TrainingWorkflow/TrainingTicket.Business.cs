namespace N2.Lms.Items.TrainingWorkflow
{
	using System;
	using N2.Templates.Items;
	using System.Diagnostics;

	partial class TrainingTicket
	{
		public Topic CurrentTopic
		{
			get;
			protected set;
		}

		public override ContentItem GetChild(string childName)
		{
			Uri _base = new Uri("http://localhost");
			UriTemplate _template = new UriTemplate("topic/{topic}");
			UriTemplateMatch _match = _template.Match(_base, new Uri(_base, childName));

			Trace.WriteLine("TrainingTicket.GetChild: " + childName, "Lms");

			if(null != _match) {
				string _topicName = _match.BoundVariables["topic"];
				Trace.WriteLine("Matched: " + _topicName, "Lms");
				
				this.CurrentTopic = (Topic)this.Training.Course.TopicContainer.GetChild(_topicName);
				
				
				
				return this;
			}

			return base.GetChild(childName);
		}
	}
}
