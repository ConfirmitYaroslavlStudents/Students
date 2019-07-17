namespace Stack_based_on_array
{
    public class Stack<T>
    {
        private T[] values;
        private int capacity;
        public int Count { get; private set; }

        public Stack(int Capacity)
        {
            capacity = Capacity;
            values = new T[capacity];
            Count = 0;
        }

        public void Push(T element)
        {
            if (Count == capacity)
            {
                throw new StackException("Stack is overflow");
            }
            values[Count] = element;
            Count++;
        }

        public T Pop()
        {
            if (IsEmpty())
            {
                throw new StackException("Stack is underflow");
            }

            Count--;
            T returnValue = values[Count];
            return returnValue;

        }
        public T Peek()
        {
            if (IsEmpty())
            {
                throw new StackException("Stack is underflow");
            }
            var lastIndex = Count - 1;
            return values[lastIndex];
        }

        public bool IsEmpty()
        {
            return Count==0;
        }
    }
}
