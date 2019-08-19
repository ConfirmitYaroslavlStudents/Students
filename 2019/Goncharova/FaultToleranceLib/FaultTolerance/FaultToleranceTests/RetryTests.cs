using FaultTolerance;
using FaultTolerance.Retry;
using System;
using Xunit;

namespace FaultToleranceTests
{
    public class RetryTests
    {
        [Fact]
        public void Count_LessThanZero_ShouldThrow()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => 
                BuildTolerance.Handle<DivideByZeroException>().Retry(-1));
        }

        [Fact]
        public void Retry_HandledExceptionThrownPermittedRetryCountTimes_ShouldNotThrow()
        {
            int count = 3;
            var tolerance = BuildTolerance.Handle<InvalidCastException>().Retry(count);

            tolerance.ExecuteActionThrows<InvalidCastException>(count);
        }

        [Fact]
        public void Retry_HandledExceptionThrownMoreThanPermittedRetryCountTimes_ShouldThrow()
        {
            int count = 3;
            var tolerance = BuildTolerance.Handle<InvalidCastException>().Retry(count);

            Assert.Throws<InvalidCastException>(() => 
                tolerance.ExecuteActionThrows<InvalidCastException>(count + 1));
        }

        [Fact]
        public void Retry_HandledExceptionThrownLessThanPermittedRetryCountTimes_ShouldNotThrow()
        {
            int count = 3;
            var tolerance = BuildTolerance.Handle<InvalidCastException>().Retry(count);

            tolerance.ExecuteActionThrows<InvalidCastException>(count - 1);
        }
        [Fact]
        public void Retry_OneOfTheHandledExceptionsThrownPermittedRetryCountTimes_ShouldNotThrow()
        {
            int count = 3;
            var tolerance = BuildTolerance
                .Handle<InvalidCastException>()
                .Handle<InvalidOperationException>()
                .Retry(count);

            tolerance.ExecuteActionThrows<InvalidCastException>(count);
        }

        [Fact]
        public void Retry_OneOfTheHandledExceptionsThrownMoreThanPermittedRetryCountTimes_ShouldThrow()
        {
            int count = 3;
            var tolerance = BuildTolerance
                .Handle<InvalidCastException>()
                .Handle<InvalidOperationException>()
                .Retry(count);

            Assert.Throws<InvalidCastException>(() => 
                tolerance.ExecuteActionThrows<InvalidCastException>(count + 1));
        }

        [Fact]
        public void Retry_OneOfTheHandledExceptionsThrownLessThanPermittedRetryCountTimes_ShouldNotThrow()
        {
            int count = 3;
            var tolerance = BuildTolerance
                .Handle<InvalidCastException>()
                .Handle<InvalidOperationException>()
                .Retry(count);

            tolerance.ExecuteActionThrows<InvalidCastException>(count - 1);
        }

        [Fact]
        public void Retry_NotHandledException_ShouldThrow()
        {
            var tolerance = BuildTolerance.Handle<InvalidCastException>().Retry(3);

            Assert.Throws<DivideByZeroException>(() =>
                tolerance.Execute(() =>
                {
                    throw new DivideByZeroException();
                }));
        }

        [Fact]
        public void Retry_NotHandledExceptionsList_ShouldThrow()
        {
            var tolerance = BuildTolerance.Handle<InvalidCastException>().Handle<InvalidOperationException>().Retry(3);

            Assert.Throws<DivideByZeroException>(() =>
                tolerance.Execute(() =>
                {
                    throw new DivideByZeroException();
                }));
        }
    }
}
