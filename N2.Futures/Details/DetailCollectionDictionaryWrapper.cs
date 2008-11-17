using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections;

namespace N2.Details
{
	/// <summary>
	/// A mutable wrapper over N2 DetailCollection.
	/// Allows to expose DetailCollection of a given ContentItem
	/// as a strongly typed Dictionary, able to accept any changes to it.
	/// </summary>
	/// <typeparam name="TItemValueType">Collection item type</typeparam>
	public class DetailCollectionDictionaryWrapper<TItemValue>: IDictionary<string, TItemValue>
	{
		#region Fields
		
		DetailCollection m_dc;

		#endregion Fields

		#region Constructors

		public DetailCollectionDictionaryWrapper(DetailCollection originalCollection)
		{
			this.m_dc = originalCollection;
		}

		#endregion Constructors

		#region Methods

		protected virtual int IndexOf(string key)
		{
			for (int i = 0; i < this.m_dc.Details.Count; i++)
				if (this.m_dc.Details[i].Name == key)
					return i;
			return -1;
		}

		#endregion Methods

		#region IDictionary<string,TItemValueType> Members

		public void Add(string key, TItemValue value)
		{
			var _cd = ContentDetail.New(this.m_dc.EnclosingItem, key, value);
			_cd.EnclosingCollection = this.m_dc;
			this.m_dc.Add(_cd);
		}

		public bool ContainsKey(string key)
		{
			return this.m_dc.Details.Any(_cd => _cd.Name == key);
		}

		public ICollection<string> Keys {
			get {
				return
					new ReadOnlyCollection<string>(
						this.m_dc
							.Details
							.Select(_cd => _cd.Name)
							.ToList()
					);
			}
		}

		public bool Remove(string key)
		{
			TItemValue @value;
			
			if(this.TryGetValue(key, out @value)) {
				this.m_dc.Remove(@value);
				return true;
			}
			
			return false;
		}

		public bool TryGetValue(string key, out TItemValue value)
		{
			return null ==
				(value = (TItemValue)this.m_dc
					.Details
					.Where(_cd => _cd.Name == key)
					.Select(_cd => _cd.Value)
					.FirstOrDefault());
		}

		public ICollection<TItemValue> Values
		{
			get {
				return
					new ReadOnlyCollection<TItemValue>(
						this.m_dc
							.Details
							.Select(_cd => _cd.Value)
							.Cast<TItemValue>()
							.ToList()
					);
			}
		}

		public TItemValue this[string key] {
			get {
				var _index = this.IndexOf(key);
				return
					_index >= 0
						? (TItemValue)this.m_dc[_index]
						: default(TItemValue);
			}
			set {
				var _index = this.IndexOf(key);
				if (_index >= 0) {
					if (null != value) {
						this.m_dc[this.IndexOf(key)] = ContentDetail.New(this.m_dc.EnclosingItem, key, value);
					} else {
						this.Remove(key);
					}
				} else if(null != value) {
					this.Add(key, value);
				}
			}
		}

		#endregion

		#region ICollection<KeyValuePair<string,TItemValueType>> Members

		public void Add(KeyValuePair<string, TItemValue> item)
		{
			this.Add(item.Key, item.Value);
		}

		public void Clear()
		{
			this.m_dc.Clear();
		}

		public bool Contains(KeyValuePair<string, TItemValue> item)
		{
			return
				this.m_dc
					.Details
					.Where(_cd => _cd.Name == item.Key)
					.Any(_cd => _cd.Value == null && item.Value == null
						|| item.Value.Equals(_cd.Value));
		}

		public void CopyTo(KeyValuePair<string, TItemValue>[] array, int arrayIndex)
		{
			for (int i = arrayIndex; i < array.Length; i++)
				array.SetValue(
					new KeyValuePair<string, TItemValue>(
						this.m_dc.Details[i].Name,
						(TItemValue)this.m_dc.Details[i].Value),
				i);
		}

		public int Count
		{
			get { return this.m_dc.Count; }
		}

		public bool IsReadOnly
		{
			get { return this.m_dc.IsReadOnly; }
		}

		public bool Remove(KeyValuePair<string, TItemValue> item)
		{
			return this.Remove(item.Key);
		}

		#endregion

		#region IEnumerable<KeyValuePair<string,TItemValueType>> Members

		public IEnumerator<KeyValuePair<string, TItemValue>> GetEnumerator()
		{
			return
				this.m_dc
					.Details
					.ToDictionary(
						_cd => _cd.Name,
						_cd => (TItemValue)_cd.Value)
					.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_dc.Details.GetEnumerator();
		}

		#endregion
	}
}
