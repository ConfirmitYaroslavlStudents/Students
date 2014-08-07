using System;
using System.Collections.Generic;
using System.Linq;

namespace Forest
{
    //Order of terms: public and then private( e.g. methods in your case), rename methods(e.g. Prefix -> GetPrefix)
    public class Tree<T> where T : IComparable
    {
        public int Count { private set; get; }
        private Node<T> _headNode;
        private Action<Node<T>> _actionWithNode;

        public Tree()
        {
            //unnecessary code
            Count = 0;
            _headNode = null;
        }

        //Rename list -> ?
        public Tree(IEnumerable<T> list)
        {
            //!list.Any() is unnecessary, throw new ArgumentNullException("list")
            if (list == null || !list.Any())
            {
                Count = 0; //unnecessary
                return;
            }
            foreach (var value in list)
            {
                Add(value);
            }
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

        //remove duplicate code(add AddRange in ctor)
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
            if (_headNode == null)
            {
                throw new InvalidOperationException();
            }
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
                        targetNode.Parent.RightNode=TransformChildren(targetNode);
                    }
                    else
                    {
                        targetNode.Parent.LeftNode = TransformChildren(targetNode);
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
            if (targetNode.Parent != null)
            {
                var parent = targetNode.Parent;

                //return (targetNode.Value.CompareTo(parent.Value) > 0)
                if (targetNode.Value.CompareTo(parent.Value) > 0)
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
                throw new Exception("targetNode.Parent == null");
            }
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

        //Rename
        public void Horizontal(Action<Node<T>> actionWithNode)
        {
            _actionWithNode = actionWithNode;
            RecursionForHorizontal(_headNode);
        }

        //Rename
        private void RecursionForHorizontal(Node<T> currentNode)
        {
            var queueOfNodes= new Queue<Node<T>>();
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

        //rename(should be verb)
        public void Prefix(Action<Node<T>> actionWithNode)
        {
            _actionWithNode = actionWithNode;
            RecursionForPrefix(_headNode);
        }

        private void RecursionForPrefix(Node<T> currentNode)
        {
            _actionWithNode.Invoke(currentNode);
            if (currentNode.LeftNode != null)
            {
                RecursionForPrefix(currentNode.LeftNode);
            }
            if (currentNode.RightNode != null)
            {
                RecursionForPrefix(currentNode.RightNode);
            }
        }

        public void Infix(Action<Node<T>> actionWithNode)
        {
            _actionWithNode = actionWithNode;
            RecursionForInfix(_headNode);
        }
        private void RecursionForInfix(Node<T> currentNode)
        {
            if (currentNode.LeftNode != null)
            {
                RecursionForInfix(currentNode.LeftNode);
            }
            _actionWithNode.Invoke(currentNode);
            if (currentNode.RightNode != null)
            {
                RecursionForInfix(currentNode.RightNode);
            }
        }

        public void Postfix(Action<Node<T>> actionWithNode)
        {
            _actionWithNode = actionWithNode;
            RecursionForPostfix(_headNode);
        }
        private void RecursionForPostfix(Node<T> currentNode)
        {
            if (currentNode.LeftNode != null)
            {
                RecursionForPostfix(currentNode.LeftNode);
            }
            if (currentNode.RightNode != null)
            {
                RecursionForPostfix(currentNode.RightNode);
            }
            _actionWithNode.Invoke(currentNode);
        }
    }
}