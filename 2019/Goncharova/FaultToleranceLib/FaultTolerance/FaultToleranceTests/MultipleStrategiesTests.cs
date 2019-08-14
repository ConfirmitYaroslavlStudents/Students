using FaultTolerance;
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
            int count = 3;
            var retryStrategy = Strategy.Handle<InvalidCastException>().Retry(count);

            bool fallbackActionIsRun = false;
            void fallbackAction() { fallbackActionIsRun = true; }

            var fallbackStrategy = Strategy
                .Handle<InvalidCastException>()
                .Fallback(fallbackAction);

            fallbackStrategy.Execute(() =>
                retryStrategy.ExecuteActionThrows<InvalidCastException>(count + 1));

            Assert.True(fallbackActionIsRun);
        }

        [Fact]
        public void FallbackAction_RetriesDoesNotFail_ShouldNotBeRun()
        {
            var retryStrategy = Strategy.Handle<InvalidCastException>().Retry(3);

            bool fallbackActionIsRun = false;
            void fallbackAction() { fallbackActionIsRun = true; }

            var fallbackStrategy = Strategy
                .Handle<InvalidCastException>()
                .Fallback(fallbackAction);

            fallbackStrategy.Execute(
                () => retryStrategy.ExecuteActionThrows<InvalidCastException>(count: 1));

            Assert.False(fallbackActionIsRun);
        }

        [Fact]
        public void FallbackAction_RetryDoesNotHandleExceptionButFallbackDoes_ShouldBeRun()
        {
            var retryStrategy = Strategy.Handle<InvalidCastException>().Retry(3);

            bool fallbackActionIsRun = false;
            void fallbackAction() { fallbackActionIsRun = true; }

            var fallbackStrategy = Strategy
                .Handle<DivideByZeroException>()
                .Fallback(fallbackAction);

            fallbackStrategy.Execute(() =>
                retryStrategy.Execute(() =>
                {
                    throw new DivideByZeroException();
                }));

            Assert.True(fallbackActionIsRun);
        }

        [Fact]
        public void RetryAndFallback_BothDoNotHandleException_ShouldThrow()
        {
            var retryStrategy = Strategy.Handle<InvalidCastException>().Retry(3);

            void fallbackAction() { }
            var fallbackStrategy = Strategy
                .Handle<DivideByZeroException>()
                .Fallback(fallbackAction);

            void fallbackAfterRetry() => fallbackStrategy.Execute(() =>
                    retryStrategy.Execute(() =>
                    {
                        throw new IndexOutOfRangeException();
                    }));

            Assert.Throws<IndexOutOfRangeException>(fallbackAfterRetry);
        }

        [Fact]
        public void FallbackAction_FallbackHandlesException_ShouldBeRun()
        {
            var retryStrategy = Strategy.Handle<InvalidCastException>().Retry(3);

            bool fallbackActionIsRun = false;
            void fallbackAction() { fallbackActionIsRun = true; }

            var fallbackStrategy = Strategy
                .Handle<DivideByZeroException>()
                .Fallback(fallbackAction);

            retryStrategy.Execute(() =>
                    fallbackStrategy.Execute(() =>
                    {
                        throw new DivideByZeroException();
                    }));

            Assert.True(fallbackActionIsRun);
        }

    }
}
