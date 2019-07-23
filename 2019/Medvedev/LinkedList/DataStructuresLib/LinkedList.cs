using System;

namespace DataStructures
{
    public class LinkedList<T> : System.Collections.Generic.IEnumerable<T>
    {
        private Node<T> _head;
        private Node<T> _tail;

        public int Count { get; private set; }

        public LinkedList()
        {
            _head = null;
            _tail = null;
            Count = 0;
        }

        public LinkedList(System.Collections.Generic.IEnumerable<T> enumerable)
        {
            foreach (var x in enumerable)
                AddLast(x);
        }

        public static bool IsNullOrEmpty(LinkedList<T> list)
        {
            return list is null || list._head is null;
        }

        private bool BelongsToList(Node<T> node)
        {
            return node != null && ReferenceEquals(node.ParentList, this);
        }

        public void AddAfter(Node<T> node, T value)
        {
            if (node is null)
                throw new ArgumentNullException("Node must not be null");
            if (!BelongsToList(node))
                throw new ArgumentException("Node must belong to this LinkedList");
            
            if (node == _tail)
            {
                AddLast(value);
                return;
            }

            Node<T> nextNode = new Node<T>(value, node.NextNode, this);
            node.NextNode = nextNode;
            Count++;
        }

        public void AddBefore(Node<T> node, T value)
        {
            if (node is null)
                throw new ArgumentNullException("Node must not be null");
            if (!BelongsToList(node))
                throw new ArgumentException("Node must belong to this LinkedList");
            
            if (node == _head)
            {
                AddFirst(value);
                return;
            }

            Node<T> nextNode = new Node<T>(value, node, this);

            var i = _head;
            while (i.NextNode != null && i.NextNode != node)
                i = i.NextNode;

            i.NextNode = nextNode;
            Count++;
        }

        public void AddFirst(T value)
        {
            Node<T> node = new Node<T>(value, _head, this);

            if (IsNullOrEmpty(this))
                _tail = node;

            _head = node;
            Count++;
        }

        public void AddLast(T value)
        {
            Node<T> node = new Node<T>(value, null, this);

            if (IsNullOrEmpty(this))
            {
                _head = node;
                _tail = node;
            }
            else
            {
                _tail.NextNode = node;
                _tail = node;
            }
            Count++;
        }

        public void Clear()
        {
            _head = null;
            _tail = null;
            Count = 0;
        }

        public bool Contains(T value)
        {
            return FindNode(value) != null;
        }

        public Node<T> FindNode(T value)
        {
            for (var i = _head; i != null; i = i.NextNode)
                if (i.Value.Equals(value))
                    return i;
            return null;
        }

        public void Remove(T value)
        {
            Node<T> removableNode = FindNode(value);

            if (IsNullOrEmpty(this))
                throw new InvalidOperationException("Cannot remove value from empty LinkedList");

            if (removableNode is null)
                return;

            if (removableNode == _head)
            {
                RemoveFirst();
                return;
            }
            if (removableNode == _tail)
            {
                RemoveLast();
                return;
            }

            Node<T> i = _head;
            while (i.NextNode != removableNode)
                i = i.NextNode;

            i.NextNode = removableNode.NextNode;
            removableNode.NextNode = null;
            Count--;
        }

        public void RemoveFirst()
        {
            if (IsNullOrEmpty(this))
                throw new InvalidOperationException("Cannot remove first value from empty LinkedList");

            if (_head == _tail)
            {
                _head = null;
                _tail = null;
            }
            else
                _head = _head.NextNode;
            Count--;
        }

        public void RemoveLast()
        {
            if (IsNullOrEmpty(this))
                throw new InvalidOperationException("Cannot remove last value from empty LinkedList");

            if (_head == _tail)
            {
                _head = null;
                _tail = null;
            }
            else
            {
                Node<T> i = _head;
                while (i.NextNode.NextNode != null)
                    i = i.NextNode;
                i.NextNode = null;
            }
            Count--;
        }

        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            for (var node = _head; node != null; node = node.NextNode)
                yield return node.Value;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
