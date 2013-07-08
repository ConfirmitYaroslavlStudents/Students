using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackProject
{
    public class StackElement<T>
    {
        public StackElement<T> Previous;
        public T Value { get; set; }
        public StackElement(T Value, StackElement<T> Previous)
        {
            this.Value = Value;
            this.Previous = Previous;
        }
    }
    public class MyStack<T>
    {
        public int Length { get; private set; }
        public StackElement<T> Current;
        public MyStack()
        {
            this.Length = 0;
            this.Current = null;
        }
        public void Push(T NewElement)
        {
            StackElement<T> Temp = new StackElement<T>(NewElement, Current);
            Current = Temp;
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
                return Current.Value;
            }
        }
        public T Pop()
        {
            if (Length > 0)
            {
                T Temp = Current.Value;
                Current = Current.Previous;
                Length--;
                return Temp;
            }
            else
            {
                throw new InvalidOperationException("MyStack is Emty");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            MyStack<int> Example = new MyStack<int>();
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
            MyStack<string> SecondExample = new MyStack<string>();
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
