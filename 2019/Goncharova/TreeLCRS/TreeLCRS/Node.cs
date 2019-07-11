using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeLCRS
{
    public class Node<T>
    {
        internal Node<T> Child { get; set; }
        internal Node<T> Next { get; set; }

        internal T Data { get; private set; }

        public Node(T data)
        {
            Data = data;
            Next = Child = null;
        }

    }
}
