using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CacheLib
{
    public class Cache<TKey,TValue>
    {
        private readonly Dictionary<TKey, CacheValue> _dictionary;
        public int Size { get; private set; }
        public int Count { get; private set; }
        public int LifeTime { get; private set; }
        private readonly IStorage<TKey, TValue> _storage;

        private class CacheValue
        {
            public TValue Value { get; set; }
            public DateTime LastAccess { get; set; }
            public CacheValue(TValue value)
            {
                LastAccess = DateTime.Now;
                Value = value;
            }
        }

        public Cache(int size, int lifetime, IStorage<TKey,TValue> storage )
        {
            Size = size;
            LifeTime = lifetime;
            Count = 0;
            _dictionary = new Dictionary<TKey, CacheValue>(size);
            _storage = storage;
        }

        public void Add(TKey key, TValue value)
        {
            var data = new CacheValue(value);

            if (!Exist(key))
            {
                if (Size <= Count)
                {
                    RemoveOldest();
                }
            }
            Console.WriteLine(key + " " + data.Value);

            _dictionary[key] = data;
            Count++;

            Task.Run(async delegate
            {
                await Task.Delay(LifeTime);
                Expire(key);
            });
        }

        private void Expire(TKey key)
        {
            _dictionary.Remove(key);
            Count--;
        }

        private bool Exist(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        private void RemoveOldest()
        {
            var min = DateTime.Now;
            var minkey = default(TKey);
            foreach (var key in _dictionary.Keys)
            {
                if (_dictionary[key].LastAccess < min)
                {
                    min = _dictionary[key].LastAccess;
                    minkey = key;
                }
            }
            Expire(minkey);
        }

        public TValue this[TKey key]
        {
            get
            {
                if (_dictionary.ContainsKey(key))
                {
                    _dictionary[key].LastAccess = DateTime.Now;
                }
                else
                {
                    Add(key, _storage.Get(key));
                }
                return _dictionary[key].Value;
            }
        }

        public TValue Get(TKey key)
        {
            return this[key];
        }
    }
}
