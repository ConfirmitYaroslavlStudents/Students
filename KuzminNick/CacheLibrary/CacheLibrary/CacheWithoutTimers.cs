using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CacheWithoutTimers;

namespace CacheLibraryWithoutTimers
{
    public class CacheWithoutTimers<T> : IDataBase<T>
    {
        private readonly Dictionary<string, Element<T>> _cache;
        private int _capacity;
        private readonly int _limitTimeStoringInCacheInSeconds;
        private readonly Stopwatch _cacheStopwatch = new Stopwatch();
        private readonly IDataBase<T> _slowDataBase;

        public CacheWithoutTimers(int capacity, int limitTimeStoringInCacheInSeconds, IDataBase<T> slowDataBase)
        {
            _cache = new Dictionary<string, Element<T>>();
            _capacity = capacity;
            _limitTimeStoringInCacheInSeconds = limitTimeStoringInCacheInSeconds;
            _slowDataBase = slowDataBase;
            _cacheStopwatch.Start();
        }

        public CacheWithoutTimers(IDataBase<T> slowDataBase) 
            : this(10, 5, slowDataBase)
        { }

        public CacheWithoutTimers(int capacity, IDataBase<T> slowDataBase)
            : this(capacity, 5, slowDataBase)
        { }

        public int Capacity
        {
            get { return _capacity; }
            set
            {
                if (value > 0 && value > _capacity)
                    _capacity = value;
                else
                    throw new ArgumentOutOfRangeException();
            }
        }

        public int Count
        {
            get { return _cache.Count; }
        }
        
        public T GetItemById(string id)
        {
            return GetElementById(id).Value;
        }

        public Element<T> GetElementById(string id)
        {
            if (_cache.ContainsKey(id))
            {
                _cache[id].FrequencyUsage++;
                _cache[id].TimeOfLastUsingInSeconds = _cacheStopwatch.Elapsed.Seconds;
                _cache[id].TypeOfStorage = "Cache";
                return _cache[id];
            }

            var itemFromDataBase = GetItemFromRemoteDataBase(id);
            var element = new Element<T>(id, itemFromDataBase)
            {
                TimeOfLastUsingInSeconds = _cacheStopwatch.Elapsed.Seconds,
                TypeOfStorage = "Data Base"
            };
            AddItem(element);
            return element;
        }

        private T GetItemFromRemoteDataBase(string identifier)
        {
            return _slowDataBase.GetItemById(identifier);
        }

        private void AddItem(Element<T> element)
        {
            RemoveOutdateElementsFromCache();
            if (CacheIsFull())
            {
                var keyRemovedElement = FindKeyLeastUsedElement(element.Identifier);
                _cache.Remove(keyRemovedElement);
            }

            _cache.Add(element.Identifier, element);
        }

        private bool CacheIsFull()
        {
            return Capacity == Count;
        }

        private string FindKeyLeastUsedElement(string keyJustAddedElement)
        {
            var keyRemovedElement = String.Empty;
            var minFrequencyUsage = int.MaxValue;
            foreach (var item in _cache)
            {
                if (item.Key == keyJustAddedElement)
                    continue;

                if (item.Value.FrequencyUsage < minFrequencyUsage)
                {
                    keyRemovedElement = item.Key;
                    minFrequencyUsage = item.Value.FrequencyUsage;
                }
            }
            return keyRemovedElement;
        }

        private void RemoveOutdateElementsFromCache()
        {
            var keysRemovingElements = GetKeysOfRemovingElements();
            RemoveElementsByKeys(keysRemovingElements);
        }

        private List<string> GetKeysOfRemovingElements()
        {
            return _cache
                .Where(pair => IsExceededStorageLimit(pair))
                .Select(pair => pair.Key)
                .ToList();
        }

        private void RemoveElementsByKeys(List<string> keysRemovingElements)
        {
            foreach (var keyRemovingElement in keysRemovingElements)
            {
                _cache.Remove(keyRemovingElement);
            }
        }

        private bool IsExceededStorageLimit(KeyValuePair<string, Element<T>> pair)
        {
            return pair.Value.TimeOfLastUsingInSeconds - _cacheStopwatch.Elapsed.Seconds >=
                   _limitTimeStoringInCacheInSeconds;
        }

        public string GetAllElementsOfCacheInStringFormat()
        {
            var allElementsInStringFormat = "";
            foreach (var cacheElement in _cache)
            {
                Element<T> element = cacheElement.Value;
                allElementsInStringFormat += "|" + element.Value;
            }

            return allElementsInStringFormat;
        }
    }
}
