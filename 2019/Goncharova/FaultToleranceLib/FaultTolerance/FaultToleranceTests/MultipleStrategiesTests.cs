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
                () =>
                {
                    retryStrategy.Execute(
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
                () =>
                {
                    retryStrategy.Execute(
                () => helper.ThrowException());
                });

            Assert.False(fallbackActionIsRun);
        }
    }
}
