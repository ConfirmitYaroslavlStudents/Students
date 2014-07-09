using System;
using System.Collections;
using System.Collections.Generic;

namespace Set
{
    public class Set<T> : IEnumerable<T>
    {
        private const int MaxArrayLength = 0X7FEFFFFF;
        private const int DefaultCapacity = 4;
        private static readonly T[] EmptyArray = new T[0];

        #region Fields

        private T[] _items;
        private int _size; 

        #endregion

        #region Properties
        public int Capacity
        {
            get { return _items.Length; }
            set
            {
                if (value < _size)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                if (value != _items.Length)
                {
                    if (value > 0)
                    {
                        var newItems = new T[value];

                        if (_size > 0)
                        {
                            Array.Copy(_items, 0, newItems, 0, _size);
                        }
                        _items = newItems;
                    }
                    else
                    {
                        _items = EmptyArray;
                    }
                }
            }
        }

        public int Count
        {
            get { return _size; }
        } 

        #endregion

        #region Methods
        public Set()
        {
            _items = EmptyArray;
        }

        public Set(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity");
            }

            _items = capacity == 0 ? EmptyArray : new T[capacity];
        }

        public Set(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }

            _size = 0;
            _items = EmptyArray;

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
            return true;
        }

        private void EnsureCapacity(int minCapacity)
        {
            if (_items.Length < minCapacity)
            {
                int newCapacity = _items.Length == 0 ? DefaultCapacity : _items.Length * 2;

                if ((uint)newCapacity > MaxArrayLength)
                {
                    newCapacity = MaxArrayLength;
                }

                if (newCapacity < minCapacity)
                {
                    newCapacity = minCapacity;
                }

                Capacity = newCapacity;
            }
        }

        public void Clear()
        {
            if (_size > 0)
            {
                Array.Clear(_items, 0, _size);
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

            EqualityComparer<T> c = EqualityComparer<T>.Default;
            for (int i = 0; i < _size; ++i)
            {
                if (c.Equals(_items[i], item))
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
            }
            _items[_size] = default(T);
            return true;
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

        #endregion
    }
}
