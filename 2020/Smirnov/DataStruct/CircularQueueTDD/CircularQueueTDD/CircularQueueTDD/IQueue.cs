namespace CircularQueueTDD
{
    public interface IQueue<T>
    {
        int Count { get; }
        int Capasity { get; }

        void Enqueue(T item);
        T Dequeue();
        T Peek();
        bool Conteins(T item);
    }
}
