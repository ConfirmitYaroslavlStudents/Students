using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue
{
    public class MyQueueFromArray<T> : IEnumerable<T>
    {
        public int Count { private set; get; }
        T[] _items;

        //Constructor
        public MyQueueFromArray()
        {
            Count = 0;
            _items = new T[0];
        }

        //Adds an object to the end of the MyQueueFromArray<T>.
        public void Enqueue(T value)
        {
            Count++;
            T[] acting = new T[Count];
            for (int i = 0; i < _items.Length; i++)
            {
                acting[i] = _items[i];
            }
            acting[Count - 1] = value;
            _items = acting;
        }

        //Removes and returns the object at the beginning of the MyQueueFromArray<T>.
        public T Dequeue()
        {
            if (Count != 0)
            {
                Count--;
                T returnedValue = _items[0];
                T[] acting = new T[Count];
                for (int i = 1; i < _items.Length; i++)
                {
                    acting[i - 1] = _items[i];
                }
                _items = acting;
                return returnedValue;
            }
            else
            {
                throw new Exception("The queue is empty.");
            }
        }

        //Removes all objects from the MyQueueFromArray<T>.
        public void Clear()
        {
            Count = 0;
            _items = new T[0];
        }

        //Returns the object at the beginning of the MyQueueFromArray<T> without removing it.
        public T Peek()
        {
            if (Count != 0)
            {
                return _items[0];
            }
            else
            {
                throw new Exception("The queue is empty.");
            }
        }

        //Determines whether an element is in the MyQueueFromArray<T>.
        public bool Contains(T value)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i].Equals(value))
                {
                    return true;
                }
            }
            return false;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            foreach (var item in _items)
            {
                yield return item;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (var item in _items)
            {
                yield return item;
            }
        }
    }
}
