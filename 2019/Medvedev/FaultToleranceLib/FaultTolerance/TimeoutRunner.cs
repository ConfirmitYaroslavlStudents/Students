using System;
using System.Threading.Tasks;

namespace FaultTolerance
{
    public class TimeoutRunner : IRunner
    {
        public int Timeout
        {
            get => _timeout;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Timeout must be positive");
                _timeout = value;
            }
        }

        public TimeoutRunner(int millisecondsTimeout)
        {
            Timeout = millisecondsTimeout;
        }

        public bool Run(Action action)
        {
            var task = new Task(action);
            task.Start();
            try
            {
                return task.Wait(Timeout);
            }
            catch (AggregateException e)
            {
                throw e.InnerExceptions[0];
            }
        }

        private int _timeout;
    }
}