using System;
using System.Collections.Generic;
using System.Linq;

namespace Cache
{
    public class Element<T>
    {
        private readonly ITime<T> _timeCache;
        private DateTime _timeLastUse;

        public Element(T value, ITime<T> time)
        {
            Value = value;
            _timeLastUse = time.CurrenTime;
            _timeCache = time;
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
            get { return _timeCache.TimeLive; }
        }

       
    }


    public class Cache<TKey, TValue> :
        IGettingValue<TKey, TValue>,
        IGetNumberIncludeCache
    {
        private const int Defaultcapacity = 10;
        private readonly Dictionary<TKey, Element<TValue>> _cache;
        private readonly TimeSpan _timeLive;
        private readonly IGettingValue<TKey, TValue> _storage;
        private readonly ITime<TValue> _time;
        private int _capacity;


        public Cache(IGettingValue<TKey, TValue> storage, ITime<TValue> time)
        {
            _storage = storage;
            _cache = new Dictionary<TKey, Element<TValue>>();
            _capacity = Defaultcapacity;
            _timeLive = time.TimeLive;
            _time = time;
        }

        public Cache(IGettingValue<TKey, TValue> storage, ITime<TValue> time, int capacity)
            : this(storage, time)
        {
            _capacity = capacity;
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
                    for (int i = 0; i < _capacity - value; ++i)
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

        public bool ContainsInCache(TKey key)
        {
            return _cache.ContainsKey(key);
        }

        int IGetNumberIncludeCache.NumberIncludeCahce { get; set; }

        int IGetNumberIncludeCache.NumberIncludeStorage { get; set; }

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

        public int GetCount()
        {
            RemoveAllOldPairFromCache();
            return _cache.Count;
        }

        private void RemoveOldPairFromCache()
        {
            if (_cache.Count <= 0) return;

            var maxOldValue = _cache.First();
            foreach (var pair in _cache.Where(pair => pair.Value.TimeUsage < maxOldValue.Value.TimeUsage))
            {
                maxOldValue = pair;
            }
            _cache.Remove(maxOldValue.Key);
        }

        private void RefreshValueInCache(TKey key)
        {
            _cache[key].TimeUsage = _time.CurrenTime;
        }

        private void RemoveAllOldPairFromCache()
        {
            TKey[] collectKeys = (from pair in _cache
                where _time.IsOldElement(pair.Value)
                select pair.Key).ToArray();
            foreach (TKey key in collectKeys)
            {
                _cache.Remove(key);
            }
        }

        private void AddNewValueInCache(TKey key, TValue value)
        {
            if (_cache.Count >= _capacity)
            {
                RemoveOldPairFromCache();
                _cache.Add(key, new Element<TValue>(value, _time));
            }
            else
            {
                _cache.Add(key, new Element<TValue>(value, _time));
            }
        }
    }
}