using FaultTolerance.Plain;
using FaultTolerance.Timeout;
using System;
using System.Threading;

namespace FaultTolerance
{
    public abstract class Tolerance
    {
        public static ToleranceBuilder Handle<TException>() where TException : Exception
        {
            return new ToleranceBuilder(exception => exception is TException);
        }

        public abstract void Execute(Action<CancellationToken> action);

        public void Execute(Action action)
        {
            Execute(_ => action());
        }
        public static PlainTolerance Plain() => new PlainTolerance();

        public static TimeoutTolerance Timeout(int timeoutInMilliseconds)
            => new TimeoutTolerance(timeoutInMilliseconds);

    }

}
