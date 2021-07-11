using System;
using System.Collections.Generic;

namespace StackStructure
{
    public class Stack<T>
    {
        private List<T> elements;

        public  Stack()
        {
            elements = new List<T>();
        }

        public int Count()
        {
            return elements.Count;
        }

        public void Push(T current)
        {
            elements.Add(current);
        }

        public T Pop()
        {
            if (elements.Count == 0)
                throw new InvalidOperationException();

            var item = elements[elements.Count - 1];
            elements.RemoveAt(elements.Count - 1);
            return item;   
        }
        public T Peek()
        {
            if (elements.Count == 0)
                throw new InvalidOperationException();

            return elements[elements.Count - 1];
        }
    }
}
