using System;
using System.Collections.Generic;
using System.Linq;

namespace QueueLib
{
    public class CustomQueue<T>
    {
        private int _defaultCapacity = 8;
        private int _size;
        private int _capacity;
        private int _head;
        private int _tail;
        private T[] _queueElements;

        public CustomQueue()
        {
            Init(_defaultCapacity);
        }

        public CustomQueue(int concreteCapacity)
        {
            if (concreteCapacity <= 0)
                throw new ArgumentOutOfRangeException();
            Init(concreteCapacity);
        }

        public CustomQueue(IEnumerable<T> itemCollection)
        {
            var collectionCount = itemCollection.Count();
            if (collectionCount == 0)
            {
                Init(_defaultCapacity);
            }
            else
            {
                _queueElements = new T[collectionCount * 2];
                Array.Copy(itemCollection.ToArray(), _queueElements, collectionCount);
                _head = 0;
                _tail = collectionCount;
                _size = collectionCount;
                _capacity = collectionCount * 2;
            }
        }

        private void Init(int capacity)
        {
            _queueElements = new T[capacity];
            _head = 0;
            _tail = 0;
            _size = 0;
            _capacity = capacity;
        }

        public int Count()
        {
            return _size;
        }

        public void Enqueue(T item)
        {
            if (_size == _capacity)
                IncreaseCapacity();
            _queueElements[_tail] = item;
            _tail = (_tail + 1) % _capacity;
            _size++;
        }

        public T Dequeue()
        {
            if (_size == 0)
                throw new InvalidOperationException();
            var result = _queueElements[_head];
            _head = (_head + 1) % _capacity;
            _size--;
            return result;
        }

        public T Peek()
        {
            if (_size == 0)
                throw new InvalidOperationException();
            return _queueElements[_head];
        }

        private void IncreaseCapacity()
        {
            var newQueue = new T[_capacity * 2];
            CopyTo(newQueue, 0);
            _capacity = _capacity * 2;
            _queueElements = newQueue;
            _head = 0;
            _tail = _size;
        }

        public void Clear()
        {
            Init(_defaultCapacity);
        }

        public void CopyTo(T[] array, int startIndex)
        {
            if (_head < _tail)
            {
                Array.Copy(_queueElements, _head, array, startIndex, _size);
            }
            else
            {
                Array.Copy(_queueElements, _head, array, startIndex, _queueElements.Length - _head);
                Array.Copy(_queueElements, 0, array, startIndex + _queueElements.Length - _head, _tail);
            }
        }

        public bool Contains(T item)
        {
            return _queueElements.Contains(item);
        }

        public T[] ToArray()
        {
            var newArray = new T[_size];
            CopyTo(newArray, 0);
            return newArray;
        }
    }
}