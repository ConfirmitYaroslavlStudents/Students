using System;

namespace FaultTolerance.Retry
{
    internal static class RetryProcessor
    {
        internal static TResult Execute<TResult>(
            Func<TResult> action,
            StrategyExceptions exceptions,
            int permittedRetryCount)
        {
            Exception lastException = null;
            for (int retryCount = 0; retryCount <= permittedRetryCount; retryCount++)
            {
                try
                {
                    return action();
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    if (!exceptions.Contains(ex.GetType()))
                    {
                        throw;
                    }
                }
            }
            throw lastException;
        }
    }
}
