using System;
using System.Threading;

namespace FaultTolerance.Plain
{
    internal static class PlainProcessor
    {
        internal static TResult Execute<TResult>(Func<CancellationToken, TResult> action)
        {
            try
            {
                return action(CancellationToken.None);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
