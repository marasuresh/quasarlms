using System;
using System.Linq;

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

		//ditto
		public static ChildItemType GetOrFindOrCreateChild<ChildItemType>(
				this ContentItem parent,
				string childName,
				Func<ChildItemType, ChildItemType> mutator)
			where ChildItemType : ContentItem
		{
			/// Attempts to find a first child of a given ChildItemType and reset it's name to childName
			/// adjust existing child name and persist it
			Func<ChildItemType> _defaultValueResolver = () => {
				var _existingItem = parent.Children.OfType<ChildItemType>().FirstOrDefault();
				
				if (null != _existingItem) {
					_existingItem.Name = childName;
					Context.Persister.Save(_existingItem);
					return _existingItem;
				} else {
					return null;
				}
			};
			
			return
				GetOrFindOrCreateChild<ChildItemType>(
					parent,
					childName,
					_defaultValueResolver,
					null,
					mutator);
		}

		internal static ChildItemType GetOrFindOrCreateChild<ChildItemType>(
				this ContentItem parent,
				string childName,
				Func<ChildItemType> resolver,
				Func<ChildItemType> creator,
				Func<ChildItemType, ChildItemType> mutator)
			where ChildItemType : ContentItem
		{
			if (null == resolver) {
				resolver = () => default(ChildItemType);
			}

			if (null == creator) {
				/// Construct new child of a given ChildItemType and inject it into hierarchy
				creator = () => {
					ChildItemType _newChild = Context.Definitions.CreateInstance<ChildItemType>(parent);
					_newChild.Name = childName;
					_newChild.Title = childName;
					_newChild.AddTo(parent);

					//prevent "object references an unsaved transient instance" error
					if (parent.IsPersistent()) {
						Context.Persister.Save(_newChild);
					}

					return _newChild;
				};
			}

			if (null == mutator) {
				mutator = i => i;
			}

			return
				parent.GetChild(childName) as ChildItemType
					?? mutator(resolver() ?? creator());
		}

		public static ChildItemType GetOrCreateChild<ChildItemType>(
				this ContentItem parent,
				string childName)
			where ChildItemType : ContentItem
		{
			return
				GetOrCreateChild<ChildItemType>(parent, childName, null);
		}

		public static ChildItemType GetOrCreateChild<ChildItemType>(
				this ContentItem parent,
				string childName,
				Func<ChildItemType, ChildItemType> mutator)
			where ChildItemType : ContentItem
		{
			return
				GetOrFindOrCreateChild<ChildItemType>(
					parent,
					childName,
					null,
					null,
					mutator);
		}

		public static bool IsPersistent(this ContentItem item)
		{
			var _item = Context.Persister.Get(item.ID);
			return null != _item;
		}
	}
}
