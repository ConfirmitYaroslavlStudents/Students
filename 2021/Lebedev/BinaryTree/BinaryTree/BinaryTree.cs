using System;

namespace BinaryTree
{
    public class BinaryTree<T> where T : IComparable
    {
        private class Node
        {
            public T Value
            {
                get;
                set;
            }

            public Node Right
            {
                get;
                set;
            }

            public Node Left
            {
                get;
                set;
            }

            public Node(T item)
            {
                Value = item;
            }

            public Node(T item, Node right, Node left)
            {
                Value = item;
                this.Right = right;
                this.Left = left;
            }
        }

        Node root;

        public void Add(T item)
        {
            if (root == null)
            {
                root = new Node(item);
                return;
            }
            Node now = root;

            while (true)
            {
                switch (item.CompareTo(now.Value))
                {
                    case 0:
                        return;
                    case 1:
                        if (now.Right == null)
                        {
                            now.Right = new Node(item);
                            return;
                        }
                        now = now.Right;
                        break;
                    case -1:
                        if (now.Left == null)
                        {
                            now.Left = new Node(item);
                            return;
                        }
                        now = now.Left;
                        break;
                }
            }
        }

        public bool Contains(T item)
        {
            var now = root;
            while (now != null)
            {
                switch (item.CompareTo(now.Value))
                {
                    case 0:
                        return true;
                    case 1:
                        now = now.Right;
                        break;
                    case -1:
                        now = now.Left;
                        break;
                }
            }
            return false;
        }

        public void Remove(T item)
        {
            Node prev = null;
            bool flag = false;
            var now = root;
            while (now != null)
            {
                switch (item.CompareTo(now.Value))
                {
                    case 0:
                        if (now.Left == null)
                        {
                            if (now.Right == null)
                            {
                                if (prev == null)
                                {
                                    root = null;
                                    return;
                                }
                                if (flag)
                                    prev.Right = null;
                                else
                                    prev.Left = null;
                                return;
                            }
                            if (prev == null)
                            {
                                root = root.Right;
                                return;
                            }
                            if (flag)
                                prev.Right = now.Right;
                            else
                                prev.Left = now.Right;
                            return;
                        }
                        if (now.Right == null)
                        {
                            if (prev == null)
                            {
                                root = null;
                                return;
                            }
                            if (flag)
                                prev.Right = null;
                            else
                                prev.Left = null;
                            return;
                        }

                        Node prev1 = null;
                        var closest = now.Right;
                        while (closest.Left != null)
                        {
                            prev1 = closest;
                            closest = closest.Left;
                        }
                        now.Value = closest.Value;
                        if (prev1 == null)
                        {
                            now.Right = closest.Right;
                            return;
                        }
                        if (closest.Right != null)
                        {
                            prev1.Left = closest.Right;
                        }
                        return;
                    case 1:
                        prev = now;
                        flag = true;
                        now = now.Right;
                        break;
                    case -1:
                        prev = now;
                        flag = false;
                        now = now.Left;
                        break;
                }
            }
            return;
        }
    }
}