using System;
using System.Threading;

namespace FaultTolerance.Timeout
{
    public class TimeoutTolerance : Tolerance
    {
        private readonly int timeout;

        public TimeoutTolerance(int timeoutInMilliseconds)
        {
            if (timeoutInMilliseconds <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(timeoutInMilliseconds),
                    "Timeout should be positive");
            }
            timeout = timeoutInMilliseconds;
        }

        public override void Execute(Action<CancellationToken> action)
            => TimeoutProcessor.Execute(_ => action(_),
                                        timeout);
    }
}
