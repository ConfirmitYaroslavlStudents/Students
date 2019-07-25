using System;
using System.Collections;
using System.Collections.Generic;

namespace DoubleLinkedListLib
{
    public class DoubleLinkedList<T> : IEnumerable<T>
    {
        private Node<T> _head;
        private Node<T> _tail;
        public int Count { get; private set; } = 0;
        public DoubleLinkedList() { }
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
        private void RemoveNode(Node<T> node)
        {
            if (node.Previous != null)
            {
                node.Previous.Next = node.Next;
                node.Previous.Previous = node.Previous;

                if (node.Next == null)
                {
                    _tail = node.Previous;
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

            Count--;
        }
        public void DeleteFirst()
        {
            RemoveNode(_head);
        }
        public void DeleteLast()
        {
            RemoveNode(_tail);
        }
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            }

            Node<T> current = _head;

            for (int ind = 0; ind < index; ind++)
            {
                current = current.Next;
            }

            RemoveNode(current);
        }
        public void Remove(T value)
        {
            for (var current = _head; current != null; current = current.Next)
            {
                if (current.Value.Equals(value))
                {
                    RemoveNode(current);
                    break;
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
            if (index < 0 || index > Count + 1)
            {
                throw new IndexOutOfRangeException();
            }

            if (index == 0)
            {
                AddFirst(value);

                return;
            }

            if (index == Count + 1)
            {
                AddLast(value);

                return;
            }

            Node<T> node = new Node<T>(value);
            Node<T> current = _head;

            for (int ind = 0; ind < index; ind++)
            {
                current = current.Next;
            }

            if (current.Previous != null && current != null)
            {
                current.Previous.Next = node;
                current.Previous = node;
                node.Previous = current.Previous;
                node.Next = current;
            }

            Count++;
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
            return GetEnumerator();
        }
    }
}
