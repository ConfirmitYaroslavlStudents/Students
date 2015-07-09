using System.Collections.Generic;

namespace QueueInterface
{
    public interface IQueueable<T> : IEnumerable<T>
    {
        int Count { get; }
        void Enqueue(T value);
        T Dequeue();
        T Peek();
        void Clear();
    }
}
