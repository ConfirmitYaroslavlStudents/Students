using System.Collections.Generic;

namespace MyList
{
    public interface IList<T>
    {
        void Add(T item);

        void AddRange(IEnumerable<T> items);
        void Clear();
        int IndexOf(T item);
        bool Contains(T item);
        void Remove(T item);
        int Count { get; }
        void Insert(int index, T item);
        void RemoveAt(int index);

    }
}
