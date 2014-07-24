using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace CacheLibrary
{
    public class Cache<T>
    {
        private readonly Dictionary<string, Element<T>> _cache;
        private int _capacity;
        private int _limitTimeStoringInCacheInSeconds;
        private readonly Stopwatch _cacheStopwatch = new Stopwatch();
        private TimerCallback _timerCallingRefreshing;

        public Cache()
        {
            _cache = new Dictionary<string, Element<T>>();
            _capacity = 10;
            _limitTimeStoringInCacheInSeconds = 10;
            ActivateStopwatch();
            ActivateTimerRefresher();
        }

        public Cache(int capacity)
        {
            _cache = new Dictionary<string, Element<T>>();
            _capacity = capacity;
            _limitTimeStoringInCacheInSeconds = 10;
            ActivateStopwatch();
            ActivateTimerRefresher();
        }

        public Cache(int capacity, int limitTimeStoringInCacheInSeconds)
        {
            _capacity = capacity;
            _limitTimeStoringInCacheInSeconds = limitTimeStoringInCacheInSeconds;
            ActivateStopwatch();
            ActivateTimerRefresher();
        }

        public int LimitTimeStoringInCacheInSeconds
        {
            get { return _limitTimeStoringInCacheInSeconds; }
            set { _limitTimeStoringInCacheInSeconds = value; }
        }

        public int Capacity
        {
            get { return _capacity; }
            set
            {
                if (value > 0 && value > _capacity)
                    _capacity = value;

                throw new ArgumentOutOfRangeException();
            }
        }

        public int CurrentSize
        {
            get { return _cache.Count; }
        }

        private string IdentifierMostUsedElement { get; set; }

        public Element<T> GetItem(string identifier)
        {
            if (_cache.ContainsKey(identifier))
            {
                _cache[identifier].FrequencyUsage++;
                _cache[identifier].TimeLastUsingInSeconds = _cacheStopwatch.Elapsed.Seconds;
                return _cache[identifier];
            }

            return GetItemFrowRemotingDataBase();
        }

        // TODO!!
        private Element<T> GetItemFrowRemotingDataBase()
        {
            return null;
        }

        private void ActivateStopwatch()
        {
            _cacheStopwatch.Start();
        }

        private void ActivateTimerRefresher()
        {
            _timerCallingRefreshing = RefreshCache;
            new Timer(_timerCallingRefreshing, null, 0, 10);
        }

        public void AddItem(Element<T> element)
        {
            if (CacheIsFull())
            {
                var keyRemovedElement = FindKeyLeastUsedElement();
                _cache.Remove(keyRemovedElement);
            }

            _cache.Add(element.Identifier, element);
        }

        private bool CacheIsFull()
        {
            return Capacity == CurrentSize;
        }

        private string FindKeyLeastUsedElement()
        {
            var keyRemovedElement = IdentifierMostUsedElement;
            foreach (var item in _cache)
            {
                if (item.Value.FrequencyUsage == 0)
                {
                    keyRemovedElement = item.Key;
                    break;
                }

                if (item.Value.FrequencyUsage < _cache[keyRemovedElement].FrequencyUsage)
                {
                    keyRemovedElement = item.Key;
                }
            }
            return keyRemovedElement;
        }

        public string GetAllElementsCacheInStringFormat()
        {
            var allElementsInStringFormat = "";
            foreach (var cacheElement in _cache)
            {
                Element<T> element = cacheElement.Value;
                allElementsInStringFormat += "|" + element.Value;
            }

            return allElementsInStringFormat;
        }

        private void RefreshCache(object state)
        {
            var keysRemovingElements = GetKeysRemovingElements();
            RemoveElementsByKeys(keysRemovingElements);
            IdentifierMostUsedElement = GetIdentifierMostUsedElement();
        }

        private List<string> GetKeysRemovingElements()
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
            return pair.Value.TimeLastUsingInSeconds - _cacheStopwatch.Elapsed.Seconds >=
                   LimitTimeStoringInCacheInSeconds;
        }

        private string GetIdentifierMostUsedElement()
        {
            var updatedIdentifierMostUsedElement = IdentifierMostUsedElement;
            foreach (var element in _cache)
            {
                if (element.Value.FrequencyUsage > _cache[IdentifierMostUsedElement].FrequencyUsage)
                {
                    updatedIdentifierMostUsedElement = element.Value.Identifier;
                }
            }
            return updatedIdentifierMostUsedElement;
        }
    }
}
