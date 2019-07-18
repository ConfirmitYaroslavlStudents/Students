namespace DataStructures
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> NextNode { get; internal set; }
        internal LinkedList<T> ParentList { get; private set; }
        public Node(T value, Node<T> nextNode = null, LinkedList<T> list = null)
        {
            Value = value;
            NextNode = nextNode;
            ParentList = list;
        }

    }
}
