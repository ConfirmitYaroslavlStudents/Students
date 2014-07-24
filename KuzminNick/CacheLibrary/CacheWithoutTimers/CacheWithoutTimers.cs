using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CacheLibrary;

namespace CacheLibraryWithoutTimers
{
    public class CacheWithoutTimers<T>
    {
        private readonly Dictionary<string, Element<T>> _cache;
        private int _capacity;
        private readonly int _limitTimeStoringInCacheInSeconds;
        private readonly Stopwatch _cacheStopwatch = new Stopwatch();
        private readonly IDataBase<T> _slowDataBase;

        public CacheWithoutTimers(IDataBase<T> slowDataBase)
        {
            _cache = new Dictionary<string, Element<T>>();
            _capacity = 10;
            _limitTimeStoringInCacheInSeconds = 5;
            _slowDataBase = slowDataBase;
            _cacheStopwatch.Start();
        }

        public CacheWithoutTimers(int capacity, IDataBase<T> slowDataBase)
        {
            _cache = new Dictionary<string, Element<T>>();
            _capacity = capacity;
            _limitTimeStoringInCacheInSeconds = 5;
            _slowDataBase = slowDataBase;
            _cacheStopwatch.Start();
        }

        public CacheWithoutTimers(int capacity, int limitTimeStoringInCacheInSeconds, IDataBase<T> slowDataBase)
        {
            _cache = new Dictionary<string, Element<T>>();
            _capacity = capacity;
            _limitTimeStoringInCacheInSeconds = limitTimeStoringInCacheInSeconds;
            _slowDataBase = slowDataBase;
            _cacheStopwatch.Start();
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

        public Element<T> GetItemByIdentifier(string identifier, ref string typeOfStorage)
        {
            if (!_cache.ContainsKey(identifier))
            {
                typeOfStorage = "Taken from Slow DataBase";
                var elementFromRemotingDataBase = GetItemFromRemotingDataBase(identifier);
                AddItem(elementFromRemotingDataBase);
                return elementFromRemotingDataBase;
            }

            typeOfStorage = "Taken from Cache";
            _cache[identifier].FrequencyUsage++;
            return _cache[identifier];
        }

        private Element<T> GetItemFromRemotingDataBase(string identifier)
        {
            return _slowDataBase.GetItemByIdentifier(identifier);
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
            return Capacity == CurrentSize;
        }

        private string FindKeyLeastUsedElement(string keyJustAddedElement)
        {
            var keyRemovedElement = IdentifierMostUsedElement;
            foreach (var item in _cache)
            {
                if (item.Key == keyJustAddedElement)
                    continue;

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

        private void RemoveOutdateElementsFromCache()
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
                   _limitTimeStoringInCacheInSeconds;
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
    }
}
