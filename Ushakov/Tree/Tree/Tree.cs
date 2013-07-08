using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    class Node<T> where T: IComparable
    {
        public T Value { get; set; }
        public Node<T> Parent { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public Node(T value, Node<T> parent)
        {
            this.Value = value;
            Parent = parent;
            Left = null;
            Right = null;
        }
    }
    class Tree<T> where T:IComparable
    {
        Node<T> _root;

        public Tree()
        {
            _root = null;
        }

        public void Add(T element)
        {
            if (_root == null)
            {
                _root = new Node<T>(element, null);
            }
            else
            {
                AddNext(element, _root);
            }
        }

        private void AddNext(T element, Node<T> currentNode)
        {
            if (element.CompareTo(currentNode.Value) >= 0)
                if (currentNode.Right != null)
                    AddNext(element, currentNode.Right);
                else
                    currentNode.Right = new Node<T>(element, currentNode);
            else
                if (currentNode.Left != null)
                    AddNext(element, currentNode.Left);
                else
                    currentNode.Left = new Node<T>(element, currentNode);
        }

        public void Remove(T element)
        {
            Node<T> node = SearchElement(element, _root);
            if (node != null)
                DeleteNode(node);
            return;
        }

        private Node<T> SearchElement(T element, Node<T> currentNode)
        {
            if (currentNode.Value.CompareTo(element) == 0)
                return currentNode;
            if (element.CompareTo(currentNode.Value) > 0 && currentNode.Right != null)
                return SearchElement(element, currentNode.Right);
            if (element.CompareTo(currentNode.Value) < 0 && currentNode.Left != null)
                return SearchElement(element, currentNode.Left);
            
            return null;
        }

        private void DeleteNode(Node<T> node)
        {
            if (node.Parent == null)
            {
                if (node.Right != null)
                {
                    _root = node.Right;
                    node.Right.Parent = null;
                    AddLeft(node.Right, node.Left);
                }
                else
                {
                    _root = node.Left;
                    if (node.Left != null)
                        node.Left.Parent = null;
                }
            }
            else
            {
                if (node.Parent.Left == node)
                {
                    if (node.Right != null)
                    {
                        node.Parent.Left = node.Right;
                        node.Right.Parent = node.Parent;
                        AddLeft(node.Right, node.Left);
                    }
                    else
                    {
                        node.Parent.Left = node.Left;
                        if (node.Left != null)
                            node.Left.Parent = node.Parent;
                    }
                }
                else
                {
                    if (node.Right != null)
                    {
                        node.Parent.Right = node.Right;
                        node.Right.Parent = node.Parent;
                        AddLeft(node.Right, node.Left);
                    }
                    else
                    {
                        node.Parent.Right = node.Left;
                        if (node.Left != null)
                            node.Left.Parent = node.Parent;
                    }
                }
            } 
        }

        private void AddLeft(Node<T> node, Node<T> left)
        {
            while (node.Left != null)
                node = node.Left;
            node.Left = left;
            if (left != null)
                node.Left.Parent = node;
        }

        public List<T> DirectTraversing()
        {
            if (_root != null)
                return DirectTraversing(new List<T>(), _root);
            else
                return new List<T>();
        }

        private List<T> DirectTraversing(List<T> valueList, Node<T> currentNode)
        {
            valueList.Add(currentNode.Value);
            if (currentNode.Left != null)
                DirectTraversing(valueList, currentNode.Left);
            if (currentNode.Right != null)
                DirectTraversing(valueList, currentNode.Right);
            
            return valueList;
        }

        public List<T> ReverseTraversing()
        {
            if (_root != null)
                return ReverseTraversing(new List<T>(), _root);
            else
                return new List<T>();
        }

        private List<T> ReverseTraversing(List<T> valueList, Node<T> currentNode)
        {
            if (currentNode.Left != null)
                ReverseTraversing(valueList, currentNode.Left);
            if (currentNode.Right != null)
                ReverseTraversing(valueList, currentNode.Right);
            valueList.Add(currentNode.Value);
            
            return valueList;
        }

        public List<T> SymmetricTraversing()
        {
            if (_root != null)
                return SymmetricTraversing(new List<T>(), _root);
            else
                return new List<T>();
        }

        private List<T> SymmetricTraversing(List<T> valueList, Node<T> currentNode)
        {
            if (currentNode.Left != null)
                SymmetricTraversing(valueList, currentNode.Left);
            valueList.Add(currentNode.Value);
            if (currentNode.Right != null)
                SymmetricTraversing(valueList, currentNode.Right);
            
            return valueList;
        }
    }
}
