using System;

namespace QueueLib
{
    public class Queue<T>
    {
        private Item<T> _first;
        private Item<T> _last;
        public int Count { get; private set; }

        public Queue()
        {
            _first = null;
            _last = null;
            Count = 0;
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
            try
            {
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
            catch (Exception)
            {                
                throw new InvalidOperationException("Queue is Empty");
            }
        }

    }
}
