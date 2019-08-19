using System;
using System.Threading;

namespace FaultTolerance.Fallback
{
    public class FallbackTolerance : Tolerance
    {
        private readonly Action fallbackAction;
        private readonly ToleranceExceptions configuredExceptions;

        internal FallbackTolerance(ToleranceExceptions exceptions, Action fallback)
        {
            configuredExceptions = exceptions;
            fallbackAction = fallback ?? throw new ArgumentNullException(nameof(fallback));
        }

        public override void Execute(Action<CancellationToken> action)
            => FallbackProcessor.Execute(_ => action(_), 
                                         configuredExceptions, 
                                         () => fallbackAction());
    }
}