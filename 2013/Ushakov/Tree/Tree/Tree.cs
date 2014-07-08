using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    public class Tree<T> : IEnumerable<T> where T : IComparable
    {
        private Node<T> _root;

        public Traversing<T> Traversing { get; set; }

        public Tree()
        {
            _root = null;
            Traversing = new DirectTraversing<T>();
        }

        public void Add(T element)
        {
            _root = Add(element, _root);
        }

        private Node<T> Add(T element, Node<T> currentNode)
        {
            if (currentNode == null)
                return new Node<T>(element);
            if (currentNode.Value.CompareTo(element) < 0)
                currentNode.Right = Add(element, currentNode.Right);
            else
                currentNode.Left = Add(element, currentNode.Left);

            currentNode.SetHeight();
            return BalanceTree(currentNode);
        }

        public void Remove(T element)
        {
            _root = DeleteNode(element, _root);
            if (_root != null)
                _root = BalanceTree(_root);
        }

        private Node<T> FindMin(Node<T> node)
        {
            return (node.Left != null) ? FindMin(node.Left) : node;
        }

        private Node<T> RemoveMin(Node<T> node)
        {
            if (node.Left == null)
                return node.Right;
            node.Left = RemoveMin(node.Left);

            node.SetHeight();
            return BalanceTree(node);
        }

        private Node<T> DeleteNode(T element, Node<T> node)
        {
            if (node == null)
                return null;
            if (element.CompareTo(node.Value) > 0)
                node.Right = DeleteNode(element, node.Right);
            else
                if (element.CompareTo(node.Value) < 0)
                    node.Left = DeleteNode(element, node.Left);
                else
                {
                    if (node.Right == null)
                        return node.Left;
                    Node<T> min = FindMin(node.Right);
                    min.Right = RemoveMin(node.Right);
                    min.Left = node.Left;
                    return BalanceTree(min);
                }

            node.SetHeight();
            return BalanceTree(node);
        }

        private Node<T> RotateRight(Node<T> node)
        {
            Node<T> left = node.Left;
            node.Left = left.Right;
            left.Right = node;
            node.SetHeight();
            left.SetHeight();
            return left;
        }

        private Node<T> RotateLeft(Node<T> node)
        {
            Node<T> right = node.Right;
            node.Right = right.Left;
            right.Left = node;
            node.SetHeight();
            right.SetHeight();
            return right;
        }

        private Node<T> BalanceTree(Node<T> node)
        {
            if (node.GetBalanceFactor() >= 2)
            {
                if (node.Right.GetBalanceFactor() < 0)
                    node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            if (node.GetBalanceFactor() <= -2)
            {
                if (node.Left.GetBalanceFactor() > 0)
                    node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            return node;
        }


        public List<T> Traverse()
        {
            return Traversing.Traverse(_root);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Traverse().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Traverse().GetEnumerator();
        }
    }
}
