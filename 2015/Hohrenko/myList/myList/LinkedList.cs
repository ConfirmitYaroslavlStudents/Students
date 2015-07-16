using System;
using System.Collections;
using System.Collections.Generic;

namespace MyList
{
    public class EnumLinkedList<T> : IEnumerator<T>
    {
        private Node<T> _first;
        private Node<T> _node;
        private bool _firstFlag;

        public EnumLinkedList(Node<T> node)
        {
            _first = _node = node;
            _firstFlag = true;
        }
        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            if (_firstFlag)
            {
                _firstFlag = false;
                return _node != null;
            }
            _node = _node.Next;
            return _node != null;
        }

        public void Reset()
        {
            _node = _first;
        }

        public T Current { get { return _node.Value; } }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }

    public class Node<T>
    {
        public T Value;
        public Node<T> Next, Previous;

        public Node(T val)
        {
            Value = val;
            Next = null;
            Previous = null;
        }    

    }

    public class LinkedList<T> : IEnumerable<T>, IList<T>
    {
        public Node<T> First { get; private set; }
        public Node<T> Last { get; private set; }
        private int _count;

        public LinkedList()
        {
            First = null;
            Last = null;
            _count = 0;
        }

        public int Count
        {
            get { return _count; }
        }

        public void Add(T item)
        {
            Node<T> node = new Node<T>(item);
            if (Count == 0)
            {
                First = node;
                Last = First;
            }
            else
            {
                Last.Next = node;
                node.Previous = Last;
                Last = node;
            }
            _count++;
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                Add(item);
            }
        }

        public void Clear()
        {
            First = null;
            Last = null;
            _count = 0;
        }

        public bool Contains(T item)
        {
            for (var currentNode = First; currentNode != null; currentNode = currentNode.Next)
            {
                if (currentNode.Value.Equals(item))
                    return true;
            }
            return false;
        }

        public void Remove(T item)
        {
            var nodeToRemove = NodeAt(IndexOf(item));
            if (IndexOf(item) == 0)
            {
                First = nodeToRemove.Next;
                nodeToRemove.Next.Previous = nodeToRemove.Previous;
                _count--;
                return;
            }
            nodeToRemove.Previous.Next = nodeToRemove.Next;
            nodeToRemove.Next.Previous = nodeToRemove.Previous;
            _count--;
        }

        public void RemoveAt(int index)
        {
            if ((index >= 0) && (index < Count))
                Remove(NodeAt(index).Value);
        }

        public int IndexOf(T item)
        {
            int itemIndex = 0;
            Node<T> currentNode;
            for (currentNode = First; currentNode != null; currentNode = currentNode.Next)
            {
                if (currentNode.Value.Equals(item))
                    return itemIndex;
                itemIndex++;
            }
            return -1;
        }
        private Node<T> NodeAt(int index)
        {
            int ind = 0;
            Node<T> currentNode;

            if (index >= Count || index < 0)
                throw new ArgumentOutOfRangeException();
            for (currentNode = First; currentNode != null; currentNode = currentNode.Next)
            {
                if (ind == index)
                    break;
                ind++;
            }
            return currentNode; 
        }

        public T this[int index]
        {
            get { return NodeAt(index).Value; }
            set
            {
                NodeAt(index).Value = value;
            }
        }

        public void Insert(int index, T item)
        {
            Node<T> nodeToInsert = new Node<T>(item);
            Node<T> nodeWithIndex = NodeAt(index);
            
            if ((index < Count) && (index >= 0))
            {
                _count++;
                if (index == 0)
                {
                    var tempNode = First;
                    tempNode.Previous = nodeToInsert;
                    First = nodeToInsert;
                    First.Next = tempNode;
                    return;
                }             

                nodeToInsert.Next = nodeWithIndex;
                nodeToInsert.Previous = nodeWithIndex.Previous;
                nodeWithIndex.Previous.Next = nodeToInsert;
                nodeWithIndex.Previous = nodeToInsert;
            }         
        }

        public IEnumerator<T> GetEnumerator()
        {
            return  new EnumLinkedList<T>(First);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
