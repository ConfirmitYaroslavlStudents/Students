using System;
using System.Threading;

namespace FaultTolerance.Retry
{
    public class RetryTolerance : Tolerance
    {
        private int permittedRetryCount;
        private readonly ToleranceExceptions configuredExceptions;

        public RetryTolerance(ToleranceBuilder ToleranceBuilder, int retryCount)
        {
            configuredExceptions = ToleranceBuilder.configuredExceptions;
            PermittedRetryCount = retryCount;
        }

        public int PermittedRetryCount
        {
            get => permittedRetryCount;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("RetryCount should be non negative");
                }
                else
                {
                    permittedRetryCount = value;
                }
            }
        }

        public override void Execute(Action<CancellationToken> action)
            => RetryProcessor.Execute(_ => action(_),
                                      configuredExceptions,
                                      PermittedRetryCount);
    }

}
