using FaultTolerance;
using System;
using System.Threading;

namespace FaultToleranceTests
{
    public static class TestHelper
    {
        public static void ExecuteActionThrows<TException>(this Tolerance Tolerance, int count) where TException : Exception, new()
        {
            int thrownCount = 0;

            Tolerance.Execute(() =>
            {
                if (thrownCount < count)
                {
                    thrownCount++;
                    throw new TException();
                }
            });
        }

        public static Action<TimeSpan, CancellationToken> Sleep = (timeSpan, cancellationToken) =>
        {
            if (cancellationToken.WaitHandle.WaitOne(timeSpan))
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
        };

    }
}
