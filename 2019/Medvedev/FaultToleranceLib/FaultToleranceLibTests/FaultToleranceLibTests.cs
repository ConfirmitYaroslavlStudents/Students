using System;
using Xunit;

namespace FaultToleranceLib.Tests
{
    public class FaultToleranceLibTests
    {
        [Fact]
        public void Try_Failed_ThrowException()
        {
            Action action = () => throw new NotImplementedException();

            FaultTolerance faultTolerance = new FaultTolerance();

            Assert.Throws<NotImplementedException>(() => faultTolerance.Try(typeof(NotImplementedException), action, 2));
        }

        [Fact]
        public void TryFallback_Failed_RunFallback()
        {
            bool fallbackHadRun = false;

            Action action = () => throw new NotImplementedException();
            Action fallback = () => fallbackHadRun = true;

            FaultTolerance faultTolerance = new FaultTolerance();

            faultTolerance.TryFallback(typeof(NotImplementedException), action, 2, fallback);

            Assert.True(fallbackHadRun);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public void Try_Succeed_NoExceptionThrown(int d)
        {
            FaultTolerance faultTolerance = new FaultTolerance();

            int count = 1;
            Action action = () => {
                if (count < d)
                {
                    count++;
                    throw new ArithmeticException();
                }
            };

            faultTolerance.Try(typeof(ArithmeticException), action, 3);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public void TryFallback_Succed_NoFallbackRun(int d)
        {
            FaultTolerance faultTolerance = new FaultTolerance();
            bool fallbackHadRun = false;

            int count = 1;
            Action action = () => {
                if (count < d)
                {
                    count++;
                    throw new ArithmeticException();
                }
            };

            Action fallback = () => fallbackHadRun = true;

            faultTolerance.TryFallback(typeof(ArithmeticException), action, 3, fallback);

            Assert.False(fallbackHadRun);
        }
    }
}
