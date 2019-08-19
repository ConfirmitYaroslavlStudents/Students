using FaultTolerance.Plain;
using FaultTolerance.Timeout;
using System;

namespace FaultTolerance
{
    public static class BuildTolerance
    {
        public static ToleranceBuilder Handle<TException>() where TException : Exception
            => new ToleranceBuilder(exception => exception is TException);

        public static PlainTolerance Plain() => new PlainTolerance();

        public static TimeoutTolerance Timeout(int timeoutInMilliseconds)
            => new TimeoutTolerance(timeoutInMilliseconds);

    }
}
