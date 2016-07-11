using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyListLib
{
    public class Node<T>
    {
        public T Value { set; get; }
        public Node<T> nextNode { set; get; }
        public Node(T value)
        {
            this.Value = value;
            this.nextNode = null;
        }
    }

    public class MyListNodes<T> : IEnumerable<T>
    {
        Node<T> _headNode;
        Node<T> _tailNode;
        public int Count { get; private set; }

        public MyListNodes()
        {
            _headNode = null;
            _tailNode = null;
            Count = 0;
        }

        public MyListNodes(IEnumerable<T> list)
        {
            if (list == null || !list.Any())
            {
                Count = 0;
                return;
            }

            CreateFirstNode(list.ElementAt(0));

            foreach (var value in list.Skip(1))
            {
                Add(value);
            }
        }

        public T this[int i]
        {
            get
            {
                if (i >= 0 && i < Count)
                    return GetNode(i);
                else
                    return default(T);
            }
            set
            {
                if (i >= 0 && i < Count)
                    SetNode(i, value);
            }
        }

        private T GetNode(int i)
        {
            var tempNode = FindNode(i);
            return tempNode.Value;
        }

        private Node<T> FindNode(int i)
        {
            Node<T> tempNode = _headNode;
            while (i > 0)
            {
                tempNode = tempNode.nextNode;
                i--;
            }
            return tempNode;
        }

        private void SetNode(int i, T value)
        {
            var tempNode = FindNode(i);
            tempNode.Value = value;
        }

        public void Add(T value)
        {
            if (_tailNode != null)
            {
                Node<T> newNode = new Node<T>(value);
                _tailNode.nextNode = newNode;
                _tailNode = newNode;
                Count++;
            }
            else
            {
                CreateFirstNode(value);
            }
        }

        private void CreateFirstNode(T value)
        {
            _headNode = new Node<T>(value);
            _tailNode = _headNode;
            Count = 1;
        }

        public void Clear()
        {
            _headNode = null;
            _tailNode = null;
            Count = 0;
        }

        public void Insert(int i, T value)
        {
            var tempNode = FindNode(i-1);
            var newNode = new Node<T>(value);
            var nextForNewNode = tempNode.nextNode;
            tempNode.nextNode = newNode;
            newNode.nextNode = nextForNewNode;
            Count++;
        }

        public bool Contains(T value)
        {
            for (Node<T> listNode = _headNode; listNode != null; listNode = listNode.nextNode)
            {
                if (listNode.Value.Equals(value))
                    return true;
            }
            return false;
        }

        public int IndexOf(T value)
        {
            int i = 0;
            for (Node<T> listNode = _headNode; listNode != null; listNode = listNode.nextNode)
            {
                if (listNode.Value.Equals(value))
                    return i;
                i++;
            }
            return -1;
        }

        #region IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            for (Node<T> listNode = _headNode; listNode != null; listNode = listNode.nextNode)
            {
                yield return listNode.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (Node<T> listNode = _headNode; listNode != null; listNode = listNode.nextNode)
            {
                yield return listNode.Value;
            }
        }
        #endregion
    }
}
