using System;
using System.Collections;
using System.Collections.Generic;

namespace CircularQueue
{
    public class CircularQueue<T>:IEnumerable<T>
    {
        public const int DefaultSize = 10;

        private T[] _items;

        private int _head;

        private int _tail;

        private int Head { get => _head; set => _head = IndexIncrement(value); }

        private int Tail { get => _tail; set => _tail = IndexIncrement(value); }

        private int IndexIncrement(int index) => index % _items.Length;

        private void Resize()
        {
            var newQueue = new T[2 * _items.Length];
            Array.Copy(_items, Head, newQueue, 0, Capacity - Head);
            Array.Copy(_items, 0, newQueue, Capacity - Head, Tail);
            _items = newQueue;
            Head = 0;
            Tail = Capacity / 2;
        }

        public CircularQueue(int capacity = DefaultSize)
        {
            if (capacity <= 0) 
                throw new ArgumentException("Capacity value was unacceptable.");
            _items = new T[capacity];
        }

        public int Size { get; private set; }

        public int Capacity => _items.Length;

        public void Enqueue(T value)
        {
            if (Size == _items.Length)
                Resize();
            Size++;
            _items[Tail++] = value;
        }

        public virtual T Dequeue()
        {
            if (Size == 0)
                throw new InvalidOperationException("Queue was empty.");
            Size--;
            return _items[Head++];
        }

        public T Peek() => _items[Head];

        public IEnumerator<T> GetEnumerator()
        {
            var count = 0;
            var index = Head;
            while (Size != count)
            {
                count++;
                yield return _items[index];
                index = IndexIncrement(index + 1);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
