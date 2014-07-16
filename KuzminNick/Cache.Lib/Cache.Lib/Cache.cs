using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace CacheLib
{
    public class Element<T>
    {
        private T _value;
        private readonly int _hashCode;
        private int _frequenceUsage;
        private int _timeLastUsingInSeconds;

        public Element()
        {
            FrequencyUsage = 0;
        }

        public Element(T value)
            : this()
        {
            Value = value;
            FrequencyUsage = 0;
        }

        public Element(int hashCode, T value)
            : this()
        {
            _hashCode = hashCode;
            Value = value;
        }

        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public int HashCode
        {
            get { return _hashCode; }
        }

        public int FrequencyUsage
        {
            get { return _frequenceUsage; }
            set { _frequenceUsage = value; }
        }
        public int TimeLastUsingInSeconds
        {
            get { return _timeLastUsingInSeconds; }
            set { _timeLastUsingInSeconds = value; }
        }
    }

    public class Cache<T>
    {
        private readonly Dictionary<int, Element<T>> _cache;
        private int _capacity;
        private int _limitTimeStoringInCacheInSeconds;
        private Stopwatch _cacheStopwatch = new Stopwatch();
        private TimerCallback _timerCallingRefreshing;

        public Cache()
        {
            _cache = new Dictionary<int, Element<T>>();
            _capacity = 10;
            _limitTimeStoringInCacheInSeconds = 10;
            ActivateStopwatch();
            ActivateTimerRefresher();
        }

        public Cache(int capacity)
        {
            _cache = new Dictionary<int, Element<T>>();
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
                if(value > 0 && value > _capacity)
                    _capacity = value;

                throw new ArgumentOutOfRangeException();
            }
        }

        public int CurrentSize
        {
            get { return _cache.Count; }
        }

        private int KeyMostUsedElement { get; set; }

        public Element<T> GetItem(IEnumerable<Element<T>> collection, int hashCode)
        {
            if (_cache.ContainsKey(hashCode))
            {
                _cache[hashCode].FrequencyUsage++;
                _cache[hashCode].TimeLastUsingInSeconds = _cacheStopwatch.Elapsed.Seconds;
                return _cache[hashCode];
            }

            // 'slow' getting data from some IEnumerable Structure
            foreach (var element in collection)
            {
                if (!element.GetHashCode().Equals(hashCode)) continue;

                AddItem(element);
                return element;
            }

            throw new NullReferenceException();
        }
        private void ActivateStopwatch()
        {
            _cacheStopwatch.Start();
        }

        private void ActivateTimerRefresher()
        {
            _timerCallingRefreshing = new TimerCallback(RefreshCache);
            var time = new Timer(_timerCallingRefreshing, null, 0, 10);
        }

        public void AddItem(Element<T> element)
        {
            if (CacheIsFull())
            {
                var keyRemovedElement = FindKeyLeastUsedElement();
                _cache.Remove(keyRemovedElement);
            }

            _cache.Add(element.Value.GetHashCode(), element);
        }

        private bool CacheIsFull()
        {
            return Capacity == CurrentSize;
        }

        private int FindKeyLeastUsedElement()
        {
            var keyRemovedElement = KeyMostUsedElement;
            foreach (var item in _cache)
            {
                if (item.Value.FrequencyUsage == 0)
                {
                    keyRemovedElement = item.Key;
                    break;
                }

                if (item.Value.FrequencyUsage < keyRemovedElement)
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
            var keysRemovingElements = _cache
                .Where(
                    pair =>
                        pair.Value.TimeLastUsingInSeconds - _cacheStopwatch.Elapsed.Seconds >=
                        LimitTimeStoringInCacheInSeconds)
                .Select(pair => pair.Key)
                .ToList();

            foreach (var keyRemovingElement in keysRemovingElements)
            {
                _cache.Remove(keyRemovingElement);
            }
        }
    }
}