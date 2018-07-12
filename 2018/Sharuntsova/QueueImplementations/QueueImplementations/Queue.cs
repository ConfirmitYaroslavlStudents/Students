using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    class Queue<T>
    {
        private Node<T> _head;
        private Node<T> _tail;
        private int count;

        public void Enqueue(T data)
        {
            Node<T> newNode = new Node<T>(data);
            Node<T> tempNode = _tail;
            _tail = newNode;
            if (count==0)
                _head = _tail;
            else
                tempNode.Next = newNode;
            count++;
        }

        public T Dequeue ()
        {
            if (count == 0)
                throw new Exception ("Queue is empty.");
            T result = _head.Data;
            _head = _head.Next;
            count--;
            return result;
        }

        public int Count ()
        {
            return count;
        }

        public T First()
        {
            if (count == 0)
                throw new InvalidOperationException();
            return _head.Data;
        }
        public T Last()
        {
            if (count == 0)
                throw new InvalidOperationException();
            return _tail.Data;
        }

        public bool Contains (T data)
        {
            Node<T> currentNode = _head;
            while (currentNode!=null)
            {
                if (currentNode.Data.Equals(data))
                    return true;
                currentNode = currentNode.Next;
            }
            return false;
        }

    }

    public class ArrayQueue<T>
    {
        private T[] _array;
        private int _augmentStep;
        private int count;

        public ArrayQueue ()
        {
            _array = new T[10];
            _augmentStep = 10;
        }

        public ArrayQueue(int arrayLength)
        {
            _array = new T[arrayLength];
            _augmentStep = 10;
        }

        public ArrayQueue(int arrayLength, int augmentStep)
        {
            _array = new T[arrayLength];
            _augmentStep = augmentStep;
        }

        public void Enqueue(T data)
        {
            if (count == _array.Length)
                ExpandArray();
            _array[count] = data;
            count++;
        }

        public T Dequeue()
        {
            if (count == 0)
                throw new InvalidOperationException();
            T output = _array[0];
            for (int i = 0; i < _array.Length - 1; i++)
                _array[i] = _array[i + 1];
            return output;
        }

        public T First()
        {
            return _array[0];
        }

        public T Last()
        {
            return _array[count - 1];
        }

        public int Count()
        {
            return count;
        }

        public bool Contains (T data)
        {
            return _array.Contains(data);
        }

        private void ExpandArray()
        {
            T[] newArray = new T[_array.Length + _augmentStep];
            for (int i =0; i<_array.Length; i++)
                newArray[i] = _array[i];

            _array = newArray;
        }
    }
}
