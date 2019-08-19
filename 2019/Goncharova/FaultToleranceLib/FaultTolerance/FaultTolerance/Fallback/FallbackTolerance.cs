using System;
using System.Threading;

namespace FaultTolerance.Fallback
{
    public class FallbackTolerance : Tolerance
    {
        private readonly Action fallbackAction;
        private readonly ToleranceExceptions configuredExceptions;

        public FallbackTolerance(ToleranceBuilder ToleranceBuilder, Action fallback)
        {
            configuredExceptions = ToleranceBuilder.configuredExceptions;
            fallbackAction = fallback ?? throw new ArgumentNullException(nameof(fallback));
        }

        public override void Execute(Action<CancellationToken> action)
            => FallbackProcessor.Execute(_ => action(_), 
                                         configuredExceptions, 
                                         () => fallbackAction());
    }
}