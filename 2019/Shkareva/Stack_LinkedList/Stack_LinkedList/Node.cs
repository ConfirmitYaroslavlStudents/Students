namespace Stack_LinkedList
{
    internal class Node<T>
    {
        public T Value;
        public Node<T> Previous;

        public Node(T value)
        {
            Value = value;
            Previous = null;
        }
    }
}
