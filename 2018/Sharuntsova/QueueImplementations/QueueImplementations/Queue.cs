using System;
using System.Linq;


namespace QueueImplementations
{
    class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }

        public Node(T data)
        {
            Data = data;
        }

    }

    public class Queue<T> : IQueue<T>
    {
        private Node<T> _head;
        private Node<T> _tail;
        private int _count;

        public void Enqueue(T data)
        {
            var newNode = new Node<T>(data);
            var tempNode = _tail;
            _tail = newNode;
            if (_count == 0)
                _head = _tail;
            else
                tempNode.Next = newNode;
            _count++;
        }

        public T Dequeue()
        {
            if (_count == 0)
                throw new InvalidOperationException("Queue is empty.");
            T result = _head.Data;
            _head = _head.Next;
            _count--;
            return result;
        }

        public int Count()
        {
            return _count;
        }

        public T First()
        {
            if (_count == 0)
                throw new InvalidOperationException("Queue is empty.");
            return _head.Data;
        }
        public T Last()
        {
            if (_count == 0)
                throw new InvalidOperationException("Queue is empty.");
            return _tail.Data;
        }

        public bool Contains(T data)
        {
            var currentNode = _head;
            while (currentNode != null)
            {
                if (currentNode.Data.Equals(data))
                    return true;
                currentNode = currentNode.Next;
            }
            return false;
        }

    }

    public class ArrayQueue<T> : IQueue<T>
    {
        private T[] _array;
        private int _count;
        private int _initialSizeArray = 100;
        private int _firstElementIndex;
        private int _lastElementIndex;

        public ArrayQueue()
        {
            _array = new T[_initialSizeArray];
            _lastElementIndex = -1;
        }

        public void Enqueue(T data)
        {
            if (_count == _array.Length)
                ExpandArray();

            if (_lastElementIndex == _array.Length - 1)
                _lastElementIndex = -1;
            _array[_lastElementIndex + 1] = data;
            _count++;
            _lastElementIndex++;
        }

        public T Dequeue()
        {
            if (_count == 0)
                throw new InvalidOperationException("Queue is empty.");

            T output = _array[_firstElementIndex];
            if (_firstElementIndex == _array.Length - 1)
                _firstElementIndex = 0;
            else _firstElementIndex++;
            _count--;
            if (_count == 0)
            {
                _firstElementIndex = 0;
                _lastElementIndex = 0;
            }
            return output;
        }

        public T First()
        {
            if (_count == 0)
                throw new InvalidOperationException("Queue is empty.");
            return _array[_firstElementIndex];
        }

        public T Last()
        {
            if (_count == 0)
                throw new InvalidOperationException("Queue is empty.");
            return _array[_lastElementIndex];
        }

        public int Count()
        {
            return _count;
        }

        public bool Contains(T data)
        {
            return _array.Contains(data);
        }

        private void ExpandArray()
        {
            T[] newArray = new T[2 * _array.Length];
            if (_firstElementIndex < _lastElementIndex)
            {
                for (int i = 0; i < _array.Length; i++)
                    newArray[i] = _array[i];
            }
            else
            {
                for (int i = _firstElementIndex; i < _array.Length; i++)
                    newArray[i - _firstElementIndex] = _array[i];
                int currentIndex = _array.Length - _firstElementIndex;
                for (int i = 0; i <= _lastElementIndex; i++)
                    newArray[i + currentIndex] = _array[i];
            }

            _firstElementIndex = 0;
            _lastElementIndex = _count - 1;
            _array = newArray;
        }
    }
}
