using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Queue
{
    public class Queue<T> : System.Object, IEnumerable<T>
    {
        private const int DefaultSize = 5;
        private T[] _items;
        private int _tail;

        // Constructor, items to be added in a queue can be written in arguments.
        public Queue(params T[] values)
        {
            _items = new T[DefaultSize];
            _tail = -1;
            foreach (T value in values)
            {
                Enqueue(value);
            }
        }
        
        // Property, returns size of a queue
        public int Count
        {
            get { return _tail + 1; }
        }

        // Property, returns an array of items, only for tests.
        public T[] Items
        {
            get { return _items; }
        }

        // Add an item into a queue.
        public void Enqueue(T item)
        {
            if (_tail + 1 == _items.Length)
                Array.Resize(ref _items, _items.Length*2);
            _items[++_tail] = item;
        }

        // Returns an item from a queue with removing it.
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

        // Returns an item from a queue without removing it.
        public T Peek()
        {
            return _items[0];
        }
        
        // Delete all the items in a queue.
        public void Clear()
        {
            for (int i = 0; i < _tail + 1; i++)
            {
                _items[i] = default(T);
            }
            _tail = -1;   
        }

        // Print a queue, each element on a new line.
        public void Print()
        {
            foreach(T item in this)
                Console.WriteLine(item.ToString());
        }

        // IEnumerable<T> realization.
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

        // Methods for comparing queues while testing.
        private T GetElement(int index)
        {
            if (index > _tail)
                throw new ArgumentOutOfRangeException();
            return _items[index];
        }
        public static bool operator ==(Queue<T> a, Queue<T> b)
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
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Queue<T> p = obj as Queue<T>;
            if ((System.Object)p == null)
            {
                return false;
            }

            return this == p;
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
}
