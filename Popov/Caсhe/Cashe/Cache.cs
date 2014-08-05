using System;
using System.Collections.Generic;
using System.Linq;

namespace Cache
{
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

    public class Cache<TKey, TValue> : 
            IGettingValue<TKey, TValue>, 
            ICheckCantainsKeyInCache<TKey>, 
            IGetNumberIncludeCache, 
            IMakeElementsInCacheOld

    {
        private readonly IGettingValue<TKey, TValue> _storage;
        private readonly Dictionary<TKey, Element<TValue>> _cache;
        private int _capacity;
        private readonly TimeSpan _timeLive;
        private const int Defaultcapacity = 10;
        private readonly TimeSpan _defaultTimeLive = new TimeSpan(0, 0, 5);


        int IGetNumberIncludeCache.NumberIncludeCahce { get; set; }

        int IGetNumberIncludeCache.NumberIncludeStorage { get; set; }

        bool ICheckCantainsKeyInCache<TKey>.ContainsInCache(TKey key)
        {
            return _cache.ContainsKey(key);
        }

        void IMakeElementsInCacheOld.MakeElementsOld(int numberElements)
        {
            for (var i = 1; i <= numberElements; ++i)
            {
                RemoveOldPairFromCache();
            }
        }


        public Cache(IGettingValue<TKey, TValue> storage)
        {
            _storage = storage;
            _cache = new Dictionary<TKey, Element<TValue>>();
            _capacity = Defaultcapacity;
            _timeLive = _defaultTimeLive;
        }

        public Cache( IGettingValue<TKey, TValue> storage, int capacity)
            : this(storage)
        {
            _capacity = capacity;
            _timeLive = _defaultTimeLive;
        }

        public Cache(IGettingValue<TKey, TValue> storage, int capacity, TimeSpan timeLive)
            : this(storage, capacity)
        {
            _timeLive = timeLive;
        }


        public TValue this[TKey key]
        {
            get
            {
                RemoveAllOldPairFromCache();
                if (_cache.ContainsKey(key))
                {
                    IncrementIncludeCache();
                    RefreshValueInCache(key);
                    return _cache[key].Value;
                }
                IncrementIncludeStorage();
                AddNewValueInCache(key, _storage[key]);
                return _cache[key].Value;

            }
        }
        private void IncrementIncludeStorage()
        {
            ++ (this as IGetNumberIncludeCache).NumberIncludeStorage;
        }
        private void IncrementIncludeCache()
        {
            ++ (this as IGetNumberIncludeCache).NumberIncludeCahce;
        }


        public int Capacity
        {
            get { return _capacity; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("Capacity must be greater than 1");

                if ((value >= _capacity) || (_cache.Count <= _capacity))
                {
                    _capacity = value;
                }
                else
                {
                    for (var i = 0; i < _capacity - value; ++i)
                    RemoveOldPairFromCache();
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
            return _cache.Count; 
        }

        void RemoveOldPairFromCache()
        {
            if (_cache.Count <= 0) return;

            var maxOldValue = _cache.First();
            foreach (var pair in _cache)
            {
                if (pair.Value.TimeUsage < maxOldValue.Value.TimeUsage)
                {
                    maxOldValue = pair;
                }
            }
            _cache.Remove(maxOldValue.Key);
        }
        
        void RefreshValueInCache(TKey key)
        {
            _cache[key].TimeUsage = DateTime.Now;
        }
        
        void RemoveAllOldPairFromCache()
        {
            var collectKeys = (from pair in _cache
                               where DateTime.Now - pair.Value.TimeUsage > _timeLive
                               select pair.Key).ToArray();
            foreach (var key in collectKeys)
            {
                _cache.Remove(key);
            }
        }

        void AddNewValueInCache(TKey key, TValue value)
        {
            if (_cache.Count >= _capacity)
            {
                RemoveOldPairFromCache();
                _cache.Add(key, new Element<TValue>(value, _timeLive));
            }
            else
            {
                _cache.Add(key, new Element<TValue>(value, _timeLive));
            }
        }
        
    }
}
