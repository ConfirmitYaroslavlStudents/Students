using System;

namespace Stack_based_on_array
{
    // TODO: Add default constructor
    // TODO: support overflow
    public class StackOnArray<T>
    {
        private T[] values;
        private int capacity;
        public int Count { get; private set; }

        public StackOnArray()
        {
            capacity = 10;
            values = new T[capacity];
            Count = 0;
        }

        public void Push(T element)
        {
            if (Count == capacity)
            {
                Array.Resize(ref values, capacity * 2);
                capacity *= 2;
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
