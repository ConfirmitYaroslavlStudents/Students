using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyQueue
{
    public class MyQueue<T>: IEnumerable<T>
    {
        private T[] items;
        private int head;
        private int tail;
        public int Count { get; private set; }
        private int Capacity { get; set; }

        public MyQueue() : this(4) { }
        public MyQueue(int capacity)
        {
            if (capacity > 0)
            {
                Capacity = capacity;
                items = new T[Capacity];
            }
            else
                throw new ArgumentOutOfRangeException();
        }
        public MyQueue(IEnumerable<T> list)
        {
            if (list != null)
            {
                Capacity = list.Count();
                Count = Capacity;
                tail = Count-1;
                items = new T[Capacity];
                Array.Copy(list.ToArray(), items, list.Count());
            }
            else
                throw new ArgumentNullException();
        }
        public void Enqueue(T item)
        {
            if(Count==Capacity)
                IncreaseCapacity();
            items[tail] = item;
            tail++;
            tail %= Capacity;
            Count++;
        }
        public T Dequeue()
        {
            if (Count > 0)
            {
                var item = items[head];
                head++;
                head %= Capacity;
                Count--;
                return item;
            }
            else
                throw new InvalidOperationException();
        }
        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException();
            return items[head];
        }
        private void IncreaseCapacity()
        {
            Capacity *= 2;
            var temp = new T[Capacity];
            Array.Copy(items, head, temp, 0, Count - head);
            Array.Copy(items, 0, temp, Count - head, tail);
            head = 0;
            tail = Count;
            items = temp;
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = head; i != tail; i = (i + 1) % Capacity)
                yield return items[i];
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
