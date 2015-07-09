using System;
using System.Collections;
using System.Collections.Generic;
using StackInterface;

namespace StackArrayLibrary
{
    public class Stack<T> : IStack<T>
    {
        private const int DefaultSize = 8;
        private T[] _elements;

        public int Count { get; private set; }

        public T this[int i]
        {
            get
            {
                // change 'magic values' on 'indexFromPeekOfStack' variables
                return _elements[Count - i - 1];
            }

        }

        //Replace Constructors to 'Factory' pattern or use 'this(int capacity)' Constructor
        public Stack()
        {
            Count = 0;
            _elements = new T[DefaultSize];
        }

        public Stack(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            Count = 0;
            _elements = new T[DefaultSize];

            foreach (var element in collection)
            {
                Push(element);
            }
        }

        public Stack(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity");
            }
            Count = 0;
            _elements = capacity > DefaultSize ? new T[capacity] : new T[DefaultSize];
        }
        
        public void Push(T element)
        {
            if (Count == _elements.Length)
            {
                var newArray = new T[2 * _elements.Length];
                Array.Copy(_elements, 0, newArray, 0, Count);
                _elements = newArray;
            }
            _elements[Count++] = element;
        }

        public T Pop()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
            return _elements[--Count];
        }

        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
            return _elements[Count - 1];
        }

        public void Clear()
        {
            if (Count > 0)
            {
                Array.Clear(_elements, 0, Count);
                Count = 0;
            }
        }

        public bool Contains(T element)
        {
            for (int i = 0; i < Count; ++i)
            {
                if (Equals(_elements[i], element))
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; ++i)
            {
                yield return _elements[Count - i - 1];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

