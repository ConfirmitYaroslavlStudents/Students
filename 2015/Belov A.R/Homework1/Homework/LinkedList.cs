using System;
using System.Collections;
using System.Collections.Generic;
namespace Homework1
{
    public class LinkedList<T>:IEnumerable<T>
    {
        private Node<T> _head;
        private Node<T> _tail;
        private int _count;
        public int Count
        {
            get
            {
                return _count;
            }
        }
        public bool IsEmpty
        {
            get
            {
                return _count == 0;
            }
        }
        public T this[int index]
        {
            get { return ElementAt(index); }
            set
            {
                NodeAt(index).Data = value;
            }
        }
        public LinkedList()
        {
            _count = 0;
            _head = null;
            _tail = null;
        }
        public LinkedList(T item)
            : this()
        {
            Add(item);
        }
        public LinkedList(IEnumerable<T> range)
            : this()
        {
            AddRange(range);
        }
        private Node<T> NodeAt(int index)
        {
            if (index >= Count || index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            Node<T> currentNode;
            if (index < Count - index - 1)
            {
                currentNode = _head;
                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.NextNode;
                }
            }
            else
            {
                currentNode = _tail;
                for (int i = Count-1; i > index; i--)
                {
                    currentNode = currentNode.PreviousNode;
                }

            }
            return currentNode;
        }

        private Node<T> Find(T item)
        {
            for (var currentNode = _head; currentNode != null; currentNode = currentNode.NextNode)
            {
                if (currentNode.Data.Equals(item))
                    return currentNode;
            }
            return null;
        }

        private void Remove(Node<T> node)
        {
            if (node.PreviousNode == null&&node.NextNode==null)
            {
                _head = null;
                _tail = null;
            }
            else 
            {
                if (node.PreviousNode == null)
                {
                    _head = node.NextNode;
                }
                else if (node.NextNode == null)
                {
                    _tail = node.PreviousNode;
                }
                node.BreakLinks();
            }
            _count--;
        }
        public void Add(T item)
        {
            if (IsEmpty)
            {
                _head = new Node<T>() { Data = item };
                _tail = _head;
            }
            else
            {
                var newNode = new Node<T>() { Data = item };
                _tail.AddAfter(newNode);
                _tail = newNode;

            }
            _count++;
        }
        public void AddRange(IEnumerable<T> range)
        {
            if (range == null)
                throw new ArgumentNullException("range");
            foreach (var item in range)
            {
                Add(item);
            }
        }

        public T ElementAt(int index)
        {
            return NodeAt(index).Data;
        }
        public void InsertAt(int index, T value)
        {
            if (index == Count)
            { 
                Add(value);
            }
            else
            {
                var node = NodeAt(index);
                var newNode = new Node<T>() {Data = value};
                node.AddBefore(newNode);
                if (index == 0)
                {
                    _head = newNode;
                }
                _count++;
            }
        }

        public void InsertRange(int index, IEnumerable<T> range)
        {
            if (range == null)
                throw new ArgumentNullException("range");
            foreach (var item in range)
            {
                InsertAt(index++,item);
            }
        }
        public void RemoveAt(int index)
        {
            var node = NodeAt(index);
            Remove(node);
        }

        public void Remove(T item)
        {
            var node = Find(item);
            if (node != null)
            {
                Remove(node);
            }
        }

        public bool Contains(T item)
        {
            var node = Find(item);
            return node != null;
        }

        public void Clear()
        {
            _count = 0;
            _head = null;
            _tail = null;
        }
  
        //[TODO] reimplement without yield
        public IEnumerator<T> GetEnumerator()
        {
            return new LinkedListEnumerator<T>(_head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    
    }
}
