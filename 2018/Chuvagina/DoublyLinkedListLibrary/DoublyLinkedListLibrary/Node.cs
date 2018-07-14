using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedListLibrary
{
    public class Node
    {
        public string Value { get; set; }

        public Node(string Value)
        {
            this.Value = Value;
            Previous = null;
            Next = null;
        }

        public Node Previous { get; set; }

        public Node Next { get; set; }

    }
}
