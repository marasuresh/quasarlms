namespace N2.Lms.Items
{
	using System;
	using N2.Details;
	using N2.Integrity;

	[Definition]
	[RestrictParents(typeof(Test))]
	[AllowedChildren]
	public class TestQuestion: ContentItem
	{
		public override string IconUrl { get { return "~/Lms/UI/Img/page_tick.gif"; } }
	}
}
