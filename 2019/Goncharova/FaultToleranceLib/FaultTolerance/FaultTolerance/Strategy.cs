using System;
using System.Threading;

namespace FaultTolerance
{
    public abstract partial class Strategy
    {
        public static StrategyBuilder Handle<T>() where T : Exception
        {
            return new StrategyBuilder(typeof(T));
        }

        public abstract void Execute(Action<CancellationToken> action);

        public void Execute(Action action)
        {
            Execute((ct) => { action(); });
        }

    }
    
}
