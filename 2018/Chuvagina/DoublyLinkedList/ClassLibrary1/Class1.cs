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
            data = Data;
            previous = null;
            next = null;
        }
        public Node Next
        {
            get { return next; }
            set { next = value; }
        }
        public Node Previous
        {
            get { return previous; }
            set { previous = value; }
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
        private int count;
        public DoublyLinkedList()
        {
            head = null;
            current = null;
            tail = null;
            count = 0;
        }
        public int Count 
        {
           get { return count; }
        }
        public void AddLast(object NewData)
        {
            Node newNode = new Node(NewData);
            if (head == null)
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
            count++;
        }
        public void AddFirst(object NewData)
        {
            Node newNode = new Node(NewData);
            if (head == null)
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
            count++;
        }
        public void AddAtIndex(object NewData, int Index)
        {
            if (Index < 1 || Index > count)
            {
               // throw new InvalidOperationException();
            }
            else
            {
                int nodeNumber = 1;
                current = head;
                while (nodeNumber != Index)
                {
                    current = current.Next;
                    nodeNumber++;
                }
                Node newNode = new Node(NewData);
                newNode.Previous = current.Previous;
                newNode.Next = current;
                current.Previous = newNode;
                newNode.Previous.Next = newNode;
                count++;
            }
        }

        public void DeleteAtIndex(int Index)
        {
            if (Index < 1 || Index > count)
            {
              ///  throw new InvalidOperationException();
            }
            else if (Index == 1)
            {
                PopFirst();
            }
            else if (Index == count)
            {
                PopLast();
            }
            else
            {
                int nodeNumber = 1;
                current = head;
                while (nodeNumber != Index)
                {
                    current = current.Next;
                    nodeNumber++;
                }
                current.Previous.Next = current.Next;
                current.Next.Previous = current.Previous;
                count--;
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
                count--;
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
                count--;
                return element;
            }
            else return "There's nothing to pop.";

        }
        public string Show()
        {
            int nodeNumber = 1;
            string display = "";
            current = head;
            while (current != null)
            {
                display = display+current.ToString() + "/";
                nodeNumber++;
                current = current.Next;

            }
            if (nodeNumber == 1) display = "The list is empty.";
            return display;
        }
        public string ReverseShow()
        {
            int nodeNumber = 1;
            string display = "";
            current = tail;
            while (current != null)
            {
                display = display + current.ToString() + "/";
                nodeNumber++;
                current = current.Previous;

            }
            if (nodeNumber == 1) display = "The list is empty.";
            return display;
        }
    }


}
