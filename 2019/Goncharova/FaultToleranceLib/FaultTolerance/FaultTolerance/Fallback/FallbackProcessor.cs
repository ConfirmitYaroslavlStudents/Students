using System;
using System.Threading;

namespace FaultTolerance.Fallback
{
    internal static class FallbackProcessor
    {
        internal static void Execute(
            Action<CancellationToken> action,
            StrategyExceptions exceptions,
            Action fallbackAction)
        {
            try
            {
                action(CancellationToken.None);
                return;
            }
            catch (Exception ex)
            {
                if (!exceptions.Contains(ex))
                {
                    throw;
                }
            }

            fallbackAction();
        }
    }
}
