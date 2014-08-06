using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CacheLibrary
{
    public class Cache<T> : IDataBase<T>
    {
        private readonly Dictionary<string, Element<T>> _cache;
        private int _capacity;
        private int _limitTimeStoringInCacheInSeconds;
        private readonly Stopwatch _cacheStopwatch = new Stopwatch();
        private readonly IDataBase<T> _dataBase;

        public Cache(int capacity, int limitTimeStoringInCacheInSeconds, IDataBase<T> dataBase)
        {
            _cache = new Dictionary<string, Element<T>>();
            Capacity = capacity;
            LimitTimeStoringInCacheInSeconds = limitTimeStoringInCacheInSeconds;
            _dataBase = dataBase;
            _cacheStopwatch.Start();
        }

        public Cache(IDataBase<T> slowDataBase) 
            : this(10, 5, slowDataBase)
        { }

        public Cache(int capacity, IDataBase<T> slowDataBase)
            : this(capacity, 5, slowDataBase)
        { }

        public int Capacity
        {
            get { return _capacity; }
            set
            {
                if (value > 0 && value >= _capacity)
                    _capacity = value;
                else
                    throw new ArgumentOutOfRangeException();
            }
        }

        public int LimitTimeStoringInCacheInSeconds
        {
            get { return _limitTimeStoringInCacheInSeconds; }
            set
            {
                if (value >= 0)
                    _limitTimeStoringInCacheInSeconds = value;
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
                _cache[id].IncrementFrequencyUsage();
                _cache[id].TimeOfLastUsingInSeconds = _cacheStopwatch.Elapsed.Seconds;
                _cache[id].TypeOfStorage = TypesOfStorage.Cache;
                return _cache[id];
            }

            var itemFromDataBase = GetItemFromRemoteDataBase(id);
            var element = new Element<T>(id, itemFromDataBase)
            {
                TimeOfLastUsingInSeconds = _cacheStopwatch.Elapsed.Seconds,
                TypeOfStorage = TypesOfStorage.DataBase
            };
            AddItem(element);
            return element;
        }

        private T GetItemFromRemoteDataBase(string identifier)
        {
            return _dataBase.GetItemById(identifier);
        }

        private void AddItem(Element<T> element)
        {
            RemoveOutdateElementsFromCache();
            if (CacheIsFull())
            {
                var keyRemovedElement = FindKeyLeastUsedElement(element.Id);
                _cache.Remove(keyRemovedElement);
            }

            _cache.Add(element.Id, element);
        }

        private void RemoveOutdateElementsFromCache()
        {
            var keysRemovingElements = GetKeysOfRemovingElements();
            RemoveElementsByKeys(keysRemovingElements);
        }
        private List<string> GetKeysOfRemovingElements()
        {
            return _cache
                .Where(IsExceededLimitOfStoring)
                .Select(pair => pair.Key)
                .ToList();
        }
        private bool IsExceededLimitOfStoring(KeyValuePair<string, Element<T>> pair)
        {
            return pair.Value.TimeOfLastUsingInSeconds - _cacheStopwatch.Elapsed.Seconds >=
                   _limitTimeStoringInCacheInSeconds;
        }

        private void RemoveElementsByKeys(List<string> keysRemovingElements)
        {
            foreach (var keyRemovingElement in keysRemovingElements)
            {
                _cache[keyRemovingElement].TypeOfStorage = TypesOfStorage.DataBase;
                _cache.Remove(keyRemovingElement);
            }
        }

        private bool CacheIsFull()
        {
            return Capacity == Count;
        }

        private string FindKeyLeastUsedElement(string keyJustAddedElement)
        {
            var minFrequencyUsage = int.MaxValue;
            var keyRemovedElement = _cache.First().Key;
            foreach (var item in _cache)
            {
                if (item.Key == keyJustAddedElement) continue;
                if (item.Value.FrequencyUsage > minFrequencyUsage) continue;

                if (item.Value.FrequencyUsage == minFrequencyUsage)
                {
                    if (item.Value.TimeOfLastUsingInSeconds > _cache[keyRemovedElement].TimeOfLastUsingInSeconds)
                    {
                        keyRemovedElement = item.Key;
                        minFrequencyUsage = item.Value.FrequencyUsage;
                    }
                }
            }
            return keyRemovedElement;
        }

        public string GetAllElementsOfCacheInStringFormat()
        {
            var allElementsInStringFormat = "";
            var terminalSign = "|";
            foreach (var cacheElement in _cache)
            {
                Element<T> element = cacheElement.Value;
                allElementsInStringFormat += terminalSign + element.Value;
            }

            return allElementsInStringFormat;
        }
    }
}
