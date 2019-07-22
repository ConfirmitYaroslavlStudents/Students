using System;
using System.Collections.Generic;
using System.Collections;

namespace DaraStructures
{
    // DONE TODO: unit tests
    // DONE TODO: int key -> generalize
    // DONE TODO: remove unused code
    // DONE TODO: alignment
    // DONE TODO: is null 
    // DONE TODO: implement IEnumerable


    public class Node<U,T> where U:IComparable
    {
        public U Key { get; set; }
        public T Value { get; set; }

        public Node<U, T> Left { get; set; }

        public Node<U, T> Right { get; set; }

        public Node(U key, T value)
        {
            Key = key;
            Value = value;
        }
    }

    public class BinarySearchTree<U, T> : IEnumerable<Node<U, T>>
        where U : IComparable
    {
        public Node<U, T> Root { get; private set; }
        public int Count { get; private set; }

        public BinarySearchTree(U key, T value)
        {
            Insert(key, value);
        }

        public void Insert(U key, T value)
        {
            Node<U, T> node = new Node<U, T>(key, value);
            if (Root == null)
            {
                Root = node;
                Count++;
            }
            else
            {
                Node<U, T> current = RecursiveDiving(Root, node.Key);
                if (current.Key.CompareTo(node.Key) == 0)
                    current.Value = node.Value;
                else if (node.Key.CompareTo(current.Key) < 0)
                {
                    current.Left = node;
                    Count++;
                }
                else
                {
                    current.Right = node;
                    Count++;
                }
            }
        }

        public Node<U, T> Find(U key)
        {
            if (Count == 0)
                return null;
            Node<U, T> current = RecursiveDiving(Root, key);
            if (current.Key.CompareTo(key) == 0)
                return current;
            else
                return null;
        }

        private Node<U, T> RecursiveDiving(Node<U, T> node, U key)
        {
            if (node.Key.CompareTo(key) == 0)
                return node;
            else if (key.CompareTo(node.Key) < 0)
            {
                if (node.Left == null)
                    return node;
                else
                    return RecursiveDiving(node.Left, key);
            }
            else
            {
                if (node.Right == null)
                    return node;
                else
                    return RecursiveDiving(node.Right, key);
            }
        }

        public void Remove(U key)
        {
            RecursiveRemove(Root, key);
        }

        private Node<U, T> RecursiveRemove(Node<U, T> current, U key)
        {
            if (current == null)
                return current;
            if (key.CompareTo(current.Key) < 0)
            {
                current.Left = RecursiveRemove(current.Left, key);
            }
            else if (key.CompareTo(current.Key) > 0)
            {
                current.Right = RecursiveRemove(current.Right, key);
            }
            else if (current.Left != null && current.Right != null)
            {
                Node<U, T> find = FindMinimumNode(current.Right);
                current.Key = find.Key;
                current.Value = find.Value;
                current.Right = RecursiveRemove(current.Right, current.Key);
            }
            else
            {
                if (current.Left != null)
                    current = current.Left;
                else
                    current = current.Right;
                Count--;
            }
            return current;
        }

        private Node<U, T> FindMinimumNode(Node<U, T> current)
        {
            while (current.Left != null)
                current = current.Left;
            return current;
        }

        public IEnumerator<Node<U, T>> GetEnumerator()
        {
            return NLR(Root).GetEnumerator();
        }

        public IEnumerable<Node<U, T>> NLR(Node<U, T> current)
        {
            if (current == null)
                yield break;
            yield return current;
            foreach (var item in NLR(current.Left))
                yield return item;
            foreach (var item in NLR(current.Right))
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
