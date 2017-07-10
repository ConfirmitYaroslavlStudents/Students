using System;
using System.Collections.Generic;
using System.Linq;
namespace StackLibrary
{
    public class Stack<T>
    {
        private T[] _items;
        private int _count;

        public int Count
        {
            get
            {
                return _count;
            }
            private set
            {
                _count = value > 0 ? value : 0;
            }
        }
        public int Capacity
        {
            get { return _items.Length; }
        }

        public Stack()
        {
            Init();
        }
        public Stack(IEnumerable<T> collection)
        {
            Init();
            foreach (T item in collection)
                Push(item);
        }

        private void Init()
        {
            _items = new T[8];
            Count = 0;
        }
        private void CheckCapacity()
        {
            if(Count*3 > Capacity*2)
            {
                T[] newItems = new T[_items.Length * 2];
                _items.CopyTo(newItems, 0);
                _items = newItems;
            }
        }

        public void Push(T pushedItem)
        {
            _items[Count] = pushedItem;
            Count++;
            CheckCapacity();
        }
        public T Pop()
        {
            if(Count!=0)
            {
                Count--;
                return _items[Count];
            }
            throw new InvalidOperationException("The Stack is empty");
        }
        public T Peek()
        {
            if (Count != 0)
                return _items[Count - 1];
            throw new InvalidOperationException("The Stack is empty");
        }
        public bool Contains(T item)
        {
            return _items.Contains<T>(item);
        }
        public void Clear()
        {
            Init();
        }
    }
}
