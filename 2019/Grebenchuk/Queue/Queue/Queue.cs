using System;


namespace Queue
{
    public class Queue<T>

    {
        private const int DefaultCapacity = 5;
        private T[] queue;
        private int begin;
        private int end;
        public int Count { get; private set; }

        public Queue()
        {
            queue = new T[DefaultCapacity];
            begin = 0;
            end = 0;
        }

        public Queue(int capacity)
        {
            queue = new T[capacity];
            begin = 0;
            end = 0;
        }

        private int Size
        {
            get
            {
                return queue.Length;
            }
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }

        public void Enqueue(T item)
        {
            queue[end] = item;
            int tail = (end + 1) % Size;
            if (tail == begin) Increase();
            else end = tail;
            Count++;
        }

        public T Dequeue()
        {
            if (IsEmpty()) throw new InvalidOperationException("The queue is empty");
            else
            {
                int m = begin;
                begin = (begin + 1) % Size;
                Count--;
                return queue[m];
            }
        }

        public T Peek()
        {
            if (IsEmpty()) throw new InvalidOperationException("The queue is empty");
            else return queue[begin];
        }

        public void Clear()
        {
            Array.Clear(queue, 0, Size);
            queue = new T[DefaultCapacity];
            begin = 0;
            end = 0;
            Count = 0;
        }

        private void Increase()
        {
            T[] mas = new T[Size * 2];
            Array.Copy(queue, begin, mas, 0, queue.Length - begin);
            Array.Copy(queue, 0, mas, queue.Length - begin, begin);
            begin = 0;
            end = Count;
            queue = mas;
        }

    }
}
