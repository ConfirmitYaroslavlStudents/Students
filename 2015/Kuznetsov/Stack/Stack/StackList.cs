using System;
using System.Collections.Generic;

namespace Stack
{
    public class StackList<T> : IStack<T>
    {
        private class StackListItem<T>
        {
            public StackListItem<T> Next { get; set; }
            public T Value { get; set; }
        }

        private StackListItem<T> _top;

        public int Count { get; private set; }

        public StackList()
        {
        }

        public StackList(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();

            foreach (var e in collection)
                Push(e);
        }

        public void Push(T item)
        {
            StackListItem<T> newItem = new StackListItem<T> {
                Next = _top, 
                Value = item
            };
            _top = newItem;
            Count++;
        }

        public T Pop()
        {
            if (_top == null)
                throw new InvalidOperationException();

            T popped = _top.Value;
            _top = _top.Next;
            Count--;
            return popped;
        }

        public T Peek()
        {
            if (_top == null)
                throw new InvalidOperationException();
            return _top.Value;
        }

        public void Clear()
        {
            _top = null;
            Count = 0;
        }
    }
}
