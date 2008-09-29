namespace N2.Lms.Items
{
	using System;
	using N2.Details;
	using N2.Integrity;
	using System.Collections.Generic;
	using N2.Templates.Items;

	[Definition]
	[AllowedChildren(typeof(TestQuestion))]
	[RestrictParents(typeof(TopicList), typeof(Topic))]
	public class Test: ContentItem, ISurvey
	{
		public override string IconUrl { get { return "~/Lms/UI/Img/page_script.gif"; } }

		[EditableChildren("Вопросы", null, "Questions", 10)]
		public virtual IList<ContentItem> Questions
		{
			get { return GetChildren(); }
		}
	}
}
