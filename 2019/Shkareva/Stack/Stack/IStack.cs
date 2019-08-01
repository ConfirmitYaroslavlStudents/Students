
namespace Stack
{
    public interface IStack<T>
    {
        T Pop();
        void Push(T element);
        T Peek();
        int Count { get; }
    }
}
