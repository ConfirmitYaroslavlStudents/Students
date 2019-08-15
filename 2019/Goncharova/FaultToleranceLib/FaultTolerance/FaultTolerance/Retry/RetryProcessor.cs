using System;
using System.Threading;

namespace FaultTolerance.Retry
{
    internal static class RetryProcessor
    {
        internal static void Execute(
            Action<CancellationToken> action,
            ToleranceExceptions exceptions,
            int permittedRetryCount)
        {
            Exception finalException;
            int maxTryCount = permittedRetryCount + 1;

            do
            {
                maxTryCount--;
                try
                {
                    action(CancellationToken.None);
                    return;
                }
                catch (Exception ex)
                {
                    finalException = ex;
                    if (!exceptions.Contains(ex))
                    {
                        throw;
                    }
                }
            }
            while (maxTryCount > 0);

            throw finalException;
        }
    }
}
