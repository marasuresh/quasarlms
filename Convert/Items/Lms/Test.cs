namespace N2.Lms.Items
{
	using System;
	using N2.Details;
	using N2.Integrity;

	[Definition]
	[AllowedChildren(typeof(TestQuestion))]
	[RestrictParents(typeof(TopicList), typeof(Topic))]
	public class Test: ContentItem
	{
		public override string IconUrl { get { return "~/Lms/UI/Img/page_script.gif"; } }
	}
}
