using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQueue
{
    public class Queue<T>
    {
        private Node<T> First;
        private Node<T> Last;
        private int _count;

        public Queue()
        {
            First = null;
            Last = null;
            _count = 0;
        }

        public void Enqueue(T data)
        {
            _count++;
            if (First == null)
            {
                First = new Node<T>(data);
                Last = First;
            }
            else
            {
                Node<T> item = new Node<T>(data);
                Last.Next = item;
                Last = item;
            }
        }

        public T Dequeue()
        {
            T item;
            if (First != null)
            {
                item = First.Value;
                First = First.Next;
                _count--;
                return item;
            }
            else
                throw new InvalidOperationException();
        }

        public int Count()
        {
            return _count;
        }
    }
}
