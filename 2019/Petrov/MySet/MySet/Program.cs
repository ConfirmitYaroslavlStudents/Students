using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySet
{
    class Program
    {
        internal class Node<T>
        {
            public T data { get; set; }
            public Node<T> leftChild { get; set; }
            public Node<T> rightChild { get; set; }
            public Node<T> parent { get; set; }
            public Node(T _data)
            {
                data = _data;
            }
        }
        class Set<T> where T : IComparable<T>
        {
            public Node<T> root;
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
            public int CompareTo(object obj) => throw new NotImplementedException();
            public Node<T> Find(T Data)
            {
                Node<T> node = root;
                while (node != null)
                {
                    if (node.data.Equals(Data))
                    {
                        return node;
                    }
                    node = Data.CompareTo(node.data) >= 0 ? node.rightChild : node.leftChild;
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
                if (newNode.parent == null)
                {
                    newNode.parent = root;
                }
                while (true)
                {
                    if (newNode.data.CompareTo(current.data) <= 0)
                    {
                        if (current.leftChild == null)
                        {
                            newNode.parent = current;
                            current.leftChild = newNode;
                            count++;
                            return true;
                        }
                        current = current.leftChild;
                    }
                    else
                    {
                        if (current.rightChild == null)
                        {
                            newNode.parent = current;
                            current.rightChild = newNode;
                            count++;
                            return true;
                        }
                        current = current.rightChild;
                    }
                }
            }
            public bool Remove(Node<T> removeNode)
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

                if (removeNode.leftChild == null && removeNode.rightChild == null)
                {
                    NodeHaveNoChildren(removeNode);
                    count--;
                }
                else if (removeNode.leftChild == null || removeNode.rightChild == null)
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
                if (removeNode.parent.leftChild == removeNode)
                    removeNode.parent.leftChild = null;
                if (removeNode.parent.rightChild == removeNode)
                    removeNode.parent.rightChild = null;
                removeNode.parent = null;
            }

            public void NodeHaveOneChild(Node<T> removeNode)
            {
                if (removeNode.leftChild == null)
                {
                    if (removeNode.parent.leftChild == removeNode)
                        removeNode.parent.leftChild = removeNode.rightChild;
                    else
                        removeNode.parent.rightChild = removeNode.rightChild;
                    removeNode.rightChild.parent = removeNode.parent;
                }
                else
                {
                    if (removeNode.parent.leftChild == removeNode)
                        removeNode.parent.leftChild = removeNode.leftChild;
                    else
                        removeNode.parent.rightChild = removeNode.leftChild;
                    removeNode.leftChild.parent = removeNode.parent;
                }
            }

            public void NodeHaveTwoChildren(Node<T> removeNode)
            {
                Node<T> minNode = minimumUnderTheTree(removeNode.rightChild);
                    removeNode.data = minNode.data;
                    Remove(minNode);
            }
            Node<T> nextNode(Node<T> _Node)
            {
                if (_Node.rightChild != null)
                    return minimumUnderTheTree(_Node.rightChild);
                Node<T> tmpNode = _Node.parent;
                while (tmpNode != null && _Node == tmpNode.rightChild)
                {
                    _Node = tmpNode;
                    tmpNode = tmpNode.parent;
                }
                return tmpNode;
            }
            Node<T> minimumUnderTheTree(Node<T> _Node)
            {
                if (_Node.leftChild == null)
                    return _Node;
                return minimumUnderTheTree(_Node.leftChild);
            }
            public void Unite(Set<T> _Set)
            {
                if(_Set != null)
                {
                    foreach (T i in _Set)
                    {
                        Add(i);
                    }
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
                    PrintInOrder(_Node.leftChild);
                    Console.WriteLine(_Node.data);
                    PrintInOrder(_Node.rightChild);
                }
            }
            public void PrintPreOrder(Node<T> _Node)
            {
                if (_Node != null)
                {
                    Console.WriteLine(_Node.data);
                    PrintInOrder(_Node.leftChild);
                    PrintInOrder(_Node.rightChild);
                }
            }
            public void PrintPostOrder(Node<T> _Node)
            {
                if (_Node != null)
                {
                    PrintInOrder(_Node.leftChild);
                    PrintInOrder(_Node.rightChild);
                    Console.WriteLine(_Node.data);
                }
            }
        }
        
        static void Main(string[] args)
        {
            Set<int> A = new Set<int>();
            A.Add(3);
            A.Add(2);
            A.Add(1);
            A.Add(4);
            A.Add(5);
            A.PrintInOrder(A.root);
            Console.WriteLine();
            A.PrintPostOrder(A.root);
            Console.WriteLine();
            A.PrintPreOrder(A.root);
            A.Remove(3);
            Console.WriteLine();
            A.PrintPreOrder(A.root);
            A.Clear();
            A.PrintInOrder(A.root);
        }
    }
}
