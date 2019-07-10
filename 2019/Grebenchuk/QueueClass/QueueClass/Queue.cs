using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueClass
{
    class UsualQueue <T>
    {
        const int N = 5;
        T[] queue;
        int begin;
        int end;
        int count;

        public UsualQueue()
        {
            queue = new T[N];
            begin = 0;
            end = 0;
        }

        public UsualQueue(int k)
        {
            queue = new T[k];
            begin = 0;
            end = 0;
        }

        public int Count
        {
            get
            {
                return count;
            }
        }

        int Size
        {
            get
            {
                return queue.Length;
            }
        }

        public bool IsEmpty()
        {
            if (this.Count == 0) return true;
            else return false;
        }

        public void Enqueue(T elem)
        {
            queue[end] = elem;
            int tail = (end + 1) % this.Size;
            if (tail == begin) Increase();
            else end = tail;
            count++;
        }

        public T Dequeue()
        {
            if (IsEmpty()) throw new ArgumentOutOfRangeException("The queue is empty");
            else
            {
                int m = begin;
                begin = (begin + 1) % Size;
                count--;
                return queue[m];
            }
        }

        public T Peek()
        {
            if (IsEmpty()) throw new ArgumentOutOfRangeException("The queue is empty");
            else return queue[begin];
        }

        public void Clear()
        {
            Array.Clear(queue, 0, Size);
            queue = new T[N];
            begin = 0;
            end = 0;
            count = 0;
        }

        void Increase()
        {
            T[] mas = new T[Size * 2];
            int m = begin;
            int i = 0;
            while(m!=end)
            {
                mas[i] = queue[m];
                m = (m + 1) % Size;
                i++;
            }
            begin = 0;
            end = Count;
            queue = mas;
        }
    }
}
