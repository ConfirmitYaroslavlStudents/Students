using System;
using System.IO;

namespace CircularQueue
{
    public class CircularQueueExceptionFileLogger<T>:CircularQueue<T>
    {
        private readonly CircularQueue<T> _circularQueue;

        public CircularQueueExceptionFileLogger(CircularQueue<T> circularQueue)
        {
            _circularQueue = circularQueue;
        }

        public CircularQueueExceptionFileLogger(int capacity = DefaultSize) : base(capacity)
        { }

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
                using (var writer = new StreamWriter("ExceptionLogger.txt",true))
                {
                    writer.WriteLine(e.Message);
                }
                throw;
            }
        }
    }
}
