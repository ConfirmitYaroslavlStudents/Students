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
            Assert.Throws<ArgumentOutOfRangeException>(
                () => Strategy.Handle<DivideByZeroException>().Retry(-1));
        }

        [Fact]
        public void Retry_HandledExceptionThrownPermittedRetryCountTimes_ShouldNotTrow()
        {
            var strategy = Strategy.Handle<InvalidCastException>().Retry(3);
            var helper = new Helper(new InvalidCastException(), 3);

            strategy.Execute(() => helper.ThrowException());
        }

        [Fact]
        public void Retry_HandledExceptionThrownMoreThanPermittedRetryCountTimes_ShouldTrow()
        {
            var strategy = Strategy.Handle<InvalidCastException>().Retry(3);
            var helper = new Helper(new InvalidCastException(), 3 + 1);

            Assert.Throws<InvalidCastException>(() => strategy.Execute(() => helper.ThrowException()));
        }

        [Fact]
        public void Retry_HandledExceptionThrownLessThanPermittedRetryCountTimes_ShouldNotTrow()
        {
            var strategy = Strategy.Handle<InvalidCastException>().Retry(3);
            var helper = new Helper(new InvalidCastException(), 3 - 1);

            strategy.Execute(() => helper.ThrowException());
        }
        [Fact]
        public void Retry_OneOfTheHandledExceptionsThrownPermittedRetryCountTimes_ShouldNotTrow()
        {
            var strategy = Strategy.Handle<InvalidCastException>().Handle<InvalidOperationException>().Retry(3);
            var helper = new Helper(new InvalidCastException(), 3);

            strategy.Execute(() => helper.ThrowException());
        }

        [Fact]
        public void Retry_OneOfTheHandledExceptionsThrownMoreThanPermittedRetryCountTimes_ShouldTrow()
        {
            var strategy = Strategy.Handle<InvalidCastException>().Handle<InvalidOperationException>().Retry(3);
            var helper = new Helper(new InvalidCastException(), 3 + 1);

            Assert.Throws<InvalidCastException>(() => strategy.Execute(() => helper.ThrowException()));
        }

        [Fact]
        public void Retry_OneOfTheHandledExceptionsThrownLessThanPermittedRetryCountTimes_ShouldNotTrow()
        {
            var strategy = Strategy.Handle<InvalidCastException>().Handle<InvalidOperationException>().Retry(3);
            var helper = new Helper(new InvalidCastException(), 3 - 1);

            strategy.Execute(() => helper.ThrowException());
        }

        [Fact]
        public void Retry_NotHandledException_ShouldTrow()
        {
            var strategy = Strategy.Handle<InvalidCastException>().Retry(3);

            Assert.Throws<DivideByZeroException>(
                () => strategy.Execute(
                    () => { throw new DivideByZeroException(); }));
        }

        [Fact]
        public void Retry_NotHandledExceptionsList_ShouldTrow()
        {
            var strategy = Strategy.Handle<InvalidCastException>().Handle<InvalidOperationException>().Retry(3);

            Assert.Throws<DivideByZeroException>(
                () => strategy.Execute(
                    () => { throw new DivideByZeroException(); }));
        }
    }
}
