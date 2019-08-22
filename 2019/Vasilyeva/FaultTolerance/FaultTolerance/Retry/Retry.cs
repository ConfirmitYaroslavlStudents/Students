using System;

namespace FaultTolerance
{
    public class Retry : FaultTolerance
    {
        public Retry(int retryCount)
        {
            this.retryCount = retryCount;
        }

        public Retry() { }

        private int retryCount;

        public Retry SetRetryCount(int retryCount)
        {
            this.retryCount = retryCount;

            return this;
        }

        public override void Execute(Action action)
        {
            for (int i = 0; i < retryCount; i++)
            {
                try
                {
                    action();

                    break;
                }
                catch
                {
                    continue;
                }
            }
        }

    }
}
