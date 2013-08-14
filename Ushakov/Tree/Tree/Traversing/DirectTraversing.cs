using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    public class DirectTraversing<T> : Traversing<T> where T : IComparable
    {
        protected override List<T> Traverse(List<T> valueList, Node<T> currentNode)
        {
            valueList.Add(currentNode.Value);
            if (currentNode.Left != null)
                Traverse(valueList, currentNode.Left);
            if (currentNode.Right != null)
                Traverse(valueList, currentNode.Right);

            return valueList;
        }
    }
}
