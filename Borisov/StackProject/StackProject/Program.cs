using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackProject
{
    public class StackElement<T>
    {
        public StackElement<T> Previous { get; private set; }
        public T Value { get; private set; }
        public StackElement(T value, StackElement<T> previous)
        {
            this.Value = value;
            this.Previous = previous;
        }
    }
    public class Stack<T>
    {
        public int Length { get; private set; }
        private StackElement<T> _peek;
        public Stack()
        {
            this.Length = 0;
            this._peek = null;
        }
        public void Push(T newElement)
        {
            StackElement<T> temp = new StackElement<T>(newElement, _peek);
            _peek = temp;
            Length++;
        }
        public T Peek()
        {
            if (Length == 0)
            {
                throw new InvalidOperationException("MyStack is Emty");
            }
            else
            {
                return _peek.Value;
            }
        }
        public T Pop()
        {
            if (Length <= 0)
            {
                throw new InvalidOperationException("MyStack is Emty");

            }
            else
            {
                T temp = _peek.Value;
                _peek = _peek.Previous;
                Length--;
                return temp;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> Example = new Stack<int>();
            Example.Push(124);
            Example.Push(605);
            Example.Push(89);
            Example.Push(1);
            int length = Example.Length;
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(Example.Pop());
            }
            Example.Push(194);
            Console.WriteLine(Example.Peek());
            Stack<string> SecondExample = new Stack<string>();
            SecondExample.Push("know");
            SecondExample.Push("don`t");
            SecondExample.Push("i");
            SecondExample.Push("no?");
            SecondExample.Push("yes?");
            length = SecondExample.Length;
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(SecondExample.Pop());
            }
            Console.ReadKey();
        }
    }
}
