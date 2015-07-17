using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stack
{
    public class Stack<T> : IEnumerable<T>, ICollection, IStack<T>
    {
        private const int InitialCapacity = 4;
        private T[] _items;
        private int _size;

        public int Count
        {
            get { return _size; }
        }

        public Stack()
        {
            _items = new T[0];
            _size = 0;
        }

        public Stack(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException();
            _items = new T[capacity];
            _size = 0;
        }

        public Stack(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();

            ICollection<T> c = collection as ICollection<T>;

            if (c == null)
            {
                _items = new T[InitialCapacity];
                _size = 0;

                foreach (var e in collection)
                    Push(e);
            }
            else
            {
                _items = new T[c.Count];
                _size = c.Count;
                c.CopyTo(_items, 0);
            }
        }

        public void Push(T item)
        {
            if (_size == _items.Length)
                Array.Resize(ref _items, _items.Length == 0 ? InitialCapacity : _items.Length * 2);
            _items[_size++] = item;
        }

        public T Pop()
        {
            if (_size == 0)
                throw new InvalidOperationException();
            T popped = _items[--_size];
            _items[_size] = default(T);
            return popped;
        }

        public T Peek()
        {
            if (_size == 0)
                throw new InvalidOperationException();
            return _items[_size-1];
        }

        public void Clear()
        {
            Array.Clear(_items, 0, _size);
            _size = 0;
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public void TrimeExcess()
        {
            int newSize = (int)(_items.Length*0.9);
            if (_size < newSize)
                Array.Resize(ref _items, newSize);
        }

        public object SyncRoot { get { return this; } }

        public bool IsSynchronized { get { return false; } }

        public void CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException();
            if (index < 0)
                throw new ArgumentOutOfRangeException();

            try 
            {
                _items.CopyTo(array, index);
                Array.Reverse(array, index, _size);
            }
            catch (ArrayTypeMismatchException)
            {
                throw new ArgumentException();
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public class Enumerator : IEnumerator<T>
        {
            private Stack<T> _stack;
            private int _index;

            public Enumerator(Stack<T> stack)
            {
                _stack = stack;
                _index = -2;
            }

            public bool MoveNext()
            {
                if (_index == -2)
                    _index = _stack._size;

                return _index != -1 && --_index != -1;
            }

            public T Current
            {
                get
                {
                    if (_index < 0)
                        throw new InvalidOperationException();
                    return _stack._items[_index];
                }
            }

            void IEnumerator.Reset()
            {
                _index = -2;
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Dispose()
            {
                _index = -1;
            }
        }
    }

}