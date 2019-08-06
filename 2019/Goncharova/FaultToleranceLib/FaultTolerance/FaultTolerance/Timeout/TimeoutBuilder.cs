using FaultTolerance.Timeout;

namespace FaultTolerance
{
    public partial class Strategy
    {
        public static TimeoutStrategy Timeout(int timeoutInMilliseconds)
            => new TimeoutStrategy(timeoutInMilliseconds);
    }
}
