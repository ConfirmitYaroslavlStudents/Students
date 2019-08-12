using FaultTolerance.Extensions.Fallback;
using FaultTolerance.Extensions.Retry;

namespace FaultTolerance.Extensions
{
    public static class Builder
    {
        public static FallbackBuilder BuildFallback() => new FallbackBuilder();

        public static RetryBuilder BuildRetry(int retryCount = 1) => new RetryBuilder(retryCount);
    }
}