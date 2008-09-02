namespace N2.Tests.Definitions.Definitions
{
	using N2;
	using N2.Integrity;

	[Definition]
	[RestrictParents(typeof(ParentItem))]
	public class ChildItem: ContentItem
	{
	}
}
