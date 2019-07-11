using System;
using System.Collections;

namespace MySet
{
    // TODO: ClassLibrary project
    // TODO: PrintXXXOrder = implementation details
    // TODO: public count
    // TODO: implement IEnumerable<T>
    // TODO: improve incapsulation
    // TODO: improve naming in code
    // TODO: remove all unused code
    // TODO: union + intersection return new set
    // TODO: unit tests
    class Program
    {
        
        class Set<T> where T : IComparable<T>
        {
            internal class Node<V>
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
            int count;
            public Set()
            {
                root = null;
                count = 0;
            }
            public IEnumerator Enumerator { get; set; }
            public IEnumerator GetEnumerator()
            {
                return Enumerator;
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
            public bool Add(T Data)
            {
                if (Find(Data) != null)
                {
                    return false;
                }
                Node<T> newNode = new Node<T>(Data);
                if (root == null)
                {
                    root = newNode;
                    count++;
                    return true;
                }
                Node<T> current = root;
                if (newNode.Parent == null)
                {
                    newNode.Parent = root;
                }
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
            private bool Remove(Node<T> removeNode)
            {
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
                    NodeHaveNoChildren(removeNode);
                    count--;
                }
                else if (removeNode.LeftChild == null || removeNode.RightChild == null)
                {
                    NodeHaveOneChild(removeNode);
                    count--;
                }
                else
                {
                    NodeHaveTwoChildren(removeNode);
                    count--;
                }

                return true;
            }
            public bool Remove(T data)
            {
                Node<T> tmp = Find(data);
                return Remove(tmp);
            }
            public void NodeHaveNoChildren(Node<T> removeNode)
            {
                if (removeNode.Parent.LeftChild == removeNode)
                    removeNode.Parent.LeftChild = null;
                if (removeNode.Parent.RightChild == removeNode)
                    removeNode.Parent.RightChild = null;
                removeNode.Parent = null;
            }

            public void NodeHaveOneChild(Node<T> removeNode)
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

            public void NodeHaveTwoChildren(Node<T> removeNode)
            {
                Node<T> minNode = minimumUnderTheTree(removeNode.RightChild);
                    removeNode.Data = minNode.Data;
                    Remove(minNode);
            }
            Node<T> NextNode(Node<T> _Node)
            {
                if (_Node.RightChild != null)
                    return minimumUnderTheTree(_Node.RightChild);
                Node<T> tmpNode = _Node.Parent;
                while (tmpNode != null && _Node == tmpNode.RightChild)
                {
                    _Node = tmpNode;
                    tmpNode = tmpNode.Parent;
                }
                return tmpNode;
            }
            Node<T> minimumUnderTheTree(Node<T> _Node)
            {
                if (_Node.LeftChild == null)
                    return _Node;
                return minimumUnderTheTree(_Node.LeftChild);
            }
            public void Unite(Set<T> set)
            {
                if (set == null)
                    return;

                foreach (T i in set)
                {
                    Add(i);
                }
            }
            public void Intersection(Set<T> _Set)
            {
                if(_Set!=null)
                {
                    foreach(T i in _Set)
                    {
                        Node<T> tmpNode = Find(i);
                        if (tmpNode != null)
                            Remove(tmpNode);
                    }
                }
            }
            public void Clear()
            {
                root = null;
                count = 0;
            }
            public void PrintInOrder(Node<T> _Node)
            {
                if(_Node!=null)
                {
                    PrintInOrder(_Node.LeftChild);
                    Console.WriteLine(_Node.Data);
                    PrintInOrder(_Node.RightChild);
                }
            }
            public void PrintPreOrder(Node<T> _Node)
            {
                if (_Node != null)
                {
                    Console.WriteLine(_Node.Data);
                    PrintInOrder(_Node.LeftChild);
                    PrintInOrder(_Node.RightChild);
                }
            }
            public void PrintPostOrder(Node<T> _Node)
            {
                if (_Node != null)
                {
                    PrintInOrder(_Node.LeftChild);
                    PrintInOrder(_Node.RightChild);
                    Console.WriteLine(_Node.Data);
                }
            }
        }
        
        static void Main(string[] args)
        {
            Set<int> a = new Set<int>();
            a.Add(3);
            a.Add(2);
            a.Add(1);
            a.Add(4);
            a.Add(5);
            foreach(var x in a)
            {
                Console.WriteLine(x);
            }
            Console.ReadLine();
            //a.PrintInOrder(a.root);
            //Console.WriteLine();
            //a.PrintPostOrder(a.root);
            //Console.WriteLine();
            //a.PrintPreOrder(a.root);
            a.Remove(3);
            //Console.WriteLine();
            //a.PrintPreOrder(a.root);
            //a.Clear();
            //a.PrintInOrder(a.root);
        }
    }
}
