using System;
using System.Threading;

namespace FaultTolerance.Retry
{
    internal static class RetryProcessor
    {
        internal static TResult Execute<TResult>(
            Func<CancellationToken, TResult> action,
            StrategyExceptions exceptions,
            int permittedRetryCount)
        {
            Exception finalException = null;
            for (int retryCount = 0; retryCount <= permittedRetryCount; retryCount++)
            {
                try
                {
                    return action(CancellationToken.None);
                }
                catch (Exception ex)
                {
                    finalException = ex;
                    if (!exceptions.Contains(ex.GetType()))
                    {
                        throw;
                    }
                }
            }
            throw finalException;
        }
    }
}
