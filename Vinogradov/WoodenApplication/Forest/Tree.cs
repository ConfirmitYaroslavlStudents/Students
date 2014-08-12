using System;
using System.Collections.Generic;
using System.Linq;

namespace Forest
{
    public class Tree<T> where T : IComparable
    {
        public int Count { get; private set; }
        private Node<T> _headNode;
        private Action<Node<T>> _actionWithNode;

        public Tree()
        {

        }
        public Tree(IEnumerable<T> collection)
        {
            AddRange(collection);
        }

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
        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                return;
            }
            foreach (var value in collection)
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
            var targetNode = _headNode;
            var contains = Search(value, ref targetNode);
            if (!contains)
            {
                if (value.CompareTo(targetNode.Value) > 0)
                {
                    targetNode.RightNode = new Node<T>(value, targetNode);
                }
                else
                {
                    targetNode.LeftNode = new Node<T>(value, targetNode);
                }
                ++Count;
            }
        }

        public void Remove(T value)
        {
            if (_headNode != null)
            {
                var targetNode = _headNode;
                var contains = Search(value, ref targetNode);
                if (contains)
                {
                    --Count;
                    if (targetNode.Parent == null)
                    {
                        _headNode = TransformChildren(targetNode);
                    }
                    else
                    {
                        var right = FindDirectionDesiredChild(targetNode);
                        if (right)
                        {
                            targetNode.Parent.RightNode = TransformChildren(targetNode);
                        }
                        else
                        {
                            targetNode.Parent.LeftNode = TransformChildren(targetNode);
                        }
                    }
                }
            }
        }

        private Node<T> TransformChildren(Node<T> targetNode)
        {
            var rightNodeAfterRemoteNode = targetNode.RightNode;
            var leftNodeAfterRemoteNode = targetNode.LeftNode;

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
                targetNode = GetMostLeftNode(rightNodeAfterRemoteNode);
                targetNode.LeftNode = leftNodeAfterRemoteNode;
                return rightNodeAfterRemoteNode;
            }
            return null;
        }

        private bool FindDirectionDesiredChild(Node<T> targetNode)
        {
            if (targetNode.Parent == null) throw new Exception("targetNode.Parent == null");
            var parent = targetNode.Parent;
            return targetNode.Value.CompareTo(parent.Value) > 0;
        }

        private Node<T> GetMostLeftNode(Node<T> node)
        {
            var mostLeftNode = node;
            if (mostLeftNode != null)
            {
                while (mostLeftNode.LeftNode != null)
                {
                    mostLeftNode = mostLeftNode.LeftNode;
                }
            }
            return mostLeftNode;
        }

        private bool Search(T value, ref Node<T> currentNode)
        {
            while (true)
            {
                if (value.CompareTo(currentNode.Value) > 0)
                {
                    if (currentNode.RightNode == null)
                    {
                        return false;
                    }
                    else
                    {
                        currentNode = currentNode.RightNode;
                        continue;
                    }
                }
                if (value.CompareTo(currentNode.Value) < 0)
                {
                    if (currentNode.LeftNode == null)
                    {
                        return false;
                    }
                    else
                    {
                        currentNode = currentNode.LeftNode;
                        continue;
                    }
                }
                return true;
            }
        }

        public bool Contains(T value)
        {
            if (_headNode == null)
            {
                throw new InvalidOperationException();
            }
            var targetode = _headNode;
            return Search(value, ref targetode);
        }

        public void MakeHorizontalBypass(Action<Node<T>> actionWithNode)
        {
            _actionWithNode = actionWithNode;
            DipThroughRecursionForHorizontal(_headNode);
        }
        private void DipThroughRecursionForHorizontal(Node<T> currentNode)
        {
            var queueOfNodes = new Queue<Node<T>>();
            for (int i = 0; i < Count; i++)
            {
                _actionWithNode.Invoke(currentNode);
                if (currentNode.LeftNode != null)
                {
                    queueOfNodes.Enqueue(currentNode.LeftNode);
                }
                if (currentNode.RightNode != null)
                {
                    queueOfNodes.Enqueue(currentNode.RightNode);
                }
                if (queueOfNodes.Count() != 0)
                {
                    currentNode = queueOfNodes.Dequeue();
                }
            }
        }

        public void MakePrefixBypass(Action<Node<T>> actionWithNode)
        {
            _actionWithNode = actionWithNode;
            DipThroughRecursionForPrefix(_headNode);
        }
        private void DipThroughRecursionForPrefix(Node<T> currentNode)
        {
            _actionWithNode.Invoke(currentNode);
            if (currentNode.LeftNode != null)
            {
                DipThroughRecursionForPrefix(currentNode.LeftNode);
            }
            if (currentNode.RightNode != null)
            {
                DipThroughRecursionForPrefix(currentNode.RightNode);
            }
        }

        public void MakeInfixBypass(Action<Node<T>> actionWithNode)
        {
            _actionWithNode = actionWithNode;
            DipThroughRecursionForInfix(_headNode);
        }
        private void DipThroughRecursionForInfix(Node<T> currentNode)
        {
            if (currentNode.LeftNode != null)
            {
                DipThroughRecursionForInfix(currentNode.LeftNode);
            }
            _actionWithNode.Invoke(currentNode);
            if (currentNode.RightNode != null)
            {
                DipThroughRecursionForInfix(currentNode.RightNode);
            }
        }

        public void MakePostfixBypass(Action<Node<T>> actionWithNode)
        {
            _actionWithNode = actionWithNode;
            DipThroughRecursionForPostfix(_headNode);
        }
        private void DipThroughRecursionForPostfix(Node<T> currentNode)
        {
            if (currentNode.LeftNode != null)
            {
                DipThroughRecursionForPostfix(currentNode.LeftNode);
            }
            if (currentNode.RightNode != null)
            {
                DipThroughRecursionForPostfix(currentNode.RightNode);
            }
            _actionWithNode.Invoke(currentNode);
        }
    }
}