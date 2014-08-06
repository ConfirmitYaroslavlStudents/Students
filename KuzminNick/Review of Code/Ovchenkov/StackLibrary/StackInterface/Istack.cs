using System.Collections.Generic;

namespace StackInterface
{
    public interface IStack<T> : IEnumerable<T>
    {

        int Count { get; }
        T this[int i] { get; }
        void Push(T element);
        T Pop();
        T Peek();
        void Clear();
        bool Contains(T element);
    }
}
