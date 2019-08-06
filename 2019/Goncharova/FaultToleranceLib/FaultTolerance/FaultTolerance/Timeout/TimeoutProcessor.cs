using System;
using System.Threading;
using System.Threading.Tasks;

namespace FaultTolerance.Timeout
{
    internal class TimeoutProcessor
    {
        internal static TResult Execute<TResult>(
            Func<CancellationToken, TResult> action,
            int timeoutInMilliseconds)
        {
            using (var tokenSource = new CancellationTokenSource())
            {
                CancellationToken cancelationToken = tokenSource.Token;

                try
                {
                    tokenSource.CancelAfter(timeoutInMilliseconds);

                    return action(cancelationToken);
                }
                catch (Exception)
                {
                    if(cancelationToken.IsCancellationRequested)
                    {
                        throw new TimeoutException();
                    }

                    throw;
                }
            }
        }
    }
}
