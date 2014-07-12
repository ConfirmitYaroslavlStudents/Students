using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forest
{
    public class Tree<T>
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
                if (value.GetHashCode() > tempNode.Value.GetHashCode())
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
                if (value.GetHashCode() < tempNode.Value.GetHashCode())
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
            Node<T> tempNode = _headNode;
            Node<T> previousNode = tempNode;
            bool right = false;
            Node<T> RightNodeAfterRemoteNode, LeftNodeAfterRemoteNode;
            if (value.GetHashCode() == tempNode.Value.GetHashCode() && Count != 1)
            {
                RightNodeAfterRemoteNode = tempNode.RightNode;
                LeftNodeAfterRemoteNode = tempNode.LeftNode;
                _headNode = RightNodeAfterRemoteNode;
                tempNode = GetMostLeftNode(RightNodeAfterRemoteNode);
                tempNode.LeftNode = LeftNodeAfterRemoteNode;
                Count--;
            }
            else
            {
                while (true)
                {
                    if (value.GetHashCode() > tempNode.Value.GetHashCode())
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
                    if (value.GetHashCode() < tempNode.Value.GetHashCode())
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
                RightNodeAfterRemoteNode = tempNode.RightNode;
                LeftNodeAfterRemoteNode = tempNode.LeftNode;

                if (RightNodeAfterRemoteNode == null && LeftNodeAfterRemoteNode != null)
                {
                    if (right)
                    {
                        previousNode.RightNode = LeftNodeAfterRemoteNode;
                    }
                    else
                    {
                        previousNode.LeftNode = LeftNodeAfterRemoteNode;
                    }
                }
                if (RightNodeAfterRemoteNode != null && LeftNodeAfterRemoteNode == null)
                {
                    if (right)
                    {
                        previousNode.RightNode = RightNodeAfterRemoteNode;
                    }
                    else
                    {
                        previousNode.LeftNode = RightNodeAfterRemoteNode;
                    }
                }
                if (RightNodeAfterRemoteNode == null && LeftNodeAfterRemoteNode == null)
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
                if (RightNodeAfterRemoteNode != null && LeftNodeAfterRemoteNode != null)
                {
                    if (right)
                    {
                        previousNode.RightNode = RightNodeAfterRemoteNode;
                    }
                    else
                    {
                        previousNode.LeftNode = RightNodeAfterRemoteNode;
                    }
                    tempNode = GetMostLeftNode(RightNodeAfterRemoteNode);
                    tempNode.LeftNode = LeftNodeAfterRemoteNode;
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
    }
}
