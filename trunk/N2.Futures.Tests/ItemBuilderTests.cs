using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N2.Tests.Definitions
{
	using System.Linq;
	using NUnit.Framework;
	using N2.Definitions;
	using N2.Tests.Definitions.Definitions;
	
	[TestFixture]
	public class ItemBuilderTests : Persistence.DatabasePreparingBase
	{
		[Test]
		public void ItemBuilderLoaded()
		{
			Assert.IsNotNull(engine.Resolve<ItemBuilder>());
		}

		[Test]
		public void ChildItemAutoCreatedOnceForANewParent()
		{
			ContentItem _parent = this.CreateParentItem();

			//Perform several times to ensure uniqueness of child creation
			foreach (var i in Enumerable.Range(0, 3)) {
				engine.Persister.Save(_parent);
				_parent = engine.Persister.Get(_parent.ID);
				Assert.AreEqual(_parent.Children.Count, 1);
			}

			Assert.IsInstanceOfType(typeof(ChildItem), _parent.Children.FirstOrDefault());
		}

		[Test]
		public void ChildItemAutoCreatedForExisitingParent()
		{
			ContentItem _parent = this.CreateParentItem();
			//Save item, bypassing N2 engine
			engine.Persister.Repository.Save(_parent);

			_parent = engine.Persister.Get(_parent.ID);
			Assert.IsEmpty(_parent.Children.ToList());

			engine.Persister.Save(_parent);
			_parent = engine.Persister.Get(_parent.ID);
			Assert.IsNotEmpty(_parent.Children.ToList());
		}

		protected ContentItem CreateParentItem()
		{
			return engine.Definitions.CreateInstance(typeof(ParentItem), null);
		}
	}
}
