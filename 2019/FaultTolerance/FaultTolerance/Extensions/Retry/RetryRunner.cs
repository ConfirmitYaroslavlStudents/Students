using System;

namespace FaultTolerance.Extensions.Retry
{
    internal class RetryRunner
    {
        private readonly int _retryCount;

        public RetryRunner(int retryCount)
        {
            _retryCount = retryCount;
        }

        public void Execute(Action action)
        {
            var tryCount = 0;

            while (true)
            {
                try
                {
                    action();

                    return;
                }
                catch (Exception ex)
                {
                    var canRetry = tryCount < _retryCount;
                    if (!canRetry)
                    {
                        throw ex;
                    }
                }

                ++tryCount;
            }
        }
    }
}