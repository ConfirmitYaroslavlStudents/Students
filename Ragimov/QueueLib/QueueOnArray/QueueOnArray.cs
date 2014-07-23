using System;
using System.Collections.Generic;
using QueueInterface;

namespace QueueOnArray
{
    public class Queue<T> : IQueueable<T>
    {
        public int Count { get; private set; }
        private T[] _array;
        private int _front;
        private int _back;
        public Queue()
        {
            Clear();
        }

        public void Enqueue(T value)
        {
            /* Both Stack<T> and Queue<T> use an array internally (T[]).  By default, it's setup with a length of 0.
             * On the first item added, it's set to length of 4.  Whenever you add an item >=length, the capacity is doubled.*/
            //http://social.msdn.microsoft.com/Forums/en-US/84789320-4dc2-4f8f-947a-23c087fd5150/stackqueue-size-limit-and-explosion?forum=csharplanguage

            if (Count == _array.Length)
            {
                Array.Resize(ref _array, _array.Length * 2);
            }
            _array[_back++%_array.Length] = value;
            Count++;
        }

        public T Dequeue()
        {
            if (Count == 0) throw new InvalidOperationException("Queue is Empty");
            var first = _front%_array.Length;
            var value = _array[first];
            _array[first] = default(T);
            _front++;
            Count--;
            return value;
        }

        public T Peek()
        {
            if (Count == 0) throw new InvalidOperationException("Queue is Empty");
            return _array[_front];
        }

        public void Clear()
        {
            Count = 0;
            _front = 0;
            _back = 0;
            _array = new T[4];
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = _front; i < Count; i++)
            {
                yield return _array[i];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
