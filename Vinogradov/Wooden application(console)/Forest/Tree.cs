using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forest
{
    public class Tree<T> where T:IComparable
    {
        public int Count { private set; get; }
        private Node<T> _headNode;

        #region constructors

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

        #endregion

        #region addition

        private void CreateFirstNode(T value)
        {
            _headNode = new Node<T>(value);
            Count = 1;
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

        private void FindAndAdd(T value)
        {
            Node<T> tempNode = _headNode;
            while (true)
            {
                if (value.CompareTo(tempNode.Value)>0)
                {
                    if (tempNode.RightNode == null)
                    {
                        tempNode.RightNode = new Node<T>(value);
                        Count++;
                        return;
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
                        tempNode.LeftNode = new Node<T>(value);
                        Count++;
                        return;
                    }
                    else
                    {
                        tempNode = tempNode.LeftNode;
                        continue;
                    }
                }
                return;
            }
        }

        #endregion

        #region removal

        public void Remove(T value)
        {
            if (_headNode == null)
            {
                throw new InvalidOperationException();
            }
            Node<T> tempNode = _headNode;
            Node<T> previousNode = tempNode;
            bool right = false;
            Node<T> rightNodeAfterRemoteNode, leftNodeAfterRemoteNode;
            if (value.GetHashCode() == tempNode.Value.GetHashCode() && Count != 1)
            {
                rightNodeAfterRemoteNode = tempNode.RightNode;
                leftNodeAfterRemoteNode = tempNode.LeftNode;
                _headNode = rightNodeAfterRemoteNode;
                tempNode = GetMostLeftNode(rightNodeAfterRemoteNode);
                tempNode.LeftNode = leftNodeAfterRemoteNode;
                Count--;
            }
            else
            {
                while (true)
                {
                    if (value.CompareTo(tempNode.Value) > 0)
                    {
                        if (tempNode.RightNode == null)
                        {
                            return;
                        }
                        else
                        {
                            previousNode = tempNode;
                            right = true;
                            tempNode = tempNode.RightNode;
                            continue;
                        }
                    }
                    if (value.CompareTo(tempNode.Value) < 0)
                    {
                        if (tempNode.LeftNode == null)
                        {
                            return;
                        }
                        else
                        {
                            previousNode = tempNode;
                            right = false;
                            tempNode = tempNode.LeftNode;
                            continue;
                        }
                    }
                    Count--;
                    break;
                }
                rightNodeAfterRemoteNode = tempNode.RightNode;
                leftNodeAfterRemoteNode = tempNode.LeftNode;

                if (rightNodeAfterRemoteNode == null && leftNodeAfterRemoteNode != null)
                {
                    if (right)
                    {
                        previousNode.RightNode = leftNodeAfterRemoteNode;
                    }
                    else
                    {
                        previousNode.LeftNode = leftNodeAfterRemoteNode;
                    }
                }
                if (rightNodeAfterRemoteNode != null && leftNodeAfterRemoteNode == null)
                {
                    if (right)
                    {
                        previousNode.RightNode = rightNodeAfterRemoteNode;
                    }
                    else
                    {
                        previousNode.LeftNode = rightNodeAfterRemoteNode;
                    }
                }
                if (rightNodeAfterRemoteNode == null && leftNodeAfterRemoteNode == null)
                {
                    if (Count == 1)
                    {
                        _headNode = null;
                    }
                    else
                    {
                        if (right)
                        {
                            previousNode.RightNode = null;
                        }
                        else
                        {
                            previousNode.LeftNode = null;
                        }
                    }
                }
                if (rightNodeAfterRemoteNode != null && leftNodeAfterRemoteNode != null)
                {
                    if (right)
                    {
                        previousNode.RightNode = rightNodeAfterRemoteNode;
                    }
                    else
                    {
                        previousNode.LeftNode = rightNodeAfterRemoteNode;
                    }
                    tempNode = GetMostLeftNode(rightNodeAfterRemoteNode);
                    tempNode.LeftNode = leftNodeAfterRemoteNode;
                }
            }
        }

        private Node<T> GetMostLeftNode(Node<T> node)
        {
            Node<T> tempNode = node;
            while (tempNode.LeftNode != null)
            {
                tempNode = tempNode.LeftNode;
            }
            return tempNode;
        }

        #endregion

        #region contains

        public bool Contains(T value)
        {
            if (_headNode == null)
            {
                throw new InvalidOperationException();
            }
            Node<T> tempNode = _headNode;
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

        #endregion
    }
}
