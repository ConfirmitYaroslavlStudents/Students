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
        private long _timeOfExpirationInTicks;
        private readonly Stopwatch _cacheStopwatch = new Stopwatch();
        private readonly IDataBase<T> _dataBase;

        public Cache(int capacity, int timeOfExpirationInTicks, IDataBase<T> dataBase)
        {
            _cache = new Dictionary<string, Element<T>>();
            Capacity = capacity;
            TimeOfExpirationInTicks = timeOfExpirationInTicks;
            _dataBase = dataBase;
            _cacheStopwatch.Start();
        }

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

        public long TimeOfExpirationInTicks
        {
            get { return _timeOfExpirationInTicks; }
            set
            {
                if (value >= 0)
                    _timeOfExpirationInTicks = value;
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
                _cache[id].TimeOfLastUsingInTicks = _cacheStopwatch.Elapsed.Ticks;
                _cache[id].TypeOfStorage = TypesOfStorage.Cache;
                return _cache[id];
            }

            var itemFromDataBase = GetItemFromRemoteDataBase(id);
            var element = new Element<T>(id, itemFromDataBase)
            {
                TimeOfLastUsingInTicks = _cacheStopwatch.Elapsed.Ticks,
                TypeOfStorage = TypesOfStorage.DataBase
            };
            AddElementInCache(element);
            return element;
        }

        private T GetItemFromRemoteDataBase(string identifier)
        {
            return _dataBase.GetItemById(identifier);
        }

        private void AddElementInCache(Element<T> element)
        {
            RemoveOutdateElementsFromCache();
            if (CacheIsFull())
            {
                var keyRemovedElement = FindKeyLeastUsedElement();
                _cache.Remove(keyRemovedElement);
            }

            _cache.Add(element.Id, element);
        }

        private void RemoveOutdateElementsFromCache()
        {
            var keysRemovingElements = GetKeysOfRemovingElements();
            RemoveElementsByKeys(keysRemovingElements);
        }

        private IEnumerable<string> GetKeysOfRemovingElements()
        {
            return _cache
                .Where(IsExceededLimitOfStoring)
                .Select(pair => pair.Key)
                .ToList();
        }

        private bool IsExceededLimitOfStoring(KeyValuePair<string, Element<T>> pair)
        {
            return pair.Value.TimeOfLastUsingInTicks - _cacheStopwatch.Elapsed.Ticks >=
                   _timeOfExpirationInTicks;
        }

        private void RemoveElementsByKeys(IEnumerable<string> keysRemovingElements)
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

        private string FindKeyLeastUsedElement()
        {
            var minFrequencyUsage = int.MaxValue;
            var keyRemovedElement = _cache.First().Key;

            foreach (var item in _cache)
            {
                if (item.Value.TimeOfLastUsingInTicks < _cache[keyRemovedElement].TimeOfLastUsingInTicks)
                {
                    keyRemovedElement = item.Key;
                }
            }

            return keyRemovedElement;
        }

        public List<Element<T>> GetListOfElementsLocatedInCache()
        {
            var list = new List<Element<T>>();
            foreach (var element in _cache.Values)
            {
                if(element.TypeOfStorage == TypesOfStorage.Cache)
                    list.Add(element);
            }
            return list;
        }

        public string GetAllElementsOfCacheInStringShortFormat()
        {
            var allElementsInStringFormat = "";
            const string terminalSign = "|";
            foreach (var cacheElement in _cache)
            {
                Element<T> element = cacheElement.Value;
                allElementsInStringFormat += terminalSign + element.Value;
            }

            return allElementsInStringFormat;
        }

        public string GetAllElementsOfCacheInStringFullFormat()
        {
            var allElementsInStringFormat = "";
            const string terminalSign = "|";
            foreach (var cacheElement in _cache)
            {
                Element<T> element = cacheElement.Value;
                allElementsInStringFormat += terminalSign + element;
            }

            return allElementsInStringFormat;
        }
    }
}
