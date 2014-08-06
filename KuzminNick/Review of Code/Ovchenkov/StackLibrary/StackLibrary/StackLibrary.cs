using System;
using System.Collections;
using System.Collections.Generic;
using StackInterface;

namespace StackLibrary
{
    public class Stack<T> : IStack<T>
    {

        private class Element<TValue>
        {
            public TValue Value { get; set; }
            public Element<TValue> Next { get; set; }
        }
      
        private Element<T> _top;

        public int Count { get; private set; }

        public T this[int i]
        {
            get
            {
                var elemnt = _top;
                for (int j = 0; j < i; ++j)
                    elemnt = elemnt.Next;

                return elemnt.Value;
            }
        }

        public Stack()
        {
            _top = null;
            Count = 0;
        }

        public Stack(IEnumerable<T> collection)
        {
            _top = null;
            Count = 0;
            foreach (var element in collection)
            {
                Push(element);
            }
        }

        public void Push(T element)
        {
            var temp = new Element<T> {Value = element, Next = _top};
            _top = temp;
            Count++;
        }

        public T Pop()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
            var value = _top.Value;
            _top = _top.Next;
            Count--;
            return value;
        }

        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
            return _top.Value;
        }

        public void Clear()
        {
            _top = null;
            Count = 0;
        }

        public bool Contains(T element)
        {
            var stackElement = _top;
            while (stackElement != null)
            {
                if (Equals(stackElement.Value, element))
                    return true;
                stackElement = stackElement.Next;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            yield return _top.Value;

            var stackElement = _top;
            while (stackElement.Next != null)
            {
                stackElement = stackElement.Next;
                yield return stackElement.Value;
            }
        }
      
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}


