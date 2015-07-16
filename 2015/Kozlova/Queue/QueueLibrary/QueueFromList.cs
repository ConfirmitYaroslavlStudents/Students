using System;
using System.Collections.Generic;
using System.Text;

namespace QueueLibrary
{
    /// <summary>
    /// Implementation of a queue with list structure.
    /// </summary>
    /// <typeparam name="T">Type of the items in a queue.</typeparam>
    public class QueueFromList<T> : IQueue<T>
    {
        private QueueFromListItem<T> _head;
        private QueueFromListItem<T> _tail;
        private int _size;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="values">Items to be enqueued.</param>
        public QueueFromList(params T[] values)
        {
            _size = 0;
            _tail = _head = null;
            if (values.Length > 0)
            {
                _head = new QueueFromListItem<T>(values[0]);
                _tail = _head;
                _size++;
                for (int i = 1; i < values.Length; i++)
                    Enqueue(values[i]);
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
            _size++;
            if (_head == null)
            {
                _head = new QueueFromListItem<T>(item);
                _tail = _head;
            }
            else
            {
                _tail = _tail.SetNext(item);
            }
        }

        /// <summary>
        /// Returns an item with removing it.
        /// </summary>
        /// <returns>Item from the head of a queue</returns>
        public T Dequeue()
        {
            if (_size == 0)
                throw new Exception("Queue is empty!");
            T temp = _head.Data;
            if (_size == 1)
            {
                _tail = _head = null;
            }
            else
            {
                _head = _head.NextItem;    
            }
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
            return _head.Data;
        }

        /// <summary>
        /// Delete all the items in a queue.
        /// </summary>
        public void Clear()
        {
            _size = 0;
            _tail = _head = null;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            QueueFromListItem<T> pointer = _head;
            while (pointer != null)
            {
                yield return pointer.Data;
                pointer = pointer.NextItem;
            }
            
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
        public override bool Equals(object obj)
        {
            if (this == null)
                throw new NullReferenceException();
            if (obj == null)
            {
                return false;
            }

            QueueFromList<T> p = obj as QueueFromList<T>;
            if ((System.Object)p == null)
            {
                return false;
            }

            return this == p;
        }
        public override int GetHashCode()
        {
            return Count;
        }
        public static bool operator ==(QueueFromList<T> a, QueueFromList<T> b)
        {
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            if (a.Count != b.Count)
                return false;
            QueueFromListItem<T> pointerA = a._head, pointerB = b._head;
            while (pointerA != null)
            {
                if (pointerA != pointerB)
                {
                    return false;
                }
                pointerA = pointerA.NextItem;
                pointerB = pointerB.NextItem;
            }
            return true;
        }
        public static bool operator !=(QueueFromList<T> a, QueueFromList<T> b)
        {
            return !(a == b);
        }
    }

    internal class QueueFromListItem<T> 
    {
        public T Data { get; private set; }
        public QueueFromListItem<T> NextItem { get; private set; }
        public QueueFromListItem(T value)
        {
            Data = value;
            NextItem = null;
        }

        public QueueFromListItem<T> SetNext(T value)
        {
            NextItem = new QueueFromListItem<T>(value);
            return NextItem;
        }

        public override bool Equals(object obj)
        {
            if (this == null)
                throw new NullReferenceException();  
            if (obj == null)
            {
                return false;
            }

            QueueFromListItem<T> p = obj as QueueFromListItem<T>;
            if ((object)p == null)
            {
                return false;
            }

            return this.Data.Equals(p.Data);
        }
        public override int GetHashCode()
        {
            return Data.ToString().GetHashCode();
        }
        public static bool operator ==(QueueFromListItem<T> a, QueueFromListItem<T> b)
        {
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }
            
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.Data.Equals(b.Data);
        }
        public static bool operator !=(QueueFromListItem<T> a, QueueFromListItem<T> b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}
