using System;

namespace CircularQueue
{
    public class CircularQueueExceptionConsoleLogger<T> : CircularQueue<T>
    {
        private readonly CircularQueue<T> _circularQueue;

        public CircularQueueExceptionConsoleLogger(CircularQueue<T> circularQueue)
        {
            _circularQueue = circularQueue;
        }

        public CircularQueueExceptionConsoleLogger(int capacity = DefaultSize):base(capacity)
        {}

        public override T Dequeue()
        {
            if (_circularQueue == null)
                base.Dequeue();
            try
            {
                return _circularQueue.Dequeue();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                throw;
            }
        }
    }
}
