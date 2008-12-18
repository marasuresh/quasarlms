using System.Collections.Generic;

namespace N2
{
	public interface IItemContainer<ItemType>
	{
		/// <summary>
		/// Immediate children of <paramref name="ItemType"/>
		/// </summary>
		IEnumerable<ItemType> Items { get; }
	}

	public interface IItemHierarchyContainer<ItemType>
	{
		/// <summary>
		/// Recursive hierarchy of <paramref name="ItemType"/>
		/// </summary>
		IEnumerable<ItemType> ItemHierarchy { get; }
	}
}
