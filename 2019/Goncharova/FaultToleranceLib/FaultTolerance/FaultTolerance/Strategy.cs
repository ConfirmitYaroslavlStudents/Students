using FaultTolerance.Plain;
using FaultTolerance.Timeout;
using System;
using System.Threading;

namespace FaultTolerance
{
    public abstract class Strategy
    {
        public static StrategyBuilder Handle<TException>() where TException : Exception
        {
            return new StrategyBuilder(exception => exception is TException);
        }

        public abstract void Execute(Action<CancellationToken> action);

        public void Execute(Action action)
        {
            Execute(_ => action());
        }
        public static PlainStrategy Plain() => new PlainStrategy();

        public static TimeoutStrategy Timeout(int timeoutInMilliseconds)
            => new TimeoutStrategy(timeoutInMilliseconds);

    }

}
