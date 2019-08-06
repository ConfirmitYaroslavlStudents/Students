using FaultTolerance;
using System;
using Xunit;

namespace FaultToleranceTests
{
    public class TimeoutTests
    {
        [Fact]
        public void TimeoutInMilliseconds_LessThanZero_ShouldTrow()
        {
            Assert.Throws<ArgumentOutOfRangeException>(paramName: "timeoutInMilliseconds",
                () => Strategy.Timeout(-1));
        }

        [Fact]
        public void TimeoutInMilliseconds_EqualsZero_ShouldTrow()
        {
            Assert.Throws<ArgumentOutOfRangeException>(paramName: "timeoutInMilliseconds",
                () => Strategy.Timeout(0));
        }

        [Fact]
        public void Timeout_ProvidedTimeoutIsNotEnough_ShouldTrow()
        {
            int timeout = 100;
            var strategy = Strategy.Timeout(timeout);

            void act()
            {
                strategy.Execute(ct =>
                {
                    Helper.Sleep(TimeSpan.FromMilliseconds(timeout * 2), ct);
                });
            }
            Assert.Throws<TimeoutException>(act);
        }

        [Fact]
        public void Timeout_ProvidedTimeoutIsEnough_ShouldNotTrow()
        {
            int timeout = 100;
            var strategy = Strategy.Timeout(timeout * 2);

            strategy.Execute(ct =>
                {
                    Helper.Sleep(TimeSpan.FromMilliseconds(timeout), ct);
                });
        }

        [Fact]
        public void Timeout_ActionTrowsException_ShouldTrow()
        {
            var strategy = Strategy.Timeout(100);

            Assert.Throws<NotImplementedException>(
                () => { strategy.Execute(() => { throw new NotImplementedException(); }); });
        }


    }
}
