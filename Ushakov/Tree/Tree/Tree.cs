using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    class Tree<T>:IEnumerable<T> where T:IComparable
    {
        private class Node<T> where T : IComparable
        {
            public T Value { get; set; }
            public Node<T> Left { get; set; }
            public Node<T> Right { get; set; }
            public int Height { get; private set; }

            public Node(T value)
            {
                this.Value = value;
                Left = null;
                Right = null;
                Height = 1;
            }

            public void SetHeight()
            {
                if (Left == null && Right == null)
                {
                    Height = 1;
                    return;
                }
                if (Left == null)
                {
                    Height = Right.Height+1;
                    return;
                }
                if (Right == null)
                {
                    Height = Left.Height+1;
                    return;
                }
                Height = Math.Max(Left.Height, Right.Height)+1;
            }

            public int GetBalanceFactor()
            {
                return (Right != null ? Right.Height : 0) -
                    (Left != null ? Left.Height : 0);
            }

        }

        Node<T> _root;

        public Tree()
        {
            _root = null;
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

        Node<T> FindMin(Node<T> node) 
        {
            return (node.Left != null) ? FindMin(node.Left) : node;
        }

        Node<T> RemoveMin(Node<T> node)
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

        #region traversings
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
        #endregion

        public IEnumerator<T> GetEnumerator()
        {
            return DirectTraversing().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return DirectTraversing().GetEnumerator();
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

        //убрать потом!!!
        public void PrintTree()
        {
            Print(_root, 0);
        }

        private void Print(Node<T> node, int level)
        {
            if (node != null)
            {
                Print(node.Left, level + 1);
                for (int i = 0; i < level; i++)
                    Console.Write("\t");
                Console.WriteLine(node.Value.ToString());
                Print(node.Right, level + 1);
            }
        }
    }
}
