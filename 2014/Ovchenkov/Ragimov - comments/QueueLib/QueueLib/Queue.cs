using System;
using System.Collections.Generic;
using QueueInterface;

namespace QueueLib
{
    public class Queue<T> : IQueueable<T>
    {
        private class Item<TI>
        {
            private readonly TI _value;

            public Item(TI value)
            {
                _value = value;
            }

            public TI Value
            {
                get { return _value; }
            }

            public Item<T> Next { get; set; }
        }
        private Item<T> _first;
        private Item<T> _last;
        public int Count { get; private set; }

        public Queue()
        {
            Clear();
        }

        public void Enqueue(T value)
        {
            var newItem = new Item<T>(value);
            if (_last != null)
            {
                _last.Next = newItem;
                _last = newItem;
            }
            else
            {
                _first = newItem;
                _last = _first;
            }
            Count++;
        }

        public T Dequeue()
        {
            if (Count == 0) throw new InvalidOperationException("Queue is Empty");
            T value = _first.Value;
            if (Count != 1)
            {
                _first = _first.Next;
            }
            else
            {
                _first = null;
                _last = null;
            }
            Count--;
            return value;
        }

        public T Peek()
        {
            if (Count == 0) throw new InvalidOperationException("Queue is Empty");
            return _first.Value;
        }

        public void Clear()
        {
            _first = null;
            _last = null;
            Count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var temp = _first;
            yield return temp.Value;
            while (temp.Next != null)
            {
                temp = temp.Next;
                yield return temp.Value;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
