using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedList
{
    public class DoublyLinkedList<T> : ICollection<T>
    {
        private class DoublyLinkedListNode
        {
            public T Value { set; get; }
            public DoublyLinkedListNode Previous { set; get; }
            public DoublyLinkedListNode Next { set; get; }

            public DoublyLinkedListNode(T value)
            {
                Value = value;
                Previous = null;
                Next = null;
            }
        }

        private DoublyLinkedListNode First { set; get; }
        private DoublyLinkedListNode Last { set; get; }
        public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }

        public DoublyLinkedList(params T[] arr)
        {
            var current = new DoublyLinkedListNode(default(T));
            foreach (var elem in arr)
            {
                DoublyLinkedListNode node = new DoublyLinkedListNode(elem);
                if (Count == 0)
                {
                    current = node;
                    First = current;
                }
                else
                {
                    current.Next = node;
                    node.Previous = current;
                    current = node;
                }

                Last = current;
                Count++;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = First;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            var node = new DoublyLinkedListNode(item);

            if (First == null)
            {
                First = node;
                Last = node;
            }
            else
            {
                node.Previous = Last;
                Last.Next = node;
                Last = node;
            }
            
            Count++;
        }

        public void AddLast(T item)
        {
            Add(item);
        }

        public void AddFirst(T item)
        {
            var node = new DoublyLinkedListNode(item) { Next = First };
            if (Count == 0)
                Last = node;
            First = node;
            Count++;
        }

        public void Clear()
        {
            Count = 0;
            First = Last = null;
        }

        public bool Contains(T item)
        {
            var current = First;
            while (current != null)
            {
                if (current.Value.Equals(item))
                    return true;
                current = current.Next;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex + Count > array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex),
                    "arrayIndex should not exceed length of array, list should fit the array");

            var current = First;
            while (current != null)
            {
                array[arrayIndex++] = current.Value;
                current = current.Next;
            }
        }

        public bool Remove(T item)
        {
            DoublyLinkedListNode Previous = null, current = First, next = First.Next;

            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    if (Previous != null)
                    {
                        Previous.Next = next;
                        if (next == null)
                            Last = Previous;
                        else
                            next.Previous = Previous;
                    }
                    else
                    {
                        First = First.Next;
                        if (First == null)
                            Last = null;
                        else
                            First.Previous = null;
                    }
                    Count--;
                    return true;
                }
                Previous = current;
                current = next;
                next = next?.Next;
            }

            return false;
        }

        public void RemoveFirst()
        {
            if (Count == 0) return;
            First = First.Next;
            if (First == null)
                Last = First;
            else
                First.Previous = null;
            Count--;
        }

        public void RemoveLast()
        {
            switch (Count)
            {
                case 0:
                    return;
                case 1:
                    First = Last = null;
                    break;
                default:
                    Last = Last.Previous;
                    Last.Previous.Next = null;
                    break;
            }
            Count--;
        }
    }
}
