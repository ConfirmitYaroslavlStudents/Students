using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Forest
{
    public class Tree<T> where T : IComparable
    {
        public int Count { private set; get; }
        private Node<T> _headNode;
        //=========================================================================
        public Tree()
        {
            Count = 0;
            _headNode = null;
        }
        public Tree(IEnumerable<T> list)
        {
            if (list == null || !list.Any())
            {
                Count = 0;
                return;
            }
            foreach (var value in list)
            {
                Add(value);
            }
        }
        //=========================================================================
        public void Add(T value)
        {
            if (_headNode == null)
            {
                CreateFirstNode(value);
            }
            else
            {
                FindAndAdd(value);
            }
        }
        public void AddRange(IEnumerable<T> list)
        {
            if (list == null || !list.Any())
            {
                return;
            }
            foreach (var value in list)
            {
                Add(value);
            }
        }
        private void CreateFirstNode(T value)
        {
            _headNode = new Node<T>(value, null);
            Count = 1;
        }
        private void FindAndAdd(T value)
        {
            var tempNode = _headNode;
            var bugaga = Search(value, ref tempNode);
            if (!bugaga)
            {
                if (value.CompareTo(tempNode.Value) > 0)
                {
                    tempNode.RightNode = new Node<T>(value, tempNode);
                }
                else
                {
                    tempNode.LeftNode = new Node<T>(value, tempNode);
                }
                ++Count;
            }
        }
        //========================================================================
        public void Remove(T value)
        {
            if (_headNode == null)
            {
                throw new InvalidOperationException();
            }
            var tempNode = _headNode;
            var bugaga = Search(value, ref tempNode);
            if (bugaga)
            {
                --Count;
                Node<T> trueDirectionNode;
                if (tempNode.Parent == null)
                {
                    _headNode = TransformChildren(tempNode);
                }
                else
                {
                    var right = FindDirectionDesiredChild(tempNode);
                    if (right)
                    {
                        tempNode.Parent.RightNode=TransformChildren(tempNode);
                    }
                    else
                    {
                        tempNode.Parent.LeftNode = TransformChildren(tempNode);
                    }
                }
            }
        }
        private Node<T> TransformChildren(Node<T> tempNode)
        {
            var rightNodeAfterRemoteNode = tempNode.RightNode;
            var leftNodeAfterRemoteNode = tempNode.LeftNode;

            if (rightNodeAfterRemoteNode == null && leftNodeAfterRemoteNode != null)
            {
                return leftNodeAfterRemoteNode;
            }
            if (rightNodeAfterRemoteNode != null && leftNodeAfterRemoteNode == null)
            {
                return rightNodeAfterRemoteNode;
            }
            if (rightNodeAfterRemoteNode == null && leftNodeAfterRemoteNode == null)
            {
                return null;
            }
            if (rightNodeAfterRemoteNode != null && leftNodeAfterRemoteNode != null)
            {
                tempNode = GetMostLeftNode(rightNodeAfterRemoteNode);
                tempNode.LeftNode = leftNodeAfterRemoteNode;
                return rightNodeAfterRemoteNode;
            }
            return null;
        }
        private bool FindDirectionDesiredChild(Node<T> tempNode)
        {
            if (tempNode.Parent != null)
            {
                var parent = tempNode.Parent;
                if (tempNode.Value.CompareTo(parent.Value) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new Exception("tempNode.Parent == null");
            }
        }
        private Node<T> GetMostLeftNode(Node<T> node)
        {
            var tempNode = node;
            if (tempNode != null)
            {
                while (tempNode.LeftNode != null)
                {
                    tempNode = tempNode.LeftNode;
                }
            }
            return tempNode;
        }
        //========================================================================
        private bool Search(T value, ref Node<T> tempNode)
        {
            while (true)
            {
                if (value.CompareTo(tempNode.Value) > 0)
                {
                    if (tempNode.RightNode == null)
                    {
                        return false;
                    }
                    else
                    {
                        tempNode = tempNode.RightNode;
                        continue;
                    }
                }
                if (value.CompareTo(tempNode.Value) < 0)
                {
                    if (tempNode.LeftNode == null)
                    {
                        return false;
                    }
                    else
                    {
                        tempNode = tempNode.LeftNode;
                        continue;
                    }
                }
                return true;
            }
        }
        //========================================================================
        public bool Contains(T value)
        {
            if (_headNode == null)
            {
                throw new InvalidOperationException();
            }
            var tempNode = _headNode;
            return Search(value, ref tempNode);
        }
        //========================================================================
        public void Horizontal()
        {
            RecursionForHorizontal(_headNode);
            Console.ReadLine();
        }
        private void RecursionForHorizontal(Node<T> tempNode)
        {
            var queueOfNodes= new Queue<Node<T>>();
            do
            {
                Console.Write(tempNode.Value.ToString() + " ");
                if (tempNode.LeftNode != null)
                {
                    queueOfNodes.Enqueue(tempNode.LeftNode);
                }
                if (tempNode.RightNode != null)
                {
                    queueOfNodes.Enqueue(tempNode.RightNode);
                }
                if (queueOfNodes.Count()!=0)
                {
                    tempNode = queueOfNodes.Dequeue();
                    if (queueOfNodes.Count == 0)
                    {
                        Console.Write(tempNode.Value.ToString() + " ");
                    }
                }
            } while (queueOfNodes.Count() != 0);
        }

        public void Prefix()
        {
            RecursionForPrefix(_headNode);
            Console.ReadLine();
        }
        private void RecursionForPrefix(Node<T> tempNode)
        {
            Console.Write(tempNode.Value.ToString() + " ");
            if (tempNode.LeftNode != null)
            {
                RecursionForPrefix(tempNode.LeftNode);
            }
            if (tempNode.RightNode != null)
            {
                RecursionForPrefix(tempNode.RightNode);
            }
        }

        public void Infix()
        {
            RecursionForInfix(_headNode);
            Console.ReadLine();
        }
        private void RecursionForInfix(Node<T> tempNode)
        {
            if (tempNode.LeftNode != null)
            {
                RecursionForInfix(tempNode.LeftNode);
            }
            Console.Write(tempNode.Value.ToString() + " ");
            if (tempNode.RightNode != null)
            {
                RecursionForInfix(tempNode.RightNode);
            }
        }

        public void Postfix()
        {
            RecursionForPostfix(_headNode);
            Console.ReadLine();
        }
        private void RecursionForPostfix(Node<T> tempNode)
        {
            if (tempNode.LeftNode != null)
            {
                RecursionForPostfix(tempNode.LeftNode);
            }
            if (tempNode.RightNode != null)
            {
                RecursionForPostfix(tempNode.RightNode);
            }
            Console.Write(tempNode.Value.ToString() + " ");
        }
    }
}