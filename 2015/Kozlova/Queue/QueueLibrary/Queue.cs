using System;
using System.Collections.Generic;
using System.Text;

namespace QueueLibrary
{
    /// <summary>
    /// Implementation of a queue with array structure.
    /// </summary>
    /// <typeparam name="T">Type of the items in a queue.</typeparam>
    public class Queue<T> : IQueue<T>
    {
        private const int DefaultSize = 4;
        private T[] _items;
        private int _tail;
        private int _head;
        private int _size;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="values">Items to be enqueued.</param>
        public Queue(params T[] values)
        {
            _items = new T[DefaultSize];
            SetToZero();
            foreach (T value in values)
            {
                Enqueue(value);
            }
        }

        /// <summary>
        /// Returns amount of items in the queue.
        /// </summary>
        public int Count
        {
            get { return _size; }
        }

        /// <summary>
        /// Adds item into a queue
        /// </summary>
        /// <param name="item">Item added to queue.</param>
        public void Enqueue(T item)
        {
            if (_size == _items.Length)
            {
                SetCapacity(_items.Length*2);
            }
            _items[_tail] = item;
            _tail = (_tail + 1)%_items.Length;
            _size++;
        }

        private void SetCapacity(int newCapacity)
        {
            T[] newItems = new T[newCapacity];
            Array.Copy(_items, _head, newItems, 0, _items.Length - _head);
            Array.Copy(_items, 0, newItems, _items.Length - _head, _tail);
            _items = newItems;
            _head = 0;
            _tail = _size;
        }

        /// <summary>
        /// Returns an item with removing it.
        /// </summary>
        /// <returns>Item from the head of a queue</returns>
        public T Dequeue()
        {
            if (_size == 0)
                throw new Exception("Queue is empty!");
            T temp = _items[_head];
            _items[_head] = default(T);
            _head = (_head + 1)%_items.Length;
            _size--;
            return temp;
        }

        /// <summary>
        /// Returns an item without removing it.
        /// </summary>
        /// <returns>Item from the head of a queue</returns>
        public T Peek()
        {
            if (_size == 0)
                throw new Exception("Queue is empty!");
            return _items[_head];
        }

        /// <summary>
        /// Delete all the items in a queue.
        /// </summary>
        public void Clear()
        {
            if (_head < _tail)
                Array.Clear(_items, _head, _size);
            else
            {
                Array.Clear(_items, _head, _items.Length - _head);
                Array.Clear(_items, 0, _tail);
            }
            SetToZero();
        }

        private void SetToZero()
        {
            _tail = _head = 0;
            _size = 0;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Queue<T> p = obj as Queue<T>;
            if ((object) p == null)
            {
                return false;
            }

            return this == p;
        }

        public override int GetHashCode()
        {
            int hashCode = 0;
            int index = 0;
            foreach (var element in this)
            {
                hashCode += element.ToString().GetHashCode() * (++index);
            }
            return hashCode;
        }

        public override string ToString()
        {
            StringBuilder queueToString = new StringBuilder();
            foreach (T item in this)
            {
                queueToString.Append(item + " ");
            }
            return queueToString.ToString();
        }

        private T GetElement(int index)
        {
            if (index > _size)
                throw new ArgumentOutOfRangeException();
            return _items[(_head + index)%_items.Length];
        }

        public static bool operator ==(Queue<T> a, Queue<T> b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object) a == null) || ((object) b == null))
            {
                return false;
            }

            if (a.Count != b.Count)
                return false;
            for (int i = 0; i < a.Count; i++)
            {
                if (!a.GetElement(i).Equals(b.GetElement(i)))
                    return false;
            }
            return true;
        }

        public static bool operator !=(Queue<T> a, Queue<T> b)
        {
            return !(a == b);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        private class Enumerator : IEnumerator<T>
        {
            private Queue<T> _q;
            private int _index; // -1 = not started, -2 = ended/disposed
            private T _currentElement;

            internal Enumerator(Queue<T> q)
            {
                _q = q;
                _index = -1;
                _currentElement = default(T);
            }

            public void Dispose()
            {
                _index = -2;
                _currentElement = default(T);
            }

            public bool MoveNext()
            {
                if (_index == -2)
                    return false;

                _index++;

                if (_index == _q._size)
                {
                    _index = -2;
                    _currentElement = default(T);
                    return false;
                }

                _currentElement = _q.GetElement(_index);
                return true;
            }

            public T Current
            {
                get
                {
                    if (_index < 0)
                    {
                        throw new InvalidOperationException();
                    }
                    return _currentElement;
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    if (_index < 0)
                    {
                        throw new InvalidOperationException();
                    }
                    return _currentElement;
                }
            }

            void System.Collections.IEnumerator.Reset()
            {
                _index = -1;
                _currentElement = default(T);
            }
        }
    }
}
