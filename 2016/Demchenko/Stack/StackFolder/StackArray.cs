using System;
using System.Collections;
using System.Collections.Generic;

namespace Stack_1
{
    public class StackArray<T> : IStack<T>
    {
        private const int DefaultCapacity = 8;

        private T[] _items;

        public int Count { get; private set; }

        public StackArray()
        {
            Count = 0;
            _items = new T[DefaultCapacity];
        }

        public StackArray(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            Count = 0;
            _items = new T[DefaultCapacity];

            foreach (var item in collection)
            {
                Push(item);
            }
        }

        public StackArray(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity");
            }
            Count = 0;
            _items = capacity > DefaultCapacity ? new T[capacity] : new T[DefaultCapacity];
        }

        public void Push(T item)
        {
            if (Count == _items.Length)
            {
                var newArray = new T[2 * _items.Length];
                Array.Copy(_items, 0, newArray, 0, Count);
                _items = newArray;
            }
            _items[Count++] = item;
        }

        public T Pop()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
            return _items[--Count];
        }

        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
            return _items[Count - 1];
        }

        public void Clear()
        {
            if (Count > 0)
            {
                Array.Clear(_items, 0, Count);
                Count = 0;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; ++i)
            {
                yield return _items[Count - i - 1];
            }
        }
    }
}