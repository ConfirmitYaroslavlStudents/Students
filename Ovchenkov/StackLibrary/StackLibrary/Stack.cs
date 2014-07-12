using System;

namespace StackLibrary
{
    public class Stack<T>
    {
        private Elem<T> _top;
        private int _size;

        public int Count
        {
            get { return _size; }
        }

        public Stack()
        {
            _top = null;
            _size = 0;
        }

        public void Push(T newElement)
        {

            var temp = new Elem<T> {Value = newElement, Next = _top};
            _top = temp;
            _size++;
        }

        public T Pop()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException();
            }
            var value = (T) _top.Value;
            _top = _top.Next;
            _size--;
            return value;
        }

        public T Peek()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException();
            }
            return (T) _top.Value;
        }
    }
}


