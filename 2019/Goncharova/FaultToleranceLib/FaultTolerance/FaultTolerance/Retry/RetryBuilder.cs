namespace FaultTolerance.Retry
{
    public static class RetryBuilder
    {
        public static RetryStrategy Retry(this StrategyBuilder strategyBuilder, int retryCount)
            => new RetryStrategy(strategyBuilder, retryCount);
    }
}
