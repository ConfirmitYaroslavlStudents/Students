using FaultTolerance;
using FaultTolerance.Retry;
using System;
using Xunit;

namespace FaultToleranceTests
{
    public class RetryTests
    {
        [Fact]
        public void Count_LessThanZero_ShouldTrow()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => 
                Strategy.Handle<DivideByZeroException>().Retry(-1));
        }

        [Fact]
        public void Retry_HandledExceptionThrownPermittedRetryCountTimes_ShouldNotTrow()
        {
            int count = 3;
            var strategy = Strategy.Handle<InvalidCastException>().Retry(count);

            strategy.ExecuteActionThrows<InvalidCastException>(count);
        }

        [Fact]
        public void Retry_HandledExceptionThrownMoreThanPermittedRetryCountTimes_ShouldTrow()
        {
            int count = 3;
            var strategy = Strategy.Handle<InvalidCastException>().Retry(count);

            Assert.Throws<InvalidCastException>(() => 
                strategy.ExecuteActionThrows<InvalidCastException>(count + 1));
        }

        [Fact]
        public void Retry_HandledExceptionThrownLessThanPermittedRetryCountTimes_ShouldNotTrow()
        {
            int count = 3;
            var strategy = Strategy.Handle<InvalidCastException>().Retry(count);

            strategy.ExecuteActionThrows<InvalidCastException>(count - 1);
        }
        [Fact]
        public void Retry_OneOfTheHandledExceptionsThrownPermittedRetryCountTimes_ShouldNotTrow()
        {
            int count = 3;
            var strategy = Strategy
                .Handle<InvalidCastException>()
                .Handle<InvalidOperationException>()
                .Retry(count);

            strategy.ExecuteActionThrows<InvalidCastException>(count);
        }

        [Fact]
        public void Retry_OneOfTheHandledExceptionsThrownMoreThanPermittedRetryCountTimes_ShouldTrow()
        {
            int count = 3;
            var strategy = Strategy
                .Handle<InvalidCastException>()
                .Handle<InvalidOperationException>()
                .Retry(count);

            Assert.Throws<InvalidCastException>(() => 
                strategy.ExecuteActionThrows<InvalidCastException>(count + 1));
        }

        [Fact]
        public void Retry_OneOfTheHandledExceptionsThrownLessThanPermittedRetryCountTimes_ShouldNotTrow()
        {
            int count = 3;
            var strategy = Strategy
                .Handle<InvalidCastException>()
                .Handle<InvalidOperationException>()
                .Retry(count);

            strategy.ExecuteActionThrows<InvalidCastException>(count - 1);
        }

        [Fact]
        public void Retry_NotHandledException_ShouldTrow()
        {
            var strategy = Strategy.Handle<InvalidCastException>().Retry(3);

            Assert.Throws<DivideByZeroException>(() =>
                strategy.Execute(() =>
                {
                    throw new DivideByZeroException();
                }));
        }

        [Fact]
        public void Retry_NotHandledExceptionsList_ShouldTrow()
        {
            var strategy = Strategy.Handle<InvalidCastException>().Handle<InvalidOperationException>().Retry(3);

            Assert.Throws<DivideByZeroException>(() =>
                strategy.Execute(() =>
                {
                    throw new DivideByZeroException();
                }));
        }
    }
}
