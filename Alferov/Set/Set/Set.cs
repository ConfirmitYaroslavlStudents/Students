using System;
using System.Collections;
using System.Collections.Generic;
using Set.Utils;

namespace Set
{
    public class Set<T> : IEnumerable<T> where T : IComparable<T>
    {
        private const int MaxArrayLength = 2146435071;
        private const int DefaultCapacity = 16;

        private T[] _items;
        private int _size;
        private readonly StatisticsCollector _statisticsCollector;

        public int Capacity
        {
            get { return _items.Length; }
            private set
            {
                var newItems = new T[value];

                if (_size > 0)
                {
                    Array.Copy(_items, 0, newItems, 0, _size);

                    if (_statisticsCollector != null)
                    {
                        _statisticsCollector.ChangeStatistics(_size);
                    }
                }
                _items = newItems;
            }
        }

        public int Count
        {
            get { return _size; }
        }

        public Set(StatisticsCollector statisticsCollector = null)
        {
            _statisticsCollector = statisticsCollector;
            _items = new T[DefaultCapacity];
        }

        public Set(int capacity, StatisticsCollector statisticsCollector = null)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity");
            }

            _statisticsCollector = statisticsCollector;
            _items = capacity > DefaultCapacity ? new T[capacity] : new T[DefaultCapacity];
        }

        public Set(IEnumerable<T> collection, StatisticsCollector statisticsCollector = null)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            _items = new T[DefaultCapacity];
            _statisticsCollector = statisticsCollector;

            foreach (var item in collection)
            {
                Add(item);
            }
        }

        public bool Add(T item)
        {
            if (Contains(item))
            {
                return false;
            }

            if (_size == _items.Length)
            {
                EnsureCapacity(_size + 1);
            }

            _items[_size++] = item;

            if (_statisticsCollector != null)
            {
                _statisticsCollector.ChangeStatistics(1);
            }

            return true;
        }

        public static Set<T> operator +(Set<T> set, T item)
        {
            var resultSet = new Set<T>(set, set._statisticsCollector);
            resultSet.Add(item);
            return resultSet;
        }

        private void EnsureCapacity(int minCapacity)
        {
            if (minCapacity > MaxArrayLength)
            {
                throw new InvalidOperationException();
            }

            int newCapacity = _items.Length * 2;

            // Allow the set to grow to maximum possible capacity (~2G elements(MaxArrayLength)) before encountering overflow.
            // Note that this check works even when newCapacity overflowed thanks to the (uint) cast
            if ((uint)newCapacity > MaxArrayLength)
            {
                newCapacity = MaxArrayLength;
            }

            Capacity = newCapacity;
        }

        public void Clear()
        {
            if (_size > 0)
            {
                Array.Clear(_items, 0, _size);
                
                if (_statisticsCollector != null)
                {
                    _statisticsCollector.ChangeStatistics(_size);
                }

                _size = 0;
            }
        }

        public bool Contains(T item)
        {
            if (item == null)
            {
                for (int i = 0; i < _size; ++i)
                {
                    if (_items[i] == null)
                    {
                        return true;
                    }
                }
                return false;
            }

            for (int i = 0; i < _size; ++i)
            {
                if (item.CompareTo(_items[i]) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Remove(T item)
        {
            int index = Array.IndexOf(_items, item, 0, _size);

            if (index < 0)
            {
                return false;
            }

            --_size;
            if (index < _size)
            {
                Array.Copy(_items, index + 1, _items, index, _size - index);

                if (_statisticsCollector != null)
                {
                    _statisticsCollector.ChangeStatistics(_size - index);
                }
            }
            _items[_size] = default(T);

            if (_statisticsCollector != null)
            {
                _statisticsCollector.ChangeStatistics(1);
            }

            return true;
        }

        public static Set<T> operator -(Set<T> set, T item)
        {
            var resultSet = new Set<T>(set, set._statisticsCollector);
            resultSet.Remove(item);
            return resultSet;
        }

        public void UnionWith(Set<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            foreach (T item in other)
            {
                Add(item);
            }
        }

        public static Set<T> operator +(Set<T> firstSet, Set<T> secondSet)
        {
            var resultSet = new Set<T>(firstSet, firstSet._statisticsCollector);
            resultSet.UnionWith(secondSet);
            return resultSet;
        }

        public void IntersectWith(Set<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            if (_size == 0)
            {
                return;
            }

            if (other.Count == 0)
            {
                Clear();
                return;
            }

            int i = 0;
            while (i < _size)
            {
                if (!other.Contains(_items[i]))
                {
                    Remove(_items[i]);
                    continue;
                }
                ++i;
            }
        }

        public void ExceptWith(Set<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            if (_size == 0)
            {
                return;
            }

            if (other == this)
            {
                Clear();
                return;
            }

            foreach (T element in other)
            {
                Remove(element);
            }
        }

        public static Set<T> operator -(Set<T> firstSet, Set<T> secondSet)
        {
            var resultSet = new Set<T>(firstSet, firstSet._statisticsCollector);
            resultSet.ExceptWith(secondSet);
            return resultSet;
        }

        public bool IsSubsetOf(Set<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            if (_size == 0)
            {
                return true;
            }

            if (_size > other.Count)
            {
                return false;
            }

            for (int i = 0; i < _size; ++i)
            {
                if (!other.Contains(_items[i]))
                {
                    return false;
                }
            }
            return true;

        }

        public void SymmetricExceptWith(Set<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            if (_size == 0)
            {
                UnionWith(other);
                return;
            }

            if (other == this)
            {
                Clear();
                return;
            }

            foreach (T item in other)
            {
                if (!Remove(item))
                {
                    Add(item);
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _size; ++i)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
