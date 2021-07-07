using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedListBaulina
{
    public class LinkedList<T> : IEnumerable<T>
    {
        private Node<T> _head; 
        private Node<T> _tail; 
        private int _count;

        public int Count => _count;
        public bool IsEmpty => _count == 0;

        public LinkedList()
        {
            _head = null;
            _tail = null;
            _count = 0;
        }

        public LinkedList(T[] inputArray)
        {
            foreach (var inputItem in inputArray)
            {
                Add(inputItem);
            }
        }

        public void Add(T data)
        {
            var node = new Node<T>(data);
            if (_head == null)
                _head = node;
            else
                _tail.Next = node;
            _tail = node;
            _count++;
        }

        public void Remove(T data)
        {
            var current = _head;
            Node<T> previous = null;
            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    //not a head node
                    if (previous != null)
                    {
                        // remove current
                        previous.Next = current.Next;

                        if (current.Next == null) //tail
                            _tail = previous;
                    }
                    else
                    {
                        //remove head
                        _head = _head.Next;

                        // list is empty -> tail is null
                         if (_head == null)
                            _tail = null;
                    }

                    _count--;
                    return;
                }

                previous = current;
                current = current.Next;
            }

            throw new ArgumentException();
        }



        public void Clear()
        {
            _head = null;
            _tail = null;
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
            var node = new Node<T>(data) { Next = _head };
            _head = node;
            if (_count == 0)
                _tail = _head;
            _count++;
        }

        public void Insert(int index, T data)
        {
            if (index == 0 && _count == 0) Add(data);
            if (index < 0 || index >= _count) throw new IndexOutOfRangeException();
            if (index == 0) AppendFirst(data);
            else
            {
                var currentNode = _head;
                Node<T> previousNode = null;
                for (int i = 1; i <= index; i++)
                {
                    previousNode = currentNode;
                    currentNode = currentNode.Next;
                }

                var nodeToBeInserted = new Node<T>(data);
                previousNode.Next = nodeToBeInserted;
                nodeToBeInserted.Next = currentNode;
                _count++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
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
