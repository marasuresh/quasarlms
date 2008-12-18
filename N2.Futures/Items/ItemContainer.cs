using System.Linq;
using System.Collections.Generic;

namespace N2
{
	public class ItemContainer<ItemType> : ContentItem, IItemContainer<ItemType>
	{
		public IEnumerable<ItemType> Items {
			get { return this.GetChildren().OfType<ItemType>(); }
		}

		public IEnumerable<ItemType> ItemHierarchy {
			get { return Find.EnumerateTree(this).OfType<ItemType>(); }
		}
	}
}
