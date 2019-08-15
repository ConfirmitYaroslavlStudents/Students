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
            var retryTolerance = Tolerance.Handle<InvalidCastException>().Retry(count);

            bool fallbackActionIsRun = false;
            void fallbackAction() { fallbackActionIsRun = true; }

            var fallbackTolerance = Tolerance
                .Handle<InvalidCastException>()
                .Fallback(fallbackAction);

            fallbackTolerance.Execute(() =>
                retryTolerance.ExecuteActionThrows<InvalidCastException>(count + 1));

            Assert.True(fallbackActionIsRun);
        }

        [Fact]
        public void FallbackAction_RetriesDoesNotFail_ShouldNotBeRun()
        {
            var retryTolerance = Tolerance.Handle<InvalidCastException>().Retry(3);

            bool fallbackActionIsRun = false;
            void fallbackAction() { fallbackActionIsRun = true; }

            var fallbackTolerance = Tolerance
                .Handle<InvalidCastException>()
                .Fallback(fallbackAction);

            fallbackTolerance.Execute(
                () => retryTolerance.ExecuteActionThrows<InvalidCastException>(count: 1));

            Assert.False(fallbackActionIsRun);
        }

        [Fact]
        public void FallbackAction_RetryDoesNotHandleExceptionButFallbackDoes_ShouldBeRun()
        {
            var retryTolerance = Tolerance.Handle<InvalidCastException>().Retry(3);

            bool fallbackActionIsRun = false;
            void fallbackAction() { fallbackActionIsRun = true; }

            var fallbackTolerance = Tolerance
                .Handle<DivideByZeroException>()
                .Fallback(fallbackAction);

            fallbackTolerance.Execute(() =>
                retryTolerance.Execute(() =>
                {
                    throw new DivideByZeroException();
                }));

            Assert.True(fallbackActionIsRun);
        }

        [Fact]
        public void RetryAndFallback_BothDoNotHandleException_ShouldThrow()
        {
            var retryTolerance = Tolerance.Handle<InvalidCastException>().Retry(3);

            void fallbackAction() { }
            var fallbackTolerance = Tolerance
                .Handle<DivideByZeroException>()
                .Fallback(fallbackAction);

            void fallbackAfterRetry() => fallbackTolerance.Execute(() =>
                    retryTolerance.Execute(() =>
                    {
                        throw new IndexOutOfRangeException();
                    }));

            Assert.Throws<IndexOutOfRangeException>(fallbackAfterRetry);
        }

        [Fact]
        public void FallbackAction_FallbackHandlesException_ShouldBeRun()
        {
            var retryTolerance = Tolerance.Handle<InvalidCastException>().Retry(3);

            bool fallbackActionIsRun = false;
            void fallbackAction() { fallbackActionIsRun = true; }

            var fallbackTolerance = Tolerance
                .Handle<DivideByZeroException>()
                .Fallback(fallbackAction);

            retryTolerance.Execute(() =>
                    fallbackTolerance.Execute(() =>
                    {
                        throw new DivideByZeroException();
                    }));

            Assert.True(fallbackActionIsRun);
        }

    }
}
