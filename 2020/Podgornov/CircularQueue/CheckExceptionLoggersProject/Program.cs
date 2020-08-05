using CircularQueue;
namespace CheckExceptionLoggersProject
{
    class Program
    {
        static void Main()
        {
            var queue = new CircularQueueExceptionConsoleLogger<int>(
                new CircularQueueExceptionFileLogger<int>(new CircularQueue<int>()));
            queue.Dequeue();
        }
    }
}
