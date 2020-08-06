using System;
using System.Diagnostics;

namespace CircularQueueTDD
{
    public class TimeTracker<T>: IQueue<T>
    {
        private IQueue<T> _decorated;
      
        public TimeTracker(IQueue<T> decorated)
        {
            _decorated = decorated;
        }

        public int Count => throw new NotImplementedException(); // Возник вопрос
        public int Capasity => throw new NotImplementedException();

        public T Dequeue()
        {
            var stopwatch = Stopwatch.StartNew();

            var result = _decorated.Dequeue();

            stopwatch.Stop();
            Console.WriteLine($"Time to Dequeue {stopwatch.Elapsed}");

            return result;
            
        }

        public void Enqueue(T item)
        {
            var stopwatch = Stopwatch.StartNew();

            _decorated.Enqueue(item);

            stopwatch.Stop();
            Console.WriteLine($"Time to Enqueue {stopwatch.Elapsed}");
        }

        public T Peek()
        {
            var stopwatch = Stopwatch.StartNew();

            var result = _decorated.Peek();

            stopwatch.Stop();
            Console.WriteLine($"Time to Peek {stopwatch.Elapsed}");

            return result;
        }
        public bool Conteins(T item)
        {
            var stopwatch = Stopwatch.StartNew();

            var result = _decorated.Conteins(item);

            stopwatch.Stop();
            Console.WriteLine($"Time to Peek {stopwatch.Elapsed}");

            return result;
        }
    }
}
