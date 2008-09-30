using System;

namespace N2
{
	public static class ContentItemExtensions
	{
		/// <summary>
		/// Generalised version of <see cref="N2.ContentItem"/> SetValue&lt;<typeparamref name="T"/>&gt;(..)
		/// which may be usefull when the default value is expensive to initialise,
		/// so it's initialisation is delegated to custom provider.
		/// </summary>
		/// <typeparam name="ItemType">The type of reference-type property to retrieve</typeparam>
		/// <param name="item">Object, for which the property to be retrieved</param>
		/// <param name="detailName">Property name</param>
		/// <param name="defaultValueProvider">Delegate that returns default value only when needed.</param>
		/// <returns>Property value OR if it's NULL THEN the value, returned by a provided delegate.</returns>
		public static ItemType GetDetail<ItemType>(
				this ContentItem item,
				string detailName,
				Func<ContentItem, ItemType> defaultValueProvider)
			where ItemType: class//need to be ref type for op?? to work
		{
			return
				item.GetDetail<ItemType>(detailName, default(ItemType))
				?? (null != defaultValueProvider
					? defaultValueProvider(item)
					: default(ItemType));
		}
	}
}
