using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    public abstract class Traversing<T> where T : IComparable
    {
        public List<T> Traverse(Node<T> node)
        {
            if (node != null)
                return Traverse(new List<T>(), node);
            else
                return new List<T>();
        }
        
        protected abstract List<T> Traverse(List<T> valueList, Node<T> currentNode);
    }
}
