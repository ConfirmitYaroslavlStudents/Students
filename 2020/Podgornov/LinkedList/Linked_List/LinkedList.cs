using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Linked_List
{
    public class LinkedList<T> : IEnumerable<T>
    {
        public Node<T> First { get; private set; }
        public Node<T> Last { get; private set; }
        public int Count { get; private set; }

        public LinkedList()
        {
        }
        public LinkedList(IEnumerable<T> collection)
        {
            foreach (var current in collection)
            {
                if (First == null)
                {
                    First = new Node<T>(current);
                    Last = First;
                }
                else
                {
                    var temp = new Node<T>(current);
                    temp.Previous = Last;
                    Last.Next = temp;
                    Last = temp;
                }
            }
            Count += collection.Count();

        }

        public void AddFirst(Node<T> node)
        {
            node.Next = First;
            First.Previous = node;
            First = node;
            Count++;
        }
        public Node<T> AddFirst(T value)
        {
            var node = new Node<T>(value);
            AddFirst(node);
            return node;
        }

        public void AddLast(Node<T> node)
        {
            node.Previous = Last;
            Last.Next = node;
            Last = node;
            Count++;
        }
        public Node<T> AddLast(T value)
        {
            var node = new Node<T>(value);
            AddLast(node);
            return node;
        }


        public void AddBefore(Node<T> node, Node<T> newNode)
        {            
            if (node.Previous == null)
            {
                First = newNode;
            }
            else
            {
                newNode.Previous = node.Previous;
                node.Previous.Next = newNode;
            }
            newNode.Next = node;
            node.Previous = newNode;           
            Count++;
        }
        public Node<T> AddBefore(Node<T> node, T value)
        {
            var newNode = new Node<T>(value);
            AddBefore(node, newNode);
            return newNode;
        }

        public void AddAfter(Node<T> node, Node<T> newNode)
        {
            if (node.Next == null)
            {
                Last = newNode;
            }
            else
            {
                newNode.Next = node.Next;
                node.Next.Previous = newNode;
            }               
            newNode.Previous = node;
            node.Next = newNode;

            Count++;
        }
        public Node<T> AddAfter(Node<T> node, T value)
        {
            var newNode = new Node<T>(value);
            AddAfter(node, newNode);
            return newNode;
        }

        public bool Contains(T value)
        {
            for (Node<T> current = First; current != null; current = current.Next)
            {
                if (current.Value.Equals(value)) return true;
            }
            return false;
        }

        public Node<T> Find(T value)
        {
            for (Node<T> current = First; current != null; current = current.Next)
            {
                if (current.Value.Equals(value)) return current;
            }
            return null;
        }
        public Node<T> FindLast(T value)
        {
            for (Node<T> current = Last; current != null; current = current.Previous)
            {
                if (current.Value.Equals(value)) return current;
            }
            return null;
        }

        public bool Remove(T value)
        {

            for (Node<T> current = First; current != null; current = current.Next)
            {
                if (current.Value.Equals(value))
                {
                    if (current.Previous != null)
                    {
                        current.Previous.Next = current.Next;
                    }
                    else
                    {
                        First = current.Next;
                    }
                    if (current.Next != null)
                    {
                        current.Next.Previous = current.Previous;
                    }
                    else
                    {
                        Last = current.Previous;
                    }

                    Count--;
                    return true;
                }
            }
            return false;
        }
        public bool RemoveFirst()
        {
            if (Count == 0)
            {
                return false;
            }
            else
            {
                First = First.Next;
                if (First != null) First.Previous = null;
                Count--;
                return true;
            }
        }
        public bool RemoveLast()
        {
            if (Count == 0)
            {
                return false;
            }
            else
            {
                Last = Last.Previous;
                if (Last != null) Last.Next = null;
                Count--;
                return true;
            }
        }


        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            for (Node<T> current = First; current != null; current = current.Next)
            {
                yield return current.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (Node<T> current = First; current != null; current = current.Next)
            {
                yield return current.Value;
            }
        }

    }
}
