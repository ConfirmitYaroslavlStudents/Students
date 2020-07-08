using System;
using System.Collections;

namespace Queue
{
    public class Queue<T> : IEnumerable
    {
        private T[] _queue;
        private int _tail;
        private int _head;
        public int Count { get; set; }
        public Queue()
        {
            Count = 0;
            _tail = -1;
            _head = 0;
            _queue = new T[1000];
        }
        private bool IsEmpty()
        {
            return Count == 0;
        }
        private bool IsFull()
        {
            return _queue.Length == Count;
        }
        private void Resize()
        {
            Array.Resize(ref _queue, _queue.Length * 2);
        }
        public void Enqueue(T item)
        {
            if(IsFull())
            {
                Resize();
            }
            if (IsEmpty())
            {
                _tail = -1;
                _head = 0;
            }
            Count++;
            _queue[++_tail] = item;
        }
        public T Dequeue()
        {
            if(IsEmpty())
            {
                throw new Exception("Очередь пуста.");
            }
            Count--;
            T item = _queue[_head++];
            if (IsEmpty())
            {
                _tail = -1;
                _head = 0;
            }
            return item;
        }
        public T Peek()
        {
            if (IsEmpty())
            {
                throw new Exception("Очередь пуста.");
            }
            return _queue[_head];
        }
        public void Clear()
        {
            Count = 0;
            _tail = -1;
            _head = 0;
            _queue = new T[1000];
        }
        public bool Contains(T item)
        {
            foreach (T c in _queue)
            {
                if (c.Equals(item))
                    return true;
            }
            return false;
        }
        public IEnumerator GetEnumerator()
        {
            if (this.IsEmpty())
                throw new Exception("Очередь пуста.");
            for (int i = _tail; i <= _head; i++)
                yield return _queue[i];
        }
    }
}