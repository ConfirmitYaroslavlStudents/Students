using System;

namespace StackArrayLibrary
{
    public class Stack<T>
    {
        private const int DefaultSize = 10;

        private int _size;
        private T[] _elems;

        public int Count
        {
            get { return _size; }
        }

        public Stack()
        {
            _size = 0;
            _elems = new T[DefaultSize];
        }

        public void Push(T newElement)
        {
            if (_size == _elems.Length)
            {
                var newArray = new T[2*_elems.Length];
                Array.Copy(_elems, 0, newArray, 0, _size);
                _elems = newArray;
            }
            _elems[_size++] = newElement;
        }

        public T Pop()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException();
            }
            return _elems[--_size];
        }

        public T Peek()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException();
            }
            return _elems[_size - 1];
        }
    }
}

