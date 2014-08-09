using System;
using System.Collections;
using System.Collections.Generic;
using Set.Utils;

namespace Set
{
    public class Set<T> : IEnumerable<T> where T : IEquatable<T>
    {
        private const int MaxArrayLength = 2146435071;
        private const int DefaultCapacity = 16;

        private ArrayHelper<T> _arrayHelper;
        private T[] _items;
        private int _size;

        public int Capacity
        {
            get { return _items.Length; }
            private set
            {
                var newItems = new T[value];

                if (_size > 0)
                {
                    _arrayHelper.Copy(_items, 0, newItems, 0, _size);
                }

                _items = newItems;
            }
        }

        public int Count
        {
            get { return _size; }
        }

        public Set(ArrayHelper<T> arrayHelper)
        {
            InitializeArrayHelper(arrayHelper);
            _items = new T[DefaultCapacity];
        }

        public Set(int capacity, ArrayHelper<T> arrayHelper)
        {
            InitializeArrayHelper(arrayHelper);

            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity");
            }

            _items = capacity > DefaultCapacity ? new T[capacity] : new T[DefaultCapacity];
        }

        public Set(IEnumerable<T> collection, ArrayHelper<T> arrayHelper)
            : this(arrayHelper)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            foreach (T item in collection)
            {
                Add(item);
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

        public void Add(T item)
        {
            if (Contains(item))
            {
                return;
            }

            if (_size == _items.Length)
            {
                EnsureCapacity(_size + 1);
            }

            _arrayHelper.ChangeItem(_items, _size++, item);
        }

        public static Set<T> operator +(Set<T> set, T item)
        {
            var resultSet = new Set<T>(set, set._arrayHelper);
            resultSet.Add(item);

            return resultSet;
        }

        public void Clear()
        {
            if (_size > 0)
            {
                _arrayHelper.Clear(_items, 0, _size);
                _size = 0;
            }
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < _size; ++i)
            {
                if (item.Equals(_items[i]))
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
                _arrayHelper.Copy(_items, index + 1, _items, index, _size - index);
            }

            _arrayHelper.ChangeItem(_items, _size, default(T));

            return true;
        }

        public static Set<T> operator -(Set<T> set, T item)
        {
            var resultSet = new Set<T>(set, set._arrayHelper);
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
            var resultSet = new Set<T>(firstSet, firstSet._arrayHelper);
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

            for (int i = 0; i < _size; ++i)
            {
                if (!other.Contains(_items[i]))
                {
                    Remove(_items[i]);
                    continue;
                }
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
            var resultSet = new Set<T>(firstSet, firstSet._arrayHelper);
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

        private void InitializeArrayHelper(ArrayHelper<T> arrayHelper)
        {
            if (arrayHelper == null)
            {
                throw new ArgumentNullException("arrayHelper");
            }

            _arrayHelper = arrayHelper;
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
    }
}