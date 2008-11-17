using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace N2.Details
{
	public class DetailCollectionListWrapper<TItemValue>: IList<TItemValue>
	{
		#region Fields

		DetailCollection m_dc;

		#endregion Fields

		#region Constructors

		public DetailCollectionListWrapper(DetailCollection source)
		{
			this.m_dc = source;
		}

		#endregion Constructors

		#region IList<TItemValue> Members

		public int IndexOf(TItemValue item)
		{
			return this.m_dc.IndexOf(item);
		}

		public void Insert(int index, TItemValue item)
		{
			this.m_dc.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			this.m_dc.RemoveAt(index);
		}

		public TItemValue this[int index] {
			get {
				return (TItemValue)this.m_dc[index];
			}
			set {
				this.m_dc[index] = value;
			}
		}

		#endregion

		#region ICollection<TItemValue> Members

		public void Add(TItemValue item)
		{
			this.m_dc.Add(item);
		}

		public void Clear()
		{
			this.m_dc.Clear();
		}

		public bool Contains(TItemValue item)
		{
			return this.m_dc.Contains(item);
		}

		public void CopyTo(TItemValue[] array, int arrayIndex)
		{
			for (int i = arrayIndex; i < array.Length; i++)
				array.SetValue(this.m_dc.Details[i].Value, i);
		}

		public int Count {
			get { return this.m_dc.Count; }
		}

		public bool IsReadOnly {
			get { return this.m_dc.IsReadOnly; }
		}

		public bool Remove(TItemValue item)
		{
			var _index = this.m_dc.IndexOf(item);

			if(_index >= 0) {
				this.m_dc.Remove(item);
				return true;
			}

			return false;
		}

		#endregion

		#region IEnumerable<TItemValue> Members

		public IEnumerator<TItemValue> GetEnumerator()
		{
			return this.m_dc
				.Details
				.Select(_cd => _cd.Value)
				.Cast<TItemValue>()
				.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator()
		{
			return
				this.GetEnumerator();
		}

		#endregion
	}
}
