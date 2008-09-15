using System;
using System.Diagnostics;

namespace N2.Definitions
{
	using N2.Collections;
	using N2.Engine;
	using N2.Persistence;
	
	[AttributeUsage(
		AttributeTargets.Class,
		AllowMultiple = true,
		Inherited = true)]
	public class EnsureChildAttribute: Attribute
	{
		#region Constructors

		public EnsureChildAttribute() { }

		public EnsureChildAttribute(string defaultName, Type childType)
		{
			this.Name = defaultName;
			this.Type = childType;
		}
		
		#endregion Constructors

		#region Properties

		public Type Type { get; set; }
		public string Name { get; set; }

		public IDefinitionManager Definitions { get; set; }
		public IPersister Persister { get; set; }

		#endregion Properties

		public void UpdateItem(ContentItem item)
		{
			ItemList _children = item.GetChildren(new TypeFilter(this.Type));
			
			if (0 == _children.Count) {

				Debug.WriteLine("Recreating child item of type " + this.Type.Name);

				var _child = this.Definitions.CreateInstance(this.Type, item);
				_child.Name = this.Name;
				_child.Title = this.Name;
//TODO Ensure proper moment to persist
				_child.AddTo(item);
//				this.Persister.Save(_child);
			}
		}
	}
}
