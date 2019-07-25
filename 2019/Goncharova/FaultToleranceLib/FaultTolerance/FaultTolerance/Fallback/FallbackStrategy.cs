using System;
using System.Collections.Generic;

namespace FaultTolerance.Fallback
{
    public class FallbackStrategy : Strategy
    {
        public FallbackStrategy(Exception exception, Action fallbackAction) : base(exception)
        {
            FallbackAction = fallbackAction;
        }
        public FallbackStrategy(List<Exception> exceptions, Action fallbackAction) : base(exceptions)
        {
            FallbackAction = fallbackAction;
        }

        private Action FallbackAction
        {
            get => FallbackAction;
            set
            {
                FallbackAction = value ?? throw new ArgumentNullException("Fallback action can't be null");
            }
        }

        public override void Execute(Action action)
        {
            FallbackProcessor.Execute<object>(
                () => { action(); return null; },
                ExceptionsHandled,
                () => { FallbackAction(); return null; }
                );
        }

        public override T Execute<T>(Func<T> action)
        {
            throw new InvalidOperationException("Use generic version of the policy to call generic method");
        }
    }

    internal class FallbackPolicy<TResult> : Policy<TResult>
    {
        public FallbackPolicy(Exception exception, Func<TResult> fallbackAction) : base(exception)
        {
            FallbackAction = fallbackAction;
        }
        public FallbackPolicy(List<Exception> exceptions, Func<TResult> fallbackAction) : base(exceptions)
        {
            FallbackAction = fallbackAction;
        }

        private Func<TResult> FallbackAction
        {
            get => FallbackAction;
            set
            {
                FallbackAction = value ?? throw new ArgumentNullException("Fallback action can't be null");
            }
        }

        public override TResult Execute(Func<TResult> action)
        {
            return FallbackProcessor.Execute<TResult>(action, ExceptionsHandled, FallbackAction);
        }

    }
}