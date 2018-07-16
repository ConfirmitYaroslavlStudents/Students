using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueueImplementations
{
    public interface IQueue<T>
    {
        void Enqueue(T data);
        T Dequeue();
        int Count();
        T First();
        T Last();
        bool Contains(T data);

    }
}
