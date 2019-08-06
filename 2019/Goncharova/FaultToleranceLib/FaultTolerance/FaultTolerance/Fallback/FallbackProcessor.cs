using System;
using System.Threading;

namespace FaultTolerance.Fallback
{
    internal static class FallbackProcessor
    {
        internal static TResult Execute<TResult>(
            Func<CancellationToken, TResult> action,
            StrategyExceptions exceptions,
            Func<TResult> fallbackAction)
        {
            try
            {
                return action(CancellationToken.None);
            }
            catch (Exception ex)
            {
                if (!exceptions.Contains(ex.GetType()))
                {
                    throw;
                }
            }

            return fallbackAction();
        }
    }
}
