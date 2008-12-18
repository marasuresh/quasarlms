using System.Collections.Generic;

namespace N2
{
	public interface IItemContainer<ItemType>
	{
		IEnumerable<ItemType> Items { get; }

		IEnumerable<ItemType> ItemHierarchy { get; }
	}
}
