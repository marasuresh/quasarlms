using System.Collections.Generic;

namespace N2.Lms
{
	using N2.Collections;
	
	internal static class ItemExtensions
	{
		/// <summary>
		/// Appends cloned items to a given Item List,
		/// applying ItemFilter and setting Parent property.
		/// </summary>
		/// <param name="items">Items to append to</param>
		/// <param name="virtualItems">Items to clone and append</param>
		/// <param name="parent">Parent to set for cloned items</param>
		/// <param name="filter">Filter to apply to cloned items</param>
		/// <returns>Original items with cloned items appended</returns>
		public static ItemList AppendItemsAsVirtual(
			this ItemList items,
			IEnumerable<ContentItem> virtualItems,
			ContentItem parent,
			ItemFilter filter)
		{
			//filter items
			var _newItems = new ItemList(virtualItems, filter);
			
			foreach (ContentItem _i in _newItems) {
				var _virtualItem = _i.Clone(true);
				_virtualItem.Parent = parent;
				items.Add(_virtualItem);
			}

			return items;
		}
	}
}
