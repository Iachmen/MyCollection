using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCollection.Collections
{
    class MyCollection<TKeyFirst, TKeySecond, TValue> : IDictionary<KeyPair<TKeyFirst, TKeySecond>, TValue>
    {
		private object locker = new object();
        Dictionary<KeyPair<TKeyFirst, TKeySecond>, TValue> dict;
        Dictionary<TKeyFirst, List<TValue>> dictById;
        Dictionary<TKeySecond, List<TValue>> dictByName;

        public MyCollection()
        {
            dict = new Dictionary<KeyPair<TKeyFirst, TKeySecond>, TValue>();
            dictById = new Dictionary<TKeyFirst, List<TValue>>();
            dictByName = new Dictionary<TKeySecond, List<TValue>>();
        }

        public TValue[] GetValuesById(TKeyFirst id)
        {
            return (dictById.ContainsKey(id)) ? dictById[id].ToArray() : null;
        }

        public void Add(TKeyFirst keyFirst, TKeySecond keySecond, TValue value)
        {
            Add(new KeyPair<TKeyFirst, TKeySecond>(keyFirst, keySecond), value);
        }
        public TValue[] GetValuesByName(TKeySecond name)
        {
            return (dictByName.ContainsKey(name)) ? dictByName[name].ToArray() : null;
        }

        public TValue this[KeyPair<TKeyFirst, TKeySecond> key] 
		{ 
			get  
			{
				TValue value;
				if (!this.TryGetValue(key, out value))
					throw new KeyNotFoundException("This key exists");
				return dict[key];
			}
            set
			{
				this.Add(key, value);   
			}	
		}

        public ICollection<KeyPair<TKeyFirst, TKeySecond>> Keys
		{
			get { return dict.Keys; }
        }

        public ICollection<TValue> Values 
		{
			get { return dict.Values; }
        }

        public int Count
		{
			get { return dict.Count; }
        }

        public bool IsReadOnly  
		{
			get { return true; }
        }

        public void Add(KeyPair<TKeyFirst, TKeySecond> key, TValue value)
        {
            if (dict.ContainsKey(key))
            {
                throw new ArgumentException();
            }
			lock (locker)
			{
				dict[key] = value;
				if (!dictById.ContainsKey(key.ID))
				{
					dictById[key.ID] = new List<TValue>();
				}
				if (!dictByName.ContainsKey(key.Name))
				{
					dictByName[key.Name] = new List<TValue>();
				}
				dictById[key.ID].Add(value);
				dictByName[key.Name].Add(value);
			}         
        }

        public void Add(KeyValuePair<KeyPair<TKeyFirst, TKeySecond>, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
			lock (locker)
			{
				dict.Clear();
				dictById.Clear();
				dictByName.Clear();
			}           
        }

        public bool Contains(KeyValuePair<KeyPair<TKeyFirst, TKeySecond>, TValue> item)
        {
            return dict[item.Key].Equals(item.Value);
        }

        public bool ContainsKey(KeyPair<TKeyFirst, TKeySecond> key)
        {
            return dict.ContainsKey(key);
        }

        public IEnumerator<KeyValuePair<KeyPair<TKeyFirst, TKeySecond>, TValue>> GetEnumerator()
        {
            foreach (var item in dict)
            {
                yield return item;
            }
        }

        public bool Remove(KeyPair<TKeyFirst, TKeySecond> key)
        {
            if (dict.ContainsKey(key))
            {
				lock (locker)
				{
					dict.Remove(key);
					dictById.Remove(key.ID);
					dictByName.Remove(key.Name);
				}         
                return true;
            }
            return false;
        }

        public bool Remove(KeyValuePair<KeyPair<TKeyFirst, TKeySecond>, TValue> item)
        {
            if (dict.Contains(item))
            {
				lock (locker)
				{
					dict.Remove(item.Key);
					dictById[item.Key.ID].Remove(item.Value);
					dictByName[item.Key.Name].Remove(item.Value);
				}               
                return true;
            }
            return false;
        }

        public bool TryGetValue(KeyPair<TKeyFirst, TKeySecond> key, out TValue value)
        {
            value = dict[key];
            return value != null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dict.GetEnumerator();
        }

        public void CopyTo(KeyValuePair<KeyPair<TKeyFirst, TKeySecond>, TValue>[] array, int arrayIndex)
        {
             Copy(dict, array, arrayIndex);
        }
       private void Copy<TValue>(ICollection<TValue> source, TValue[] array, int arrayIndex) 
	   {
        if (array == null)
            throw new ArgumentNullException("array");

        if (arrayIndex < 0 || arrayIndex > array.Length)
            throw new ArgumentOutOfRangeException("arrayIndex");

        if ((array.Length - arrayIndex) < source.Count)
            throw new ArgumentException("Destination array is not large enough. Check array.Length and arrayIndex.");

        foreach (TValue item in source)
            array[arrayIndex++] = item;
		}
    }
}
