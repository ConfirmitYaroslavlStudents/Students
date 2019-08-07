using FaultTolerance;
using System;
using Xunit;

namespace FaultToleranceTest
{
    public class RetryTests
    {
        [Fact]
        public void Retry_OneTime_Success()
        {
            var count = 0;
            new Fault().Retry<Exception>(1).Launch(() => { count++; });

            Assert.Equal(1, count);
        }

        [Fact]
        public void Retry_TwoTimes_Success()
        {
            var count = 0;
            new Fault().Retry<Exception>(2).Launch(() => { count++; });

            Assert.Equal(1, count);
        }

        [Fact]
        public void Retry_OneError_Success()
        {
            var count = 0;
            new Fault().Retry<Exception>(1).Launch(() => { count++; if (count == 1) throw new Exception(); });

            Assert.Equal(2, count);
        }

        [Fact]
        public void Retry_UnexpectedError_Failure()
        {
            var count = 0;

            Assert.Throws<InvalidOperationException>(() => { new Fault().Retry<Exception>(1).Launch(() => { count++; throw new InvalidOperationException(); }); });
            Assert.Equal(1, count);
        }

        [Fact]
        public void Retry_TwoDifferentErrors_Success()
        {
            var count = 0;

            new Fault().Retry<Exception>(1).Retry<InvalidOperationException>(1).Launch(() => { count++; if(count == 1)throw new InvalidOperationException(); if (count == 2) throw new Exception(); });
            Assert.Equal(3, count);
        }
    }
}
