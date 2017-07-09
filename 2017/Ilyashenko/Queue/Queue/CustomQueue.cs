using System;
using System.Collections.Generic;
using System.Linq;

namespace Queue
{
    public class CustomQueue<T>
    {
        private int _defaultCapacity = 10;
        private int _size;
        private int _capacity;
        private int _head;
        private int _tail;
        private T[] _queueElements;

        public CustomQueue()
        {
            _queueElements = new T[_defaultCapacity];
            _head = 0;
            _tail = 0;
            _size = 0;
            _capacity = _defaultCapacity;
        }

        public CustomQueue(int concreteCapacity)
        {
            if (concreteCapacity <= 0)
                throw new ArgumentOutOfRangeException();
            _queueElements = new T[concreteCapacity];
            _head = 0;
            _tail = 0;
            _size = 0;
            _capacity = concreteCapacity;
        }

        public CustomQueue(IEnumerable<T> itemCollection)
        {
            if (itemCollection.Count() == 0)
            {
                _queueElements = new T[_defaultCapacity];
                _head = 0;
                _tail = 0;
                _size = 0;
                _capacity = _defaultCapacity;
            }
            else
            {
                _queueElements = new T[itemCollection.Count() * 2];
                Array.Copy(itemCollection.ToArray(), _queueElements, itemCollection.Count());
                _head = 0;
                _tail = itemCollection.Count();
                _size = itemCollection.Count();
                _capacity = itemCollection.Count()*2;
            }
        }

        public int Count()
        {
            return _size;
        }

        public void Enqueue(T item)
        {
            if (_size > _capacity/2)
                IncreaseCapacity();
            _queueElements[_tail] = item;
            _tail++;
            _size++;
        }

        public T Dequeue()
        {
            if (_size == 0)
                throw new InvalidOperationException();
            var result = _queueElements[_head];
            _head++;
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
            var newQueue = new T[_capacity*2];
            Array.Copy(_queueElements, _head, newQueue, 0, _size);
            _capacity = _capacity*2;
            _queueElements = newQueue;
            _head = 0;
            _tail = _size;
        }

        public void Clear()
        {
            Array.Clear(_queueElements, _head, _size);
            _head = 0;
            _tail = 0;
            _size = 0;
        }

        public void CopyTo(T[] array, int startIndex)
        {
            for (int i = _head; i < _size; i++)
            {
                array[startIndex + i] = _queueElements[i];
            }
        }

        public bool Contains(T item)
        {
            return _queueElements.Contains(item);
        }

        public T[] ToArray()
        {
            var newArray = new T[_size];
            Array.Copy(_queueElements, _head, newArray, 0, _size);
            return newArray;
        }
    }
}
