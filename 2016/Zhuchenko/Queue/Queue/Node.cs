using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue
{
    public class Node<T>
    {
        public T Value { set; get; }
        public Node<T> NextNode { set; get; }

        public Node(T value)
        {
            Value = value;
            NextNode = null;
        }
    }
}
