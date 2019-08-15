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
                () => Tolerance.Timeout(-1));
        }

        [Fact]
        public void TimeoutInMilliseconds_EqualsZero_ShouldTrow()
        {
            Assert.Throws<ArgumentOutOfRangeException>(paramName: "timeoutInMilliseconds",
                () => Tolerance.Timeout(0));
        }

        [Fact]
        public void Timeout_ProvidedTimeoutIsNotEnough_ShouldTrow()
        {
            int timeout = 100;
            var Tolerance = Tolerance.Timeout(timeout);

            void act()
            {
                Tolerance.Execute(ct =>
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
            var Tolerance = Tolerance.Timeout(timeout * 2);

            Tolerance.Execute(ct =>
                {
                    TestHelper.Sleep(TimeSpan.FromMilliseconds(timeout), ct);
                });
        }

        [Fact]
        public void Timeout_ActionTrowsException_ShouldTrow()
        {
            var Tolerance = Tolerance.Timeout(100);

            Assert.Throws<NotImplementedException>(() =>
                Tolerance.Execute(() =>
                {
                    throw new NotImplementedException();
                }));
        }


    }
}
