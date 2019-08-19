using System;

namespace FaultTolerance.Fallback
{
    public static class FallbackBuilder
    {
        public static FallbackTolerance Fallback(this ToleranceBuilder ToleranceBuilder, Action fallbackAction)
            => new FallbackTolerance(ToleranceBuilder, fallbackAction);
    }
}
