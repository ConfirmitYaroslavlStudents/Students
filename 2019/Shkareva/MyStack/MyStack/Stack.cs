using System;

namespace MyStack
{
    public class Stack<T>
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
            if (!IsEmpty())
            {
                T returnValue = head.Value;
                head = head.Previous;
                Count--;
                return returnValue;
                
            }
            else
            {
                throw new Exception("Stack is underflow");
            }
           
        }
        public T Peek()
        {
            if (!IsEmpty())
            {
                return head.Value;
            }
            else
            {
                throw new Exception("Stack is underflow");
            }
        }

        public bool IsEmpty()
        {
            return head == null;
        }
    }
}
