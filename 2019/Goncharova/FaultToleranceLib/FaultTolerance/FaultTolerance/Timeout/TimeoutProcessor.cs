using System;
using System.Threading;

namespace FaultTolerance.Timeout
{
    internal class TimeoutProcessor
    {
        internal static void Execute(
            Action<CancellationToken> action,
            int timeoutInMilliseconds)
        {
            using (var tokenSource = new CancellationTokenSource())
            {
                CancellationToken cancelationToken = tokenSource.Token;

                try
                {
                    tokenSource.CancelAfter(timeoutInMilliseconds);

                    action(cancelationToken);
                }
                catch (Exception)
                {
                    if (cancelationToken.IsCancellationRequested)
                    {
                        throw new TimeoutException();
                    }

                    throw;
                }
            }
        }
    }
}
