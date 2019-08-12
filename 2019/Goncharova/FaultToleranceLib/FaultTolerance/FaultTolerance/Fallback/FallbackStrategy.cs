using System;
using System.Threading;

namespace FaultTolerance.Fallback
{
    public class FallbackStrategy : Strategy
    {
        private Action fallbackAction;
        private readonly StrategyExceptions configuredExceptions;

        public FallbackStrategy(StrategyBuilder strategyBuilder, Action fallbackAction)
        {
            configuredExceptions = strategyBuilder.configuredExceptions;
            FallbackAction = fallbackAction;
        }

        private Action FallbackAction
        {
            get => fallbackAction;
            set
            {
                fallbackAction = value ?? throw new ArgumentNullException("Fallback action can't be null");
            }
        }

        public override void Execute(Action<CancellationToken> action)
            => FallbackProcessor.Execute(_ => action(_), 
                                         configuredExceptions, 
                                         () => FallbackAction());
    }
}