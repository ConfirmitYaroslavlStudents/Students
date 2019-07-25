using FaultTolerance.Retry;
using System;
using System.Collections.Generic;
using Xunit;

namespace FaultToleranceTests
{
    public class RetryTests
    {
        [Fact]
        public void Count_LessThanZero_ShouldTrow()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new RetryStrategy(new DivideByZeroException(), -1));
        }

        [Fact]
        public void ExceptionParam_IsNull_ShouldTrow()
        {
            Assert.Throws<ArgumentNullException>(
                () => new RetryStrategy(exception: null, 3));
        }

        [Fact]
        public void ExceptionsListParam_IsNull_ShouldTrow()
        {
            Assert.Throws<ArgumentNullException>(
                () => new RetryStrategy(exceptions: null, 3));
        }

        [Fact]
        public void Retry_HandledExceptionThrownPermittedRetryCountTimes_ShouldNotTrow()
        {
            var strategy = new RetryStrategy(new InvalidCastException(), 3);
            var helper = new Helper(new InvalidCastException(), 3);

            strategy.Execute(() => helper.ThrowException());
        }

        [Fact]
        public void Retry_HandledExceptionThrownMoreThanPermittedRetryCountTimes_ShouldTrow()
        {
            var strategy = new RetryStrategy(new InvalidCastException(), 3);
            var helper = new Helper(new InvalidCastException(), 3 + 1);

            Assert.Throws<InvalidCastException>(() => strategy.Execute(() => helper.ThrowException()));
        }

        [Fact]
        public void Retry_HandledExceptionThrownLessThanPermittedRetryCountTimes_ShouldNotTrow()
        {
            var strategy = new RetryStrategy(new InvalidCastException(), 3);
            var helper = new Helper(new InvalidCastException(), 3 - 1);

            strategy.Execute(() => helper.ThrowException());
        }
        [Fact]
        public void Retry_OneOfTheHandledExceptionsThrownPermittedRetryCountTimes_ShouldNotTrow()
        {
            List<Exception> exceptions = new List<Exception>() {
                new InvalidCastException(), new InvalidOperationException() };
            var strategy = new RetryStrategy(exceptions, 3);
            var helper = new Helper(new InvalidCastException(), 3);

            strategy.Execute(() => helper.ThrowException());
        }

        [Fact]
        public void Retry_OneOfTheHandledExceptionsThrownMoreThanPermittedRetryCountTimes_ShouldTrow()
        {
            List<Exception> exceptions = new List<Exception>() {
                new InvalidCastException(), new InvalidOperationException() };
            var strategy = new RetryStrategy(exceptions, 3);
            var helper = new Helper(new InvalidCastException(), 3 + 1);

            Assert.Throws<InvalidCastException>(() => strategy.Execute(() => helper.ThrowException()));
        }

        [Fact]
        public void Retry_OneOfTheHandledExceptionsThrownLessThanPermittedRetryCountTimes_ShouldNotTrow()
        {
            List<Exception> exceptions = new List<Exception>() {
                new InvalidCastException(), new InvalidOperationException() };
            var strategy = new RetryStrategy(exceptions, 3);
            var helper = new Helper(new InvalidCastException(), 3 - 1);

            strategy.Execute(() => helper.ThrowException());
        }

        [Fact]
        public void Retry_NotHandledException_ShouldTrow()
        {
            var strategy = new RetryStrategy(new InvalidCastException(), 3);

            Assert.Throws<DivideByZeroException>(
                () => strategy.Execute(
                    () => { throw new DivideByZeroException(); }));
        }

        [Fact]
        public void Retry_NotHandledExceptionsList_ShouldTrow()
        {
            List<Exception> exceptions = new List<Exception>() {
                new InvalidCastException(), new InvalidOperationException() };
            var strategy = new RetryStrategy(exceptions, 3);

            Assert.Throws<DivideByZeroException>(
                () => strategy.Execute(
                    () => { throw new DivideByZeroException(); }));
        }
    }
}
