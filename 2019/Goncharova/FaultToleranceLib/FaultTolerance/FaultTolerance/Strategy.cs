using System;
using System.Collections.Generic;

namespace FaultTolerance
{
    public abstract class Strategy : StrategyModel
    {
        internal Strategy(Exception exception) : base(exception) { }
        internal Strategy(List<Exception> exceptions) : base(exceptions) { }

        public virtual void Execute(Action action)
        {
            Execute<object>(() => { action(); return null; });
        }
        public abstract T Execute<T>(Func<T> action);

    }
    public abstract class Policy<TResult> : StrategyModel
    {
        internal Policy(Exception exception) : base(exception) { }
        internal Policy(List<Exception> exceptions) : base(exceptions) { }

        public abstract TResult Execute(Func<TResult> action);

    }
}
