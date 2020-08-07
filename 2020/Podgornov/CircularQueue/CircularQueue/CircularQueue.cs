using System;
using System.Collections;
using System.Collections.Generic;

namespace CircularQueue
{
    public class CircularQueue<T>:IEnumerable<T>
    {
        private int Default_Size = 10;

        private T[] _items;

        private int _head;

        private int _tail;

        private int Head { get => _head; set => _head = IndexIncrement(value); }

        private int Tail { get => _tail; set => _tail = IndexIncrement(value); }

        private int IndexIncrement(int index) => index % _items.Length;

        private void Resize()
        {
            var newQueue = new T[2 * _items.Length];
            Array.Copy(_items, Head, newQueue, 0, Size - Head);
            Array.Copy(_items, 0, newQueue, Size - Head, Tail);
            _items = newQueue;
            Head = 0;
            Tail = Size / 2;
        }

        public CircularQueue()
        {
            _items = new T[Default_Size];
        }

        public int Count { get; private set; }

        public int Size { get => _items.Length; }

        public void Enqueue(T value)
        {
            if (Count == _items.Length)
                Resize();
            Count++;
            _items[Tail++] = value;
        }

        public T Dequeue()
        {
            if (Count == 0)
                throw new InvalidOperationException("Queue was empty.");
            Count--;
            return _items[Head++];
        }

        public T Peek()
        {
            return _items[Head];
        }

        public IEnumerator<T> GetEnumerator()
        {
            int count = 0;
            int index = Head;
            while (Count != count)
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
