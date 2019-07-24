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

            Assert.Throws<NotImplementedException>(() => FaultTolerance.Try(typeof(NotImplementedException), action, 2));
        }

        [Fact]
        public void TryFallback_Failed_RunFallback()
        {
            bool fallbackHadRun = false;

            Action action = () => throw new NotImplementedException();
            Action fallback = () => fallbackHadRun = true;

            FaultTolerance.TryFallback(typeof(NotImplementedException), action, 2, fallback);

            Assert.True(fallbackHadRun);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public void Try_Succeed_NoExceptionThrown(int d)
        {
            int count = 1;
            Action action = () => {
                if (count < d)
                {
                    count++;
                    throw new ArithmeticException();
                }
            };

            FaultTolerance.Try(typeof(ArithmeticException), action, 3);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public void TryFallback_Succed_NoFallbackRun(int d)
        {
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

            FaultTolerance.TryFallback(typeof(ArithmeticException), action, 3, fallback);

            Assert.False(fallbackHadRun);
        }
    }
}
