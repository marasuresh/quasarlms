using System.Collections.Generic;

namespace N2.Lms
{
	using N2.Collections;
	using N2.Lms.Items;
	using System.Linq;
	using N2.Workflow.Items;
	
	internal static class ItemExtensions
	{
		public static IEnumerable<T> FlattenChildHierarchy<T>(this T root)
			where T: ContentItem
		{
			return
				new T[] { root }.Concat(
					root.Children.OfType<T>()
						.SelectMany(_child => FlattenChildHierarchy<T>(_child)));
		}
		
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

		internal static ScheduledTopic ScheduleTopicForTraining(
			this Topic topic,
			Training training)
		{
			ScheduledTopic _topicSchedule = Context.Definitions.CreateInstance<ScheduledTopic>(training);
			
			//Repeat ContentItem.Clone() logic
			_topicSchedule.ID = 0;
			_topicSchedule.Expires = topic.Expires;
			_topicSchedule.Published = topic.Published;
			_topicSchedule.Name = topic.Name;
			_topicSchedule.Title = topic.Title;

			#region clone details and details collections
			foreach (Details.ContentDetail detail in topic.Details.Values) {
				_topicSchedule[detail.Name] = detail.Value;
			}

			foreach (Details.DetailCollection collection in topic.DetailCollections.Values) {
				Details.DetailCollection clonedCollection = collection.Clone();
				clonedCollection.EnclosingItem = _topicSchedule;
				_topicSchedule.DetailCollections[collection.Name] = clonedCollection;
			}
			#endregion clone details and details collections

			#region clone authorized roles
			if (topic.AuthorizedRoles.Any()) {
				foreach (Security.AuthorizedRole _role in topic.AuthorizedRoles) {
					Security.AuthorizedRole _tsRole = _role.Clone();
					_tsRole.EnclosingItem = _topicSchedule;
					_topicSchedule.AuthorizedRoles.Add(_tsRole);
				}
			}
			#endregion clone authorized roles
			
			_topicSchedule.AddTo(training);
			
			return _topicSchedule;
		}
	}
}
