using System;
using System.Collections.Generic;

namespace TreeLCRS
{
    public class Tree<T>
    {
        private Node<T> root;
        public int Count { get; private set; }
        public Tree()
        {
            root = null;
            Count = 0;
        }
        public Tree(T data)
        {
            root = new Node<T>(data);
            Count = 1;
        }
        /// <summary>
        /// Is used to set the root of the tree
        /// </summary>
        /// <param name="data">Should be unique</param>
        public void SetRoot(T data)
        {
            if (root != null)
            {
                throw new InvalidOperationException("Tree already has a root");
            }
            root = new Node<T>(data);
            Count++;
        }
        /// <summary>
        /// Insert a new node with parent which has data given in parentData
        /// </summary>
        /// <param name="data">Unique value for new node</param>
        public void Insert(T data, T parentData)
        {
            if(IsEmpty())
            {
                throw new InvalidOperationException("Tree is empty. Set the root first.");
            }
            if (Contains(data))
            {
                throw new ArgumentException("Tree already contains an elemnt with given data", "data");
            }

            Node<T> parent = Find(parentData);
            if (parent == null)
            {
                throw new ArgumentException("Tree doesn't contain an element with given parent's data", "parentData");
            }

            Insert(data, parent);
            Count++;
        }

        private void Insert(T data, Node<T> parent)
        {
            if (parent.Child == null)
            {
                parent.Child = new Node<T>(data);
                return;
            }

            parent = parent.Child;

            while (parent.Next != null)
            {
                parent = parent.Next;
            }

            parent.Next = new Node<T>(data);
        }
        public bool Contains(T data)
        {
            return Find(data) != null;
        }

        private Node<T> Find(T data)
        {
            Stack<Node<T>> visitNext = new Stack<Node<T>>();
            Node<T> node = root;
            visitNext.Push(node);

            while (visitNext.Count != 0)
            {
                node = visitNext.Pop();
                if (node.Data.Equals(data))
                {
                    return node;
                }
                if (node.Child != null)
                {
                    visitNext.Push(node.Child);
                }
                if (node.Next != null)
                {
                    visitNext.Push(node.Next);
                }
            }

            return null;
        }

        public void Traverse(Action<T> processNodeData)
        {
            Traverse(root, processNodeData);
        }
        private void Traverse(Node<T> root, Action<T> processNodeData)
        {
            while (root != null)
            {
                processNodeData(root.Data);
                if (root.Child != null)
                {
                    Traverse(root.Child, processNodeData);
                }
                root = root.Next;
            }
        }
        public bool IsEmpty()
        {
            return Count == 0;
        }
    }
}

