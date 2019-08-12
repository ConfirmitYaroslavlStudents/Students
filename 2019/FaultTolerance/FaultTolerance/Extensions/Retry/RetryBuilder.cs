using System;

namespace FaultTolerance.Extensions.Retry
{
    public class RetryBuilder : ToleranceBuilder
    {
        private int _retryCount;

        public RetryBuilder(int retryCount)
        {
            _retryCount = retryCount;
        }

        public RetryBuilder WithRetryCount(int retryCount)
        {
            _retryCount = retryCount;

            return this;
        }

        public override void Execute(Action action)
        {
            new RetryRunner(_retryCount).Execute(action);
        }
    }
}