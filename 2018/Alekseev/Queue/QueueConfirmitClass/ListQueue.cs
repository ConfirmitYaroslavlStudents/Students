using System;
using System.Collections.Generic;
using System.Linq;

namespace QueueConfirmitClass
{
    public class ListQueue<T> : MyQueue<T>
    {
        private readonly List<T> _queue;

        public ListQueue()
        {
            _queue = new List<T>();
        }
        public override void Enqueue(T element)
        {
            _queue.Add(element);
        }

        public override void Dequeue()
        {
            _queue.RemoveAt(0);
        }

        public override void Clear()
        {
            _queue.Clear();
        }

        public override T Peek()
        {
            return _queue.ElementAt(0);
        }

        public override bool Contains(T element)
        {
            if (_queue.Contains(element))
                return true;
            return false;
        }

        public override int Count()
        {
            return _queue.Count();
        }
    }
}
