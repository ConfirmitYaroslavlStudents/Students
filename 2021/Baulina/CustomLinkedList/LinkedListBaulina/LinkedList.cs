using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedListBaulina
{
    public class LinkedList<T> : IEnumerable<T>
    { 
        class Node<T>
        {
            public Node(T data)
            {
                Data = data;
            }
            public T Data { get; }
            public Node<T> Next { get; set; }
        }

        private Node<T> _head; 
        private int _count;

        public int Count => _count;
        public bool IsEmpty => _count == 0;

        public LinkedList()
        {
            _head = null;
            _count = 0;
        }

        public LinkedList(IEnumerable<T> inputCollection)
        {
            foreach (var inputItem in inputCollection)
            {
                Add(inputItem);
            }
        }

        public void Add(T data)
        {
            Insert(_count, data);
        }

        public void Remove(T data)
        {
            if(!Contains(data)) 
                throw new ArgumentException("Data not found");

            var current = _head;
            Node<T> previous = null;

            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;
                    }
                    else
                    {
                        _head = _head.Next;
                    }

                    _count--;
                    return;
                }

                previous = current;
                current = current.Next;
            }
        }

        public void Clear()
        {
            _head = null;
            _count = 0;
        }

        public bool Contains(T data)
        {
            var current = _head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                    return true;
                current = current.Next;
            }

            return false;
        }

        public void AppendFirst(T data)
        {
            Insert(0, data);
        }

        public void Insert(int index, T data)
        {
            if (index < 0 || index > _count) 
                throw new IndexOutOfRangeException();

            var nodeToBeInserted = new Node<T>(data);

            if (index == 0)
            {
                nodeToBeInserted.Next = _head;
                _head = nodeToBeInserted;
            }
            else
            {
                var currentNode = _head;
                Node<T> previousNode = null;
                for (int i = 1; i <= index; i++)
                {
                    previousNode = currentNode;
                    currentNode = currentNode.Next;
                }
                previousNode.Next = nodeToBeInserted;
                nodeToBeInserted.Next = currentNode;
            }
            _count++;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = _head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
    }
}
