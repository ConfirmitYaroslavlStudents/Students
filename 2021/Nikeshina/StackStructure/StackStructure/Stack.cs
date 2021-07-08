using System;
using System.Collections.Generic;

namespace StackStructure
{
    public class Stack<T>
    {
        public int Count { get; private set; }
        private List<T> elements;
        public  Stack()
        {
            elements = new List<T>();
            Count = 0;
        }
        public void Push(T current)
        {
            if (elements.Count < Count)
                elements[Count-1] = current;
            else
                elements.Add(current);
            Count=Count+1;
        }
        public T Pop()
        {
            if (Count > 0)
            {
                Count--;
                return elements[Count];
            }
            else
                throw new NullReferenceException();
            
        }
        public T Peek()
        {
            if (Count > 0)
                return elements[Count - 1];
            else
                throw new NullReferenceException();
        }
    }
}
