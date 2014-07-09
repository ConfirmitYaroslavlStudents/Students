using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueOnArray
{
    public class Queue<T>
    {
        public int Count { get; private set; }
        private T[] _array;

        public Queue()
        {
            Count = 0;
            _array = new T[1];
        }

        public void Enqueue(T value)
        {
            //Whenever you add an item >= length, the capacity is doubled.
            //http://social.msdn.microsoft.com/Forums/en-US/84789320-4dc2-4f8f-947a-23c087fd5150/stackqueue-size-limit-and-explosion?forum=csharplanguage

            if (Count == _array.Length)
            {
                Array.Resize(ref _array, _array.Length * 2);
            }
            _array[Count] = value;
            Count++;
        }

        public T Dequeue()
        {
            try
            {
                T value = _array[Count - 1];
                _array[Count - 1] = default(T);
                Count --;
                return value;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Queue is Empty");
            }
        }
    }
}
