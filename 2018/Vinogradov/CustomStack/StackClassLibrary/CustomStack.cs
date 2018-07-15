using System;
using System.Text;

namespace StackClassLibrary
{
    public class CustomStack<T>
    {
        class Node
        {
            public T Value;
            public Node Next;
            public Node(T value)
            {
                Value = value;
            }
        }

        Node _top;
        public int Count { get; private set; }
        public CustomStack()
        {
            _top = null;
            Count = 0;
        }
        public void Push(T value)
        {
            var newNode = new Node(value);
            newNode.Next = _top;
            _top = newNode;
            Count++;
        }
        public T Peek()
        {
            if (_top != null)
                return _top.Value;
            throw new InvalidOperationException("Stack is empty");
        }
        public T Pop()
        {
            if (_top != null)
            {
                T tmp = _top.Value;
                _top = _top.Next;
                Count--;
                return tmp;
            }
            throw new InvalidOperationException("Stack is empty");
        }
        public void Clear()
        {
            _top = null;
            Count = 0;
        }
        public bool Contains(T value)
        {
            bool found = false;
            var tmpNode = _top;
            while (tmpNode != null)
            {
                if (tmpNode.Value.Equals(value))
                {
                    found = true;
                    break;
                }
                tmpNode = tmpNode.Next;
            }
            return found;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            var tmpNode = _top;
            while (tmpNode != null)
            {
                sb.AppendFormat("{0} ", tmpNode.Value);
                tmpNode = tmpNode.Next;
            }
            return sb.ToString();
        }
        public T[] ToArray()
        {
            if (Count > 0)
            {
                T[] array = new T[Count];
                var tmpNode = _top;
                for (int i = 0; i < Count; i++)
                {
                    array[i] = tmpNode.Value;
                    tmpNode = tmpNode.Next;
                }
                return array;
            }
            throw new InvalidOperationException("Stack is empty");
        }
    }
}
