using System;
using System.Collections;
using System.Collections.Generic;

namespace DllLIb
{
    class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Previous { get; set; }
        public Node<T> Next { get; set; }
        public Node(T value)
        {
            Value = value;
        }
    }
    public class DoubleLinkedList<T> : IEnumerable<T>
    {
        private Node<T> _head;
        private Node<T> _tail;
        public int Count { get; private set; }
        public DoubleLinkedList()
        {
            Count = 0;
        }
        public void AddLast(T value)
        {
            var node = new Node<T>(value);
            if (_head == null)
            {
                _head = node;
                _tail = node;
                Count = 1;
                return;
            }
            _tail.Next = node;
            node.Previous = _tail;
            _tail = node;
            Count++;
        }
        private void removeAfter(Node<T> previous, Node<T> current)
        {
            if (previous != null)
            {
                previous.Next = current.Next;
                previous.Previous = current.Previous;
                if (current.Next == null)
                {
                    _tail = previous;
                }
            }
            else
            {
                _head = _head.Next;

                if (_head == null)
                {
                    _tail = null;
                }
            }
        }
        public void DeleteFirst()
        {
            removeAfter(null, _head);
            Count--;
        }
        public void DeleteLast()
        {
            removeAfter(_tail.Previous, _tail);
            Count--;
        }
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            }
            Node<T> current  = _head;
            Node<T> previous = null;
            for (int ind = 0; ind < index; ind++)
            {
                previous = current;
                current = current.Next;
            }
            removeAfter(previous, current);
            Count--;
        }
        public void Remove(T value)
        {
            Node<T> previous = null;
            for (var current = _head; current != null; current = current.Next)
            {
                if (current.Value.Equals(value))
                {
                    removeAfter(previous, current);
                    Count--;
                    break;
                }
                else
                {
                    previous = current;
                }
            }
        }
        public void AddFirst(T value)
        {
            var node = new Node<T>(value);
            if (_head == null)
            {
                _head = node;
                _tail = node;
                Count = 1;
                return;
            }
            _head.Previous = node;
            node.Next = _head;
            _head = node;
            Count++;
        }
        public void Insert(int index, T value)
        {
            if (index < 0 || index > Count)
            {
                throw new IndexOutOfRangeException();
            }
            Node<T> node = new Node<T>(value);
            Node<T> current = _head;
            Node<T> previous = null;
            for (int ind = 0; ind < index; ind++)
            {
                previous = current;
                current = current.Next;
            }
            if (previous != null && current != null)
            {
                previous.Next = node;
                current.Previous = node;
                node.Previous = previous;
                node.Next = current;
            }
            else if(previous == null)
            {
                AddFirst(value);
            }
            else
            {
                AddLast(value);
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (var current = _head; current != null; current = current.Next)
            {
                yield return current.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var current = _head; current != null; current = current.Next)
            {
                yield return current.Value;
            }
        }
    }
}
