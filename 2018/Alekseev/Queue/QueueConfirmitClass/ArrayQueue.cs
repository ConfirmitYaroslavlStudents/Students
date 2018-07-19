using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueConfirmitClass
{
    public class ArrayQueue<T> where T : IComparable<T>
    {
        int MAX_SIZE = 20;
        T[] _queue;
        int front ;
        int rear;

        public ArrayQueue()
        {
            _queue = new T[MAX_SIZE];
            front = rear = -1;
        }
        public void Enqueue(T element)
        {
            if (IsFull())
            {
                Console.WriteLine("Queue is full!");
                return;
            }

            if (IsEmpty())
            {
                front = rear = 0;
            }
            else
                rear = (rear + 1) % MAX_SIZE;

            _queue[rear] = element;
        }

        public bool IsFull()
        {
            return ((rear + 1) % MAX_SIZE == front) ? true : false;
        }
        public bool IsEmpty()
        {
            return (front == -1 && rear == -1);
        }

        public void Dequeue()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Queue is empty!");
                return;
            }
            if (rear == front)
                rear = front = -1;
            else
                front = (front + 1) % MAX_SIZE;
        }

        public T Peek()
        {
            if(front == -1)
            {
                Console.WriteLine("Queue is empty. Have no element to peek");
                return default(T);
            }
            return _queue[front];
        }

        public void Print()
        {
            int count = Count(); 
            Console.WriteLine("Printing queue...");
            for(int i = 0; i < count; i++)
            {
                int index = (front + i) % MAX_SIZE;
                Console.Write("{0} ", _queue[i]);
            }
            Console.WriteLine();
        }

        public void Clear()
        {
            front = rear = -1;
        }

        public int Count()
        {
            if (rear == -1 && front == -1)
                return (rear + MAX_SIZE - front) % MAX_SIZE;
            return (rear + MAX_SIZE - front) % MAX_SIZE + 1;
        }

        public bool Contains(T element)
        {
            return Array.Exists(_queue, elem => elem.CompareTo(element) == 0);
        }
    }
}
