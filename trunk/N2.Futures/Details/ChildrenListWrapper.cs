using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N2.Collections
{//TODO finish implementation
	public abstract class ChildrenListWrapper<TItem>: IList<TItem>
		where TItem: ContentItem
	{
		#region Fields

		ItemList m_list;
		ContentItem m_owner;
		
		#endregion Fields

		#region Constructors

		internal ChildrenListWrapper(ContentItem owner, ItemList list)
		{
			this.m_list = list;
			this.m_owner = owner;
		}

		internal ChildrenListWrapper(ContentItem owner)
			: this(owner, owner.GetChildren(new TypeFilter(typeof(TItem))))
		{
		}

		#endregion Constructors

		#region IList<TItem> Members

		public int IndexOf(TItem item)
		{
			return this.m_list.IndexOf(item);
		}

		public void Insert(int index, TItem item)
		{
			this.m_list.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			this.m_list.RemoveAt(index);
		}

		public TItem this[int index] {
			get { return (TItem)this.m_list[index]; }
			set { this.m_list[index] = value; }
		}

		#endregion

		#region ICollection<TItem> Members

		public void Add(TItem item)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public bool Contains(TItem item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(TItem[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public int Count
		{
			get { throw new NotImplementedException(); }
		}

		public bool IsReadOnly
		{
			get { throw new NotImplementedException(); }
		}

		public bool Remove(TItem item)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IEnumerable<TItem> Members

		public IEnumerator<TItem> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
