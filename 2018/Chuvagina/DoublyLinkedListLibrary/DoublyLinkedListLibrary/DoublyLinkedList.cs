
using System;
using System.Collections;


namespace DoublyLinkedListLibrary
{

    public class DoublyLinkedList<T> 
    {
        private class Node
        {
            public T Value { get; set; }

            public Node(T Value)
            {
                this.Value = Value;
                Previous = null;
                Next = null;
            }

                       
           public Node Previous { get; set; }

           public Node Next { get; set; }
          
        }

        private Node Head;
        private Node Tail;
        private int _count;

        public DoublyLinkedList(params T[] list)
        {
            Head = null;
            Tail = null;
            _count = 0;
            foreach (T item in list)
            {
                AddToTail(item);
            }
        }

        public IEnumerable GetList(bool Reverse)
        {
            if (Reverse)
            {
                Node element = Tail;

                while (element != null)
                {
                    yield return element.Value;
                    element = element.Previous;
                }
            }
            else
            {
                Node element = Head;

                while (element != null)
                {
                    yield return element.Value;
                    element = element.Next;
                }
            }

        }

        


        private void AddFirstElement(Node firstElement)
        {
            Head = firstElement;
            Tail = Head;
            _count++;
        }

        public void AddToTail(T newValue)
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

        public void AddToHead(T newValue)
        {
            Node newElement = new Node (newValue);

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
        public void Insert( int Index,T newValue)
        {
            if (Index < 1 || Index > _count)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                if (Index == 1)
                    AddToHead(newValue);
                else
                {
                    int item = 1;
                    Node current = Head;

                    while (item != Index)
                    {
                        current = current.Next;
                        item++;
                    }

                    Node newNode = new Node(newValue);
                    newNode.Previous = current.Previous;
                    newNode.Next = current;
                    current.Previous = newNode;
                    newNode.Previous.Next = newNode;
                    _count++;
                }
                
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
        public T FirstElement()
        {
            if (Head != null)
            {
                return Head.Value;
            }

            throw new IndexOutOfRangeException();

        }
        public T LastElement()
        {
            if (Tail != null)
            {
                return Tail.Value;
            }

            throw new IndexOutOfRangeException();
        }

        public T ElementAtIndex(int Index)
        {
            if (Index < 1 || Index > _count)
            {
                return default(T);
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
