using System;

namespace LinkedList
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> NextNode { get; internal set; }
        public LinkedList<T> ParentList { get; private set; }
        public Node(T value, Node<T> nextNode = null, LinkedList<T> list = null)
        {
            Value = value;
            NextNode = nextNode;
            ParentList = list;
        }

        public static bool BelongsToList(Node<T> node, LinkedList<T> list)
        {
            return node != null && object.ReferenceEquals(node.ParentList, list);
        }
    }
}
