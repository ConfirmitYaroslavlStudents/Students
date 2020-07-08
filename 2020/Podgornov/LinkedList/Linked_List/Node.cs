
namespace Linked_List
{
    public class Node<T>
    {

        public Node<T> Next { get; internal set; }

        public Node<T> Previous { get; internal set; }

        public T Value { get; set; }

        public Node(T value)
        {
            Value = value;
        }

    }
}
