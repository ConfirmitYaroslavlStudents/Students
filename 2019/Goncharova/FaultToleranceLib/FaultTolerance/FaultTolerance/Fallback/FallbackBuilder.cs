using System;

namespace FaultTolerance.Fallback
{
    public static class FallbackBuilder
    {
        public static FallbackStrategy Fallback(this StrategyBuilder strategyBuilder, Action fallbackAction)
            => new FallbackStrategy(strategyBuilder, fallbackAction);
    }
}
