using DoublyLinkedListLibrary;
using System;
using System.Collections;


namespace DoublyLinkedList
{

    public class DoublyLinkedList : IEnumerable
    {
        public Node Head { get; private set; }
        public Node Tail { get; private set; }
        private int _count;
        public DoublyLinkedList()
        {
            Head = null;
            Tail = null;
            _count = 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Node element = Head;

            while (element != null)
            {
                yield return element.Value;
                element = element.Next;
            }
        }


        private void AddFirstElement(Node firstElement)
        {
            Head = firstElement;
            Tail = Head;
            _count++;
        }

        public void AddToTail(string newValue)
        {
            Node newElement = new Node(newValue);

            if (Head == null)
            {
                AddFirstElement(newElement);
            }
            else
            {
                Tail.Next = newElement;
                newElement.Previous = Tail;
                Tail = newElement;
                _count++;
            }


        }

        public void AddToHead(string newValue)
        {
            Node newElement = new Node(newValue);

            if (Head == null)
            {
                AddFirstElement(newElement);
            }
            else
            {
                Head.Previous = newElement;
                newElement.Next = Head;
                Head = newElement;
                _count++;
            }


        }
        public void AddAtIndex(string NewData, int Index)
        {
            if (Index < 1 || Index > _count)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                int nodeNumber = 1;
                Node current = Head;

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
                _count++;
            }
        }

        public void DeleteAtIndex(int Index)
        {
            if (Index < 1 || Index > _count)
            {
                throw new IndexOutOfRangeException();
            }

            else
            {
                int item = 1;
                Node current = Head;

                while (item != Index)
                {
                    current = current.Next;
                    item++;
                }

                if (current.Previous != null) current.Previous.Next = current.Next;
                else Head = current.Next;

                if (current.Next != null) current.Next.Previous = current.Previous;
                else Tail = current.Previous;

                _count--;
            }
        }
        public string FirstElement()
        {
            if (Head != null)
            {
                return Head.Value;
            }

            throw new IndexOutOfRangeException();

        }
        public string LastElement()
        {
            if (Tail != null)
            {
                return Tail.Value;
            }

            throw new IndexOutOfRangeException();
        }

        public string ElementAtIndex(int Index)
        {
            if (Index < 1 || Index > _count)
            {
                return null;
            }

            int item = 1;
            Node current = Head;

            while (item != Index)
            {
                current = current.Next;
                item++;
            }

            return current.Value;
        }

    }


}
