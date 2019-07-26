using FaultTolerance.Fallback;
using FaultTolerance.Retry;
using System;
using Xunit;

namespace FaultToleranceTests
{
    public class MultipleStrategiesTests
    {
        [Fact]
        public void FallbackAction_RetriesFail_ShouldBeRun()
        {
            var retryStrategy = new RetryStrategy(new InvalidCastException(), 3);
            var helper = new Helper(new InvalidCastException(), 3 + 1);

            bool fallbackActionIsRun = false;
            void fallbackAction() { fallbackActionIsRun = true; }

            var fallbackStrategy = new FallbackStrategy(new InvalidCastException(), fallbackAction);
            fallbackStrategy.Execute(
                () => { retryStrategy.Execute(
                () => helper.ThrowException());
                });

            Assert.True(fallbackActionIsRun);
        }

        [Fact]
        public void FallbackAction_RetriesDoesNotFail_ShouldNotBeRun()
        {
            var retryStrategy = new RetryStrategy(new InvalidCastException(), 3);
            var helper = new Helper(new InvalidCastException(), 1);

            bool fallbackActionIsRun = false;
            void fallbackAction() { fallbackActionIsRun = true; }

            var fallbackStrategy = new FallbackStrategy(new InvalidCastException(), fallbackAction);
            fallbackStrategy.Execute(
                () => { retryStrategy.Execute(
                () => helper.ThrowException());
                });

            Assert.False(fallbackActionIsRun);
        }

        [Fact]
        public void FallbackAction_RetryDoesNotHandleExceptionButFallbackDoes_ShouldBeRun()
        {
            var retryStrategy = new RetryStrategy(new InvalidCastException(), 3);
            
            bool fallbackActionIsRun = false;
            void fallbackAction() { fallbackActionIsRun = true; }

            var fallbackStrategy = new FallbackStrategy(new DivideByZeroException(), fallbackAction);
            fallbackStrategy.Execute(
                () => {
                    retryStrategy.Execute(
            () => { throw new DivideByZeroException(); });
                });

            Assert.True(fallbackActionIsRun);
        }

        [Fact]
        public void RetryAndFallback_BothDoNotHandleException_ShouldThrow()
        {
            var retryStrategy = new RetryStrategy(new InvalidCastException(), 3);

            void fallbackAction() { }

            var fallbackStrategy = new FallbackStrategy(new DivideByZeroException(), fallbackAction);
            void fallbackAfterRetry() => fallbackStrategy.Execute(
                () =>
                {
                    retryStrategy.Execute(
            () => { throw new IndexOutOfRangeException(); });
                });

            Assert.Throws<IndexOutOfRangeException>(fallbackAfterRetry);
        }

        [Fact]
        public void FallbackAction_FallbackHandlesException_ShouldBeRun()
        {
            var retryStrategy = new RetryStrategy(new InvalidCastException(), 3);

            bool fallbackActionIsRun = false;
            void fallbackAction() { fallbackActionIsRun = true; }

            var fallbackStrategy = new FallbackStrategy(new DivideByZeroException(), fallbackAction);
            retryStrategy.Execute(
                () => {
                    fallbackStrategy.Execute(
            () => { throw new DivideByZeroException(); });
                });

            Assert.True(fallbackActionIsRun);
        }

    }
}
