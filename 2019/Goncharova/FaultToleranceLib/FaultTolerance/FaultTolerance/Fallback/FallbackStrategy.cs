using System;

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

        public override void Execute(Action action)
        {
            FallbackProcessor.Execute<object>(
                () => { action(); return null; },
                configuredExceptions,
                () => { FallbackAction(); return null; }
                );
        }

        public override T Execute<T>(Func<T> action)
        {
            throw new InvalidOperationException("Using func methods currently is not supported");
        }
    }
}