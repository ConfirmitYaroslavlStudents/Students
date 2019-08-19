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
                () => BuildTolerance.Timeout(-1));
        }

        [Fact]
        public void TimeoutInMilliseconds_EqualsZero_ShouldTrow()
        {
            Assert.Throws<ArgumentOutOfRangeException>(paramName: "timeoutInMilliseconds",
                () => BuildTolerance.Timeout(0));
        }

        [Fact]
        public void Timeout_ProvidedTimeoutIsNotEnough_ShouldTrow()
        {
            int timeout = 100;
            var tolerance = BuildTolerance.Timeout(timeout);

            void act()
            {
                tolerance.Execute(ct =>
                {
                    TestHelper.Sleep(TimeSpan.FromMilliseconds(timeout * 2), ct);
                });
            }
            Assert.Throws<TimeoutException>(act);
        }

        [Fact]
        public void Timeout_ProvidedTimeoutIsEnough_ShouldNotTrow()
        {
            int timeout = 100;
            var tolerance = BuildTolerance.Timeout(timeout * 2);

            tolerance.Execute(ct =>
                {
                    TestHelper.Sleep(TimeSpan.FromMilliseconds(timeout), ct);
                });
        }

        [Fact]
        public void Timeout_ActionTrowsException_ShouldTrow()
        {
            var tolerance = BuildTolerance.Timeout(100);

            Assert.Throws<NotImplementedException>(() =>
                tolerance.Execute(() =>
                {
                    throw new NotImplementedException();
                }));
        }


    }
}
