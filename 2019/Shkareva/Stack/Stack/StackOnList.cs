namespace Stack
{
    public class StackOnList<T>: IStack<T>
    {
        private Node<T> head;
        public int Count { get; private set; }

        public void Push(T element)
        {
            var newNode = new Node<T>(element);
            newNode.Previous = head;
            head = newNode;
            Count++;
        }

        public T Pop()
        {
            if (IsEmpty())
            {
                throw new StackException("Stack is underflow");
            }
                        
            T returnValue = head.Value;
            head = head.Previous;
            Count--;
            return returnValue;

        }
        public T Peek()
        {
            if (IsEmpty())
            {
                throw new StackException("Stack is underflow");
            }

            return head.Value;
        }

        public bool IsEmpty()
        {
            return head == null;
        }
    }
}
