using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_1
{
    public class Stack<T> : IStack<T>
    {
        private Node<T> _top;
        public int Count { get; private set; }

        public Stack()
        {
        }

        public Stack(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();

            foreach (var e in collection)
                Push(e);
        }

        public bool IsEmpty()
        {
            return this.Count == 0;
        }

        public void Push(T item)
        {
            Node<T> newItem = new Node<T>
            {
                Next = _top,
                Data = item
            };
            _top = newItem;
            Count++;
        }

        public T Pop()
        {
            if (_top == null)
                throw new InvalidOperationException();

            T itemToPop = _top.Data;
            _top = _top.Next;
            Count--;
            return itemToPop;
        }

        public T Peek()
        {
            if (_top == null)
                throw new InvalidOperationException();
            return _top.Data;
        }

        public void Clear()
        {
            _top = null;
            Count = 0;
        }
    }
}