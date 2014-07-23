namespace StackInterface
{
    public interface IStack<T>
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
