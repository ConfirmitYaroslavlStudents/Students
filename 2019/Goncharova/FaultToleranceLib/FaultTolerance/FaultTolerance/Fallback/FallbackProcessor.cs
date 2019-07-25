using System;

namespace FaultTolerance.Fallback
{
    internal static class FallbackProcessor
    {
        internal static TResult Execute<TResult>(
            Func<TResult> action,
            StrategyExceptions exceptions,
            Func<TResult> fallbackAction)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                if (!exceptions.Contains(ex))
                {
                    throw;
                }
            }

            return fallbackAction();
        }
    }
}
