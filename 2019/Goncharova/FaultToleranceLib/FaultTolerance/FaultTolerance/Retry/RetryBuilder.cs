namespace FaultTolerance.Retry
{
    public static class RetryBuilder
    {
        public static RetryTolerance Retry(this ToleranceBuilder ToleranceBuilder, int retryCount)
            => new RetryTolerance(ToleranceBuilder, retryCount);
    }
}
