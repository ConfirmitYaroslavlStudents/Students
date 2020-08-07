using System;
using System.Collections;

namespace Queue
{
    public class Queue<T> : IEnumerable
    {
        private T[] _queue;
        private int _tail;
        private int _head;
        public int Count { get; private set; }
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
            var NewArray = new T[_queue.Length*2];
            if (_head < _tail)
            {
                Array.Copy(_queue, _head, NewArray, 0, _queue.Length);
            }
            else
            {
                Array.Copy(_queue, _head, NewArray, 0, _queue.Length - _head);
                Array.Copy(_queue, 0, NewArray, _queue.Length - _head, _tail);
            }
            _head = 0;
            _tail = Count - 1;
            _queue = NewArray;
        }
        public void Enqueue(T item)
        {
            if(IsFull())
            {
                Resize();
            }
            if (_tail == _queue.Length - 1)
            {
                _tail = -1;
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
                throw new Exception("Queue is Empty");
            }
            if (_head == _queue.Length -1)
            {
                _head = 0;
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
                throw new Exception("Queue is Empty");
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
                throw new Exception("Queue is Empty");
            for (int i = _head; i <= _tail; i++)
                yield return _queue[i];
        }
    }
}