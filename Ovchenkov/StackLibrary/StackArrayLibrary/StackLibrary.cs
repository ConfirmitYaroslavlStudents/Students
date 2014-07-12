using System;
using System.Collections;
using System.Collections.Generic;

namespace StackArrayLibrary
{
    public class Stack<T> : IEnumerable<T>
    {
        private const int DefaultSize = 8;

        #region Fields
        
        private int _count;
        private T[] _elements;
        
        #endregion

        #region Properties

        public int Count
        {
            get { return _count; }
        }

        public T this[int i]
        {
            get
            {
                return _elements[i];
            }
        }

        #endregion

        #region Constructors

        public Stack()
        {
            _count = 0;
            _elements = new T[DefaultSize];
        }

        public Stack(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            _count = 0;
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
            _count = 0;
            _elements = capacity > DefaultSize ? new T[capacity] : new T[DefaultSize];
        }

        #endregion

        #region Methods

        public void Push(T element)
        {
            if (_count == _elements.Length)
            {
                var newArray = new T[2 * _elements.Length];
                Array.Copy(_elements, 0, newArray, 0, _count);
                _elements = newArray;
            }
            _elements[_count++] = element;
        }

        public T Pop()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException();
            }
            return _elements[--_count];
        }

        public T Peek()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException();
            }
            return _elements[_count - 1];
        }

        public void Clear()
        {
            if (_count > 0)
            {
                Array.Clear(_elements, 0, _count);
                _count = 0;
            }
        }

        public bool Contains(T element)
        {
            for (int i = 0; i < _count; ++i)
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
            return ((IEnumerable<T>)_elements).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}

