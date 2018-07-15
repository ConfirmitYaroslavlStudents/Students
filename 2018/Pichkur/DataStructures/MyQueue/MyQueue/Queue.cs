using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQueue
{
    public class Queue<T>
    {
        private Node<T> _first;
        private Node<T> _last;

        public int Count { get; private set; }

        public Queue()
        {
            _first = null;
            _last = null;
            Count = 0;
        }

        public void Enqueue(T data)
        {
            Count++;
            if (_first == null)
            {
                _first = new Node<T>(data);
                _last = _first;
            }
            else
            {
                Node<T> item = new Node<T>(data);
                _last.Next = item;
                _last = item;
            }
        }

        public T Dequeue()
        {
            T item;
            if (_first != null)
            {
                item = _first.Value;
                _first = _first.Next;
                Count--;
                return item;
            }
            else
                throw new InvalidOperationException();
        }
        
    }
}
