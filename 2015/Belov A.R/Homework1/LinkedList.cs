using System;
using System.Collections;
using System.Collections.Generic;
namespace Homework1
{
    public class LinkedList<T>:IEnumerable<T>
    {
        #region Fields
        private Node<T> _head;
        private Node<T> _lastNode;
        private int _count;
        #endregion
        #region Props

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
        #endregion
        #region Indexer
        public T this[int index]
        {
            get { return ElementAt(index); }
            set
            {
                NodeAt(index).Data = value;
            }
        }
        #endregion
        #region Ctors
        public LinkedList()
        {
            _count = 0;
            _head = null;
            _lastNode = null;
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
        #endregion
        #region Methods
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
                currentNode = _lastNode;
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
                _lastNode = null;
            }
            else 
            {
                if (node.PreviousNode == null)
                {
                    _head = node.NextNode;
                }
                else if (node.NextNode == null)
                {
                    _lastNode = node.PreviousNode;
                }
                Node<T>.Remove(node);
            }
            _count--;
        }
        public void Add(T item)
        {
            if (IsEmpty)
            {
                _head = new Node<T>() { Data = item };
                _lastNode = _head;
            }
            else
            {
                var newNode = new Node<T>() { Data = item };
                Node<T>.AddAfter(_lastNode, newNode);
                _lastNode = newNode;

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
                Node<T>.AddBefore(node,newNode);
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
            _lastNode = null;
        }
  
        #endregion
        #region Ifaces
        public IEnumerator<T> GetEnumerator()
        {
            for (var currentNode=_head;currentNode!=null;currentNode=currentNode.NextNode)
            {
                yield return currentNode.Data;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    
    }
}
