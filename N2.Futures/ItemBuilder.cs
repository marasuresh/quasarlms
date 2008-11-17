using System;

namespace N2.Definitions
{
	using System.Diagnostics;
	using Castle.Core;
	using N2.Persistence;
	using N2.Plugin;

	[Obsolete]
	public class ItemBuilder : IStartable, IAutoStart
	{
		readonly IPersister persister;
		readonly IDefinitionManager definitions;

		public ItemBuilder(IPersister persister, IDefinitionManager definitions)
		{
			this.persister = persister;
			this.definitions = definitions;
		}

		public void ApplyDefinitions(ContentItem item)
		{
			foreach (EnsureChildAttribute _attr in item.GetType().GetCustomAttributes(typeof(EnsureChildAttribute), true)) {
//TODO rework to dependency injection
				_attr.Definitions = this.definitions;
				_attr.Persister = this.persister;

				_attr.UpdateItem(item);
			}
		}

		#region IStartable Members

		public void Start()
		{
			this.persister.ItemSaving += this.OnItemSaving;

			Debug.WriteLine("Event handlers attached");
		}

		public void Stop()
		{
			this.persister.ItemSaving -= this.OnItemSaving;

			Debug.WriteLine("Event handlers detached");
		}

		#endregion

		#region Event handlers

		void OnItemSaving(object sender, CancellableItemEventArgs e)
		{
			Debug.WriteLine("Applying definitions");

			this.ApplyDefinitions(e.AffectedItem);
		}

		#endregion Event handlers
	}
}
