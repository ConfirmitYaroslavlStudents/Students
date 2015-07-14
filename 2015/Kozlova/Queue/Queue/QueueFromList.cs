using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Queue
{
    public class QueueFromList<T>: System.Object, IEnumerable<T>
    {
        private QueueFromListItem<T> _head;
        private QueueFromListItem<T> _tail;
        private int _size;

        // Property, returns size of a queue
        public int Count
        {
            get { return _size; }
        }

        // Constructor, items to be added in a queue can be written in arguments.
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

        // Add an item into a queue
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

        // Returns an item from a queue with removing it.
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

        // Returns an item from a queue without removing it.
        public T Peek()
        {
            return _head.Data;
        }

        // Clears all the queue
        public void Clear()
        {
            _size = 0;
            _tail = _head = null;
        }

        //Print the queue
        //[TODO] fix
        public void Print()
        {
            foreach (T item in this)
                Console.WriteLine(item.ToString());
        }
        
        // IEnumerable<T> realization.
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

        // Methods for comparing queues while testing.
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

        // Conversion to string
        public override string ToString()
        {
            StringBuilder queueToString = new StringBuilder();
            foreach (T item in this)
            {
                queueToString.Append(item + " ");
            }
            return queueToString.ToString();
        }
    }

    internal class QueueFromListItem<T>: System.Object
    {
        public T Data { get; private set; }
        public QueueFromListItem<T> NextItem { get; private set; }

        public QueueFromListItem(T value)
        {
            Data = value;
            NextItem = null;
        }

        // Creates and returns next item
        public QueueFromListItem<T> SetNext(T value)
        {
            NextItem = new QueueFromListItem<T>(value);
            return NextItem;
        }

        // Methods for comparing queues while testing.
        public override bool Equals(object obj)
        {
            if (this == null)
                throw new NullReferenceException();  
            if (obj == null)
            {
                return false;
            }

            QueueFromListItem<T> p = obj as QueueFromListItem<T>;
            if ((System.Object)p == null)
            {
                return false;
            }

            return this.Data.Equals(p.Data);
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

        // Conversion to string
        public override string ToString()
        {
            return Data.ToString();
        }

    }
}
