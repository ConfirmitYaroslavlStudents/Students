using System;
using System.Collections.Generic;
using System.Linq;


namespace Cashe
{

    class Element<T> where T : IComparable<T>, IEquatable<T>
    {

        private DateTime _timeLastUse;
        
        public Element(T value)
        {
            Value = value;
            _timeLastUse = DateTime.Now;
        }

        public T Value { get; private set; }

        public DateTime TimeUsage
        {
            get { return _timeLastUse; }
            set
            {
                if (value > _timeLastUse)
                {
                    _timeLastUse = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("New treatment must be greater previous treatment");
                }
            }
        }
    }

    public class Cashe<TKey, TValue> where TValue : IComparable<TValue>, IEquatable<TValue>
    {
        private Dictionary<TKey, TValue> _slowDictionary;
        private Dictionary<TKey, Element<TValue>> _fastDictionary;
        private int _capacity;
        
        public Cashe()
        {
            ClearOrInitial();
        }

        public Cashe(int capacity)
        {
            ClearOrInitial();
            _capacity = capacity;
        }

        public Cashe(int capacity, IEnumerable<KeyValuePair<TKey, TValue>> values)
        {
            ClearOrInitial();
            foreach (var pair in values)
            {
                _slowDictionary.Add(pair.Key, pair.Value);
            }
            _capacity = capacity;
        }


        /// <summary>
        /// Adds a new value with its key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            _slowDictionary.Add(key, value);
        }

        /// <summary>
        /// Adds a collection of new values with their keys
        /// </summary>
        /// <param name="values"></param>
        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> values)
        {
            foreach (var pair in values)
            {
                _slowDictionary.Add(pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// Clear or initial cashe
        /// </summary>
        public void ClearOrInitial()
        {
            _capacity = 10;
            if (_slowDictionary != null)
            {
                _slowDictionary.Clear();
            }
            else
            {
                _slowDictionary = new Dictionary<TKey, TValue>();
            }
            if (_fastDictionary != null)
            {
                _fastDictionary.Clear();
            }
            else
            {
                _fastDictionary = new Dictionary<TKey, Element<TValue>>();
            }
        }

        /// <summary>
        /// Determines whether the cache this key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            return ((_slowDictionary.ContainsKey(key)) || (_fastDictionary.ContainsKey(key)));
        }

        /// <summary>
        /// Determines whether the cache this value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ContainsValue(TValue value)
        {
            return ((_slowDictionary.ContainsValue(value)) || ContainsValueInFastDictionary(value));
        }

        /// <summary>
        /// Remove key-value pair from this cashe
        /// </summary>
        /// <param name="key"></param>
        public void Remove(TKey key)
        {
            if (_fastDictionary.ContainsKey(key))
            {
                _fastDictionary.Remove(key);
            }
            else
            {
                _slowDictionary.Remove(key);
            }
        }

        /// <summary>
        /// Remove old key-value pair into fast dictionary and add this pair in slow dictionary
        /// </summary>
        void RemoveOldPairFromFastDictionary()
        {
            var maxOldValue = _fastDictionary.First();
            foreach (var pair in _fastDictionary)
            {
                if (pair.Value.TimeUsage < maxOldValue.Value.TimeUsage)
                {
                    maxOldValue = pair;
                }
            }
            _fastDictionary.Remove(maxOldValue.Key);
            _slowDictionary.Add(maxOldValue.Key,maxOldValue.Value.Value);
        }

        /// <summary>
        /// Determines whether the fast dictionary this value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool ContainsValueInFastDictionary(TValue value)
        {
            foreach (var item in _fastDictionary)
            {
                if (item.Value.Value.Equals(value))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Add new key-value pair in fast dictionary or refresh time last using this value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void AddNewOrRefreshFromFastDictionary(TKey key, TValue value)
        {
            if (_fastDictionary.ContainsKey(key))
            {
                _fastDictionary[key].TimeUsage = DateTime.Now;
            }
            else
            {
                if (_fastDictionary.Count < _capacity)
                {
                    _fastDictionary.Add(key, new Element<TValue>(value));
                }
                else
                {
                    RemoveOldPairFromFastDictionary();
                    _fastDictionary.Add(key, new Element<TValue>(value));
                }
            }
        }

        
        public TValue this[TKey key]
        {
            get
            {
                if (_fastDictionary.ContainsKey(key))
                {
                    AddNewOrRefreshFromFastDictionary(key, _fastDictionary[key].Value);
                    return _fastDictionary[key].Value;
                }
                if (_slowDictionary.ContainsKey(key))
                {
                    AddNewOrRefreshFromFastDictionary(key, _slowDictionary[key]);
                    _slowDictionary.Remove(key);
                    return _fastDictionary[key].Value;
                }
                throw new KeyNotFoundException("Key not found");
            }
            set
            {
                if (_fastDictionary.ContainsKey(key))
                {
                    _fastDictionary[key] = new Element<TValue>(value);
                    return;
                }
                if (_slowDictionary.ContainsKey(key))
                {
                    AddNewOrRefreshFromFastDictionary(key,value);
                    _slowDictionary.Remove(key);
                    return;
                }
                throw new KeyNotFoundException("Key not found");
            }
        }

        /// <summary>
        /// Get count cashe
        /// </summary>
        public int Count
        {
            get { return _slowDictionary.Count + _fastDictionary.Count; }
        }

        /// <summary>
        /// Get capacity cashe
        /// </summary>
        public int Capacity
        {
            get { return _capacity; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("Capacity must be greater than 1");
                
                if ((value >= _capacity)||(_fastDictionary.Count <= _capacity))
                {
                    _capacity = value;
                }
                else
                {
                    for (int i = 0; i < _capacity-value; ++i)
                        RemoveOldPairFromFastDictionary();
                    _capacity = value;
                }
            }
        }
    }
}
