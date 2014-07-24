using System;
using System.Collections.Generic;
using System.Linq;

namespace Cache
{
    public interface ICashe<TKey, TValue> 
    {
        TValue GetValue(TKey key);
    }

    class Element<T>
    {
        private DateTime _timeLastUse;
        private readonly TimeSpan _timeLive;
       
        public Element(T value, TimeSpan timeLive)
        {
            Value = value;
            _timeLastUse = DateTime.Now;
            _timeLive = timeLive;
        }

        public T Value { get; private set; }

        public DateTime TimeUsage
        {
            get { return _timeLastUse; }
            set
            {
                if (value >= _timeLastUse)
                {
                    _timeLastUse = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("New treatment must be greater previous treatment");
                }
            }
        }

        public TimeSpan TimeLive
        {
            get { return _timeLive; }
        }
    }

    public class Cache<TKey, TValue> where TValue : class
    {
        private readonly ICashe<TKey, TValue> _storage;
        private readonly Dictionary<TKey, Element<TValue>> _cashe;
        private int _capacity;
        private readonly TimeSpan _timeLive;
        private const int Defaultcapacity   = 10;
        private readonly TimeSpan _defaultTimeLive   = new TimeSpan(0, 0, 5);
        

        public Cache(ICashe<TKey, TValue> storage)
        {
            _storage = storage;
            _cashe = new Dictionary<TKey, Element<TValue>>();
            _capacity = Defaultcapacity;
            _timeLive = _defaultTimeLive;
        }

        public Cache( ICashe<TKey, TValue> storage, int capacity)
            : this(storage)
        {
            _capacity = capacity;
            _timeLive = _defaultTimeLive;
        }

        public Cache(ICashe<TKey, TValue> storage, int capacity, TimeSpan timeLive)
            : this(storage, capacity)
        {
            _timeLive = timeLive;
        }


        public TValue this[TKey key]
        {
            get
            {
                RemoveAllOldPairFromCashe();
                if (_cashe.ContainsKey(key))
                {
                    RefreshValueInCashe(key);
                    return _cashe[key].Value;
                }
                AddNewValueInCashe(key, _storage.GetValue(key));
                return _cashe[key].Value;

            }
        }

        public int Capacity
        {
            get { return _capacity; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("Capacity must be greater than 1");

                if ((value >= _capacity) || (_cashe.Count <= _capacity))
                {
                    _capacity = value;
                }
                else
                {
                    for (var i = 0; i < _capacity - value; ++i)
                        RemoveOldPairFromCashe();
                    _capacity = value;
                }
            }
        }

        public int DefaultCapacity
        {
            get { return Defaultcapacity; }
        }

        public TimeSpan TimeLive
        {
            get { return _timeLive; }
        }

        public int GetCount()
        {
            return _cashe.Count; 
        }

        void RemoveOldPairFromCashe()
        {
            if (_cashe.Count > 0)
            {
                var maxOldValue = _cashe.First();
                foreach (var pair in _cashe)
                {
                    if (pair.Value.TimeUsage < maxOldValue.Value.TimeUsage)
                    {
                        maxOldValue = pair;
                    }
                }
                _cashe.Remove(maxOldValue.Key);
            }
        }
        
        void RefreshValueInCashe(TKey key)
        {
            _cashe[key].TimeUsage = DateTime.Now;
        }
        
        void RemoveAllOldPairFromCashe()
        {
            var collectKeys = (from pair in _cashe
                               where DateTime.Now - pair.Value.TimeUsage > pair.Value.TimeLive
                               select pair.Key).ToArray();
            foreach (var key in collectKeys)
            {
                _cashe.Remove(key);
            }
        }

        void AddNewValueInCashe(TKey key, TValue value)
        {
            if (_cashe.Count >= _capacity)
            {
                RemoveOldPairFromCashe();
                _cashe.Add(key, new Element<TValue>(value, _timeLive));
            }
            else
            {
                _cashe.Add(key, new Element<TValue>(value, _timeLive));
            }
        }
        
    }
}
