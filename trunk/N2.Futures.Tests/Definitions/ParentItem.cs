namespace N2.Tests.Definitions.Definitions
{
	using N2;
	using N2.Definitions;

	[Definition]
	[EnsureChild("Child", typeof(ChildItem))]
	public class ParentItem: ContentItem
	{
	}
}
