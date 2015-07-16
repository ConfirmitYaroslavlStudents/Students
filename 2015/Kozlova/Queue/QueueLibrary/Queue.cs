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
        private const int DefaultSize = 5;
        private T[] _items;
        private int _tail;
        
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="values">Items to be enqueued.</param>
        public Queue(params T[] values)
        {
            _items = new T[DefaultSize];
            Clear();
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
            get { return _tail + 1; }
        }

        /// <summary>
        /// Adds item into a queue
        /// </summary>
        /// <param name="item">Item added to queue.</param>
        public void Enqueue(T item)
        {
            if (_tail + 1 == _items.Length)
                Array.Resize(ref _items, _items.Length*2);
            _items[++_tail] = item;
        }

        //TODO fix, cyclic array
        /// <summary>
        /// Returns an item with removing it.
        /// </summary>
        /// <returns>Item from the head of a queue</returns>
        public T Dequeue()
        {
            if (_tail == -1)
                throw new Exception("Queue is empty!");
            T temp = _items[0];
            for (int i = 0; i < _tail; i++)
                _items[i] = _items[i + 1];
            _items[_tail--] = default(T);
            return temp;
        }

        /// <summary>
        /// Returns an item without removing it.
        /// </summary>
        /// <returns>Item from the head of a queue</returns>
        public T Peek()
        {
            return _items[0];
        }
        
        /// <summary>
        /// Delete all the items in a queue.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < _tail + 1; i++)
            {
                _items[i] = default(T);
            }
            _tail = -1;   
        }

        //TODO implement without yield
        public IEnumerator<T> GetEnumerator()
        {
            
            for (int i = 0; i < _tail + 1; i++)
            {
                yield return _items[i];
            }
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Queue<T> p = obj as Queue<T>;
            if ((object)p == null)
            {
                return false;
            }

            return this == p;
        }
        public override int GetHashCode()
        {
            return Count;
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
            if (index > _tail)
                throw new ArgumentOutOfRangeException();
            return _items[index];
        }
        public static bool operator ==(Queue<T> a, Queue<T> b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
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
        
    }
}
