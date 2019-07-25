using System;
using System.Collections;
using System.Collections.Generic;

namespace MySetLib
{
    public class Set<T> : IEnumerable<T> where T : IComparable<T>
    {
        public class Node<V>
        {
            public V Data { get; set; }
            public Node<V> LeftChild { get; set; }
            public Node<V> RightChild { get; set; }
            public Node<V> Parent { get; set; }
            public Node(V data)
            {
                Data = data;
            }
        }
        private Node<T> root;
        public int count;
        public Set()
        {
            root = null;
            count = 0;
        }

        public Node<T> Find(T data)
        {
            var node = root;
            while (node != null)
            {
                if (node.Data.Equals(data))
                {
                    return node;
                }
                node = data.CompareTo(node.Data) >= 0 ? node.RightChild : node.LeftChild;
            }
            return null;
        }

        public bool Add(T data)
        {
            if (Find(data) != null)
            {
                return false;
            }
            Node<T> newNode = new Node<T>(data);
            if (root == null)
            {
                root = newNode;
                count++;
                return true;
            }
            Node<T> current = root;
            newNode.Parent = root;
            while (true)
            {
                if (newNode.Data.CompareTo(current.Data) <= 0)
                {
                    if (current.LeftChild == null)
                    {
                        newNode.Parent = current;
                        current.LeftChild = newNode;
                        count++;
                        return true;
                    }
                    current = current.LeftChild;
                }
                else
                {
                    if (current.RightChild == null)
                    {
                        newNode.Parent = current;
                        current.RightChild = newNode;
                        count++;
                        return true;
                    }
                    current = current.RightChild;
                }
            }
        }

        public bool Remove(T data)
        {
            Node<T> removeNode = Find(data);
            if (root == null || removeNode == null)
            {
                return false;
            }

            if (count == 1)
            {
                root = null;
                count--;
                return true;
            }

            if (removeNode.LeftChild == null && removeNode.RightChild == null)
            {
                RemoveNodeWithoutChildren(removeNode);
                count--;
            }
            else if (removeNode.LeftChild == null || removeNode.RightChild == null)
            {
                RemoveNodeWithOneChild(removeNode);
                count--;
            }
            else
            {
                RemoveNodeWithTwoChildren(removeNode);
                count--;
            }

            return true;
        }

        private void RemoveNodeWithoutChildren(Node<T> removeNode)
        {
            if (removeNode.Parent.LeftChild == removeNode)
                removeNode.Parent.LeftChild = null;
            if (removeNode.Parent.RightChild == removeNode)
                removeNode.Parent.RightChild = null;
            removeNode.Parent = null;
        }

        private void RemoveNodeWithOneChild(Node<T> removeNode)
        {
            if (removeNode.LeftChild == null)
            {
                if (removeNode.Parent.LeftChild == removeNode)
                    removeNode.Parent.LeftChild = removeNode.RightChild;
                else
                    removeNode.Parent.RightChild = removeNode.RightChild;
                removeNode.RightChild.Parent = removeNode.Parent;
            }
            else
            {
                if (removeNode.Parent.LeftChild == removeNode)
                    removeNode.Parent.LeftChild = removeNode.LeftChild;
                else
                    removeNode.Parent.RightChild = removeNode.LeftChild;
                removeNode.LeftChild.Parent = removeNode.Parent;
            }
        }
        private void RemoveNodeWithTwoChildren(Node<T> removeNode)
        {
            Node<T> minNode = GetMinimumNodeUnderTheTree(removeNode.RightChild);
            T tmp = minNode.Data;
            if (minNode.LeftChild == null && minNode.RightChild == null)
                RemoveNodeWithoutChildren(minNode);
            else
                RemoveNodeWithOneChild(minNode);
            removeNode.Data = tmp;
        }
        private Node<T> GetMinimumNodeUnderTheTree(Node<T> node)
        {
            if (node.LeftChild == null)
                return node;
            return GetMinimumNodeUnderTheTree(node.LeftChild);
        }
        public Set<T> Union(Set<T> set)
        {
            if (set == null)
                return this;
            Set<T> newSet = new Set<T>();
            foreach (T i in set)
            {
                newSet.Add(i);
            }
            foreach (T i in this)
            {
                newSet.Add(i);
            }
            return newSet;
        }
        public Set<T> Intersection(Set<T> set)
        {
            if (set == null)
                return this;
            Set<T> newSet = new Set<T>();
            foreach (T i in set)
            {
                Node<T> tmpNode = Find(i);
                if (tmpNode != null)
                    newSet.Add(i);
            }
            return newSet;
        }
        public void Clear()
        {
            root = null;
            count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            tmp.Clear();
            TraverseTreePreOrder(this.root);
            return tmp.GetEnumerator();
        }

        List<T> tmp = new List<T>();
        private void TraverseTreePreOrder(Node<T> node)
        {
            if (node == null)
                return;
            tmp.Add(node.Data);
            TraverseTreePreOrder(node.LeftChild);
            TraverseTreePreOrder(node.RightChild);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
