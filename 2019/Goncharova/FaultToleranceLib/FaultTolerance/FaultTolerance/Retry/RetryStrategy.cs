using System;

namespace FaultTolerance.Retry
{
    public class RetryStrategy : Strategy
    {
        private int permittedRetryCount;
        private readonly StrategyExceptions configuredExceptions;

        public RetryStrategy(StrategyBuilder strategyBuilder, int retryCount)
        {
            configuredExceptions = strategyBuilder.configuredExceptions;
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

        public override T Execute<T>(Func<T> action)
            => RetryProcessor.Execute<T>(action, configuredExceptions, PermittedRetryCount);
    }

}
