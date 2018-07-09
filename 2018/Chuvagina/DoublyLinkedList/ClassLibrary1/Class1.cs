using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListLibrary
{
    public class Node
    {
        private object data;
        private Node previous;
        private Node next;
        public Node(object Data)
        {
            this.data = Data;
            this.previous = null;
            this.next = null;
        }
        public Node Next
        {
            get { return this.next; }
            set { this.next = value; }
        }
        public Node Previous
        {
            get { return this.previous; }
            set { this.previous = value; }
        }
        public override string ToString()
        {
            return data.ToString();
        }
    } 

    public class DoublyLinkedList
    {
        private Node head;
        private Node current;
        private Node tail;
        public DoublyLinkedList()
        {
            head = null;
            current = null;
            tail = null;
        }
        public int Count()
        {
            int size = 0;
            current = head;
            while (current != null)
            {
                current = current.Next;
                size++;
            }

            return size;
        }
        public void AddLast(object NewData)
        {
            Node newNode = new Node(NewData);
            if (this.head == null)
            {
                head = newNode;
                tail = head;
            }
            else
            {
                tail.Next = newNode;
                newNode.Previous = tail;
                tail = newNode;
            }
        }
        public void AddFirst(object NewData)
        {
            Node newNode = new Node(NewData);
            if (this.head == null)
            {
                head = newNode;
                tail = head;
            }
            else
            {
                head.Previous = newNode;
                newNode.Next = head;
                head = newNode;
            }
        }
        public void AddAtIndex(object NewData, int Index)
        {
            if (Index < 1 || Index > Count())
            {
               // throw new InvalidOperationException();
            }
            else
            {
                int count = 1;
                current = head;
                while (count != Index)
                {
                    current = current.Next;
                    count++;
                }
                Node newNode = new Node(NewData);
                newNode.Previous = current.Previous;
                newNode.Next = current;
                current.Previous = newNode;
                newNode.Previous.Next = newNode;
            }
        }
        public void DeleteAtIndex(int Index)
        {
            if (Index < 1 || Index > Count())
            {
              ///  throw new InvalidOperationException();
            }
            else if (Index == 1)
            {
                if (head.Next != null) head.Next.Previous = null;
                head = head.Next;
            }
            else if (Index == Count())
            {
                tail.Previous.Next = null;
                tail = tail.Previous;
            }
            else
            {
                int count = 1;
                current = head;
                while (count != Index)
                {
                    current = current.Next;
                    count++;
                }
                current.Previous.Next = current.Next;
                current.Next.Previous = current.Previous;
            }
        }
        public object PopFirst()
        {
            if (head != null)
            {
                object element = head;
                if (head.Next != null) head.Next.Previous = null;
                else tail = null;
                head = head.Next;
                return element;
            }
            else return "There's nothing to pop.";

        }
        public object PopLast()
        {
            if (tail != null)
            {
                object element = tail;
                if (tail.Previous != null) tail.Previous.Next = null;
                else head = null;
                tail = tail.Previous;
                return element;
            }
            else return "There's nothing to pop.";

        }
        public string Show()
        {
            int count = 1;
            string display = "";
            current = head;
            while (current != null)
            {
                display = display+current.ToString() + "/";
                count++;
                current = current.Next;

            }
            if (count == 1) display = "The list is empty.";
            return display;
        }
        public string ReverseShow()
        {
            int count = 1;
            string display = "";
            current = tail;
            while (current != null)
            {
                display = display + current.ToString() + "/";
                count++;
                current = current.Previous;

            }
            if (count == 1) display = "The list is empty.";
            return display;
        }
    }


}
