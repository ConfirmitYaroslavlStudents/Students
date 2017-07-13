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
            public DoublyLinkedListNode Prev { set; get; }
            public DoublyLinkedListNode Next { set; get; }

            public DoublyLinkedListNode(T value)
            {
                Value = value;
                Prev = null;
                Next = null;
            }
        }

        

        private DoublyLinkedListNode First { set; get; }
        private DoublyLinkedListNode Last { set; get; }
        public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }

        public DoublyLinkedList(params T[] arr)
        {
            DoublyLinkedListNode curr = new DoublyLinkedListNode(default(T));
            foreach (var elem in arr)
            {
                DoublyLinkedListNode node = new DoublyLinkedListNode(elem);
                if (Count == 0)
                {

                    curr = node;
                    First = curr;
                }
                else
                {
                    curr.Next = node;
                    node.Prev = curr;
                    curr = node;
                }

                Last = curr;
                Count++;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            DoublyLinkedListNode curr = First;
            while (curr != null)
            {
                yield return curr.Value;
                curr = curr.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            DoublyLinkedListNode node = new DoublyLinkedListNode(item);

            if (First == null)
            {
                First = node;
                Last = node;
            }
            else
            {
                node.Prev = Last;
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
            DoublyLinkedListNode node = new DoublyLinkedListNode(item) { Next = First };
            if (Count == 0)
                Last = node;
            First = node;
            Count++;
        }

        public void Clear()
        {
            Count = 0;
            DoublyLinkedListNode curr = First;

            while (curr.Next != null)
            {
                curr = curr.Next;
                curr.Prev = null;
            }
            curr = null;
            First = Last = null;
        }

        public bool Contains(T item)
        {
            DoublyLinkedListNode curr = First;
            while (curr != null)
            {
                if (curr.Value.Equals(item))
                    return true;
                curr = curr.Next;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex + Count > array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex),
                    "arrayIndex should not exceed length of array, list should fit the array");

            DoublyLinkedListNode curr = First;
            while (curr != null)
            {
                array[arrayIndex++] = curr.Value;
                curr = curr.Next;
            }
        }

        public bool Remove(T item)
        {
            DoublyLinkedListNode prev = null, curr = First, next = First.Next;

            while (curr != null)
            {
                if (curr.Value.Equals(item))
                {
                    if (prev != null)
                    {
                        prev.Next = next;
                        if (next == null)
                        {
                            Last = prev;
                        }
                        else
                        {
                            next.Prev = prev;
                        }
                    }
                    else
                    {
                        First = First.Next;
                        if (First == null)
                            Last = null;
                        else
                            First.Prev = null;
                    }
                    Count--;
                    return true;
                }
                prev = curr;
                curr = next;
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
                First.Prev = null;
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
                    Last = Last.Prev;
                    Last.Prev.Next = null;
                    break;
            }
            Count--;
        }
    }
}
