using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace myList
{
    public class List<T>
    {
        private class Node
        {
            public T Info;
            public Node Next;

            public Node(T info)
            {
                Info = info;
                Next = null;
            }
        }

        private Node Head,
            Tail;

        public int Count { get; private set; }

        public List()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        private Node NodeOfIndex(int index)
        {
            if (index >= Count)
            {
                throw new IndexOutOfRangeException();
            }
            var current = Head;
            for (var i = 0; i < index; i++)
                current = current.Next;
            return current;
        }

        public T this[int index]
        {
            get { return NodeOfIndex(index).Info; }
            set { NodeOfIndex(index).Info = value; }
        }

        private void PushBack(T item)
        {
            if (Head == null)
            {
                Head = new Node(item);
                Tail = Head;
            }
            else
            {
                Tail.Next = new Node(item);
                Tail = Tail.Next;
            }
            Count++;
        }

        public void Insert(T item, int index)
        {
            if (index > Count)
                throw new IndexOutOfRangeException();

            if (index == Count)
            {
                PushBack(item);
                return;
            }

            if (index == 0)
            {
                var current = new Node(item);
                current.Next = Head;
                Head = current;
                Count++;
            }
            else
            {
                var previous = Head;
                for (var i = 0; i < index - 1; i++)
                    previous = previous.Next;
                var current = new Node(item);
                current.Next = previous.Next;
                previous.Next = current.Next;
                Count++;
            }
        }

        public bool Contains(T item)
        {
            var current = Head;
            while (current != null && !current.Info.Equals(item))
                current = current.Next;
            if (current == null)
                return false;
            return true;
        }

        public int IndexOf(T item)
        {
            var current = Head;
            var index = 0;
            while (current != null && !current.Info.Equals(item))
            {
                current = current.Next;
                index++;
            }
            if (current == null)
                return -1;
            return index;
        }

        public void Remove(T item)
        {
            if (Head.Info.Equals(item))
            {
                Head = Head.Next;
                Count--;
            }
            else
            {
                var current = Head;
                while (current.Next != null && !current.Next.Info.Equals(item))
                    current = current.Next;

                if (current.Next == null)
                    return;

                current.Next = current.Next.Next;
                Count--;
                if (current.Next == null)
                    Tail = current;
            }
        }
    }
}
