using System;

namespace FaultTolerance
{
    public abstract partial class Strategy
    {
        public static StrategyBuilder Handle<T>() where T : Exception
        {
            return new StrategyBuilder(typeof(T));
        }

        public virtual void Execute(Action action)
        {
            Execute<object>(() => { action(); return null; });
        }

        public abstract T Execute<T>(Func<T> action);

    }
    
}
