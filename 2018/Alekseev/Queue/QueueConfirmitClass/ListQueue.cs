using System;
using System.Collections.Generic;
using System.Linq;

namespace QueueConfirmitClass
{
    public class ListQueue<T>
    {
        private List<T> queue;

        public ListQueue()
        {
            queue = new List<T>();
        }
        public void Enqueue(T element)
        {
            queue.Add(element);
        }

        public T Dequeue()
        {
            var removed = this.Peek();
            queue.RemoveAt(0);
            return removed;
        }

        public void Clear()
        {
            queue.Clear();
        }

        public T Peek()
        {
            return queue.ElementAt(0);
        }

        public bool Contains(T element)
        {
            if (queue.Contains(element))
                return true;
            return false;
        }

        public long Count()
        {
            return queue.Count();
        }
    }
}
