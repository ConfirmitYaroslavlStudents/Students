using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    interface ITraversing<T> where T : IComparable
    {
        List<T> Traverse(Node<T> node);
    }

    public class DirectTraversing<T> : ITraversing<T> where T : IComparable
    {
        List<T> ITraversing<T>.Traverse(Node<T> node)
        {
            if (node != null)
                return DirectTraverse(new List<T>(), node);
            else
                return new List<T>();
        }

        private List<T> DirectTraverse(List<T> valueList, Node<T> currentNode)
        {
            valueList.Add(currentNode.Value);
            if (currentNode.Left != null)
                DirectTraverse(valueList, currentNode.Left);
            if (currentNode.Right != null)
                DirectTraverse(valueList, currentNode.Right);

            return valueList;
        }
    }

    public class ReverseTraversing<T> : ITraversing<T> where T : IComparable
    {
        List<T> ITraversing<T>.Traverse(Node<T> node)
        {
            if (node != null)
                return ReverseTraverse(new List<T>(), node);
            else
                return new List<T>();
        }

        private List<T> ReverseTraverse(List<T> valueList, Node<T> currentNode)
        {
            if (currentNode.Left != null)
                ReverseTraverse(valueList, currentNode.Left);
            if (currentNode.Right != null)
                ReverseTraverse(valueList, currentNode.Right);
            valueList.Add(currentNode.Value);

            return valueList;
        }
    }

    public class SymmetricTraversing<T> : ITraversing<T> where T : IComparable
    {
        List<T> ITraversing<T>.Traverse(Node<T> node)
        {
            if (node != null)
                return SymmetricTraverse(new List<T>(), node);
            else
                return new List<T>();
        }

        private List<T> SymmetricTraverse(List<T> valueList, Node<T> currentNode)
        {
            if (currentNode.Left != null)
                SymmetricTraverse(valueList, currentNode.Left);
            valueList.Add(currentNode.Value);
            if (currentNode.Right != null)
                SymmetricTraverse(valueList, currentNode.Right);

            return valueList;
        }
    }
}
