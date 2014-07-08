using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    public class ReverseTraversing<T> : Traversing<T> where T : IComparable
    {
        protected override List<T> Traverse(List<T> valueList, Node<T> currentNode)
        {
            if (currentNode.Left != null)
                Traverse(valueList, currentNode.Left);
            if (currentNode.Right != null)
                Traverse(valueList, currentNode.Right);
            valueList.Add(currentNode.Value);

            return valueList;
        }
    }
}
