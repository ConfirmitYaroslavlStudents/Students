using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace myList
{
    public class List<T>
    {
        class Node
        {
            public T info;
            public Node next;
            public Node(T Info)
            {
                info = Info;
                next = null;
            }
        }

        private Node head,
           tail;
        private int count;

        public List()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public int Count
        {
            get
            {
                return count;
            }
        }

        public T this[int index]
        {
            get
            {
                if (index >= count)
                {
                    throw new Exception("Index out of range");
                }
                Node current = head;
                for (int i = 0; i < index; i++)
                    current = current.next;
                return current.info;
            }
            set
            {
                if (index >= count)
                {
                    throw new Exception("Index out of range");
                }
                Node current = head;
                for (int i = 0; i < index; i++)
                    current = current.next;
                current.info = value;
            }
        }

        public void PushBack(T item)
        {
            if (head == null)
            {
                head = new Node(item);
                tail = head;
            }
            else
            {
                tail.next = new Node(item);
                tail = tail.next;
            }
            count++;
        }

        public bool Contains(T item)
        {
            Node current = head;
            while (current != null && !current.info.Equals(item))
                current = current.next;
            if (current == null)
                return false;
            return true;
        }

        public int IndexOfFirstEquals(T item)
        {
            Node current = head;
            int index = 0;
            while (current != null && !current.info.Equals(item))
            {
                current = current.next;
                index++;
            }
            if (current == null)
                return -1;
            return index;
        }

        public void Insert(T item, int index)
        {
            if (index > count)
            {
                throw new Exception("Index out of range");
            }
            if (index == count)
            {
                PushBack(item);
                return;
            }
            if (index == 0)
            {
                Node current = new Node(item);
                current.next = head;
                head = current;
                count++;
            }
            else
            {
                Node previous = head;
                for (int i = 0; i < index - 1; i++)
                    previous = previous.next;
                Node current = new Node(item);
                current.next = previous.next;
                previous.next = current.next;
                count++;
            }
        }

        public void RemoveFirstEquals(T item)
        {
            if (head.info.Equals(item))
            {
                head = head.next;
                count--;
            }
            else
            {
                Node current = head;
                while (current.next != null && !current.next.info.Equals(item))
                    current = current.next;
                if (current.next != null)
                {
                    current.next = current.next.next;
                    count--;
                    if (current.next == null)
                        tail = current;
                }
            }
        }

        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }
    }
}
