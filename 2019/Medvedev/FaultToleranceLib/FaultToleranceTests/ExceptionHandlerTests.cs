using System;
using System.IO;
using System.Threading;
using Xunit;

namespace FaultTolerance.Tests
{
    public class ExceptionHandlerTests
    {
        [Fact]
        public void Try_Succeed_NoFallbackRun()
        {
            var param = new ExceptionHandlerParameters(3);

            bool ioExceptionFallbackRun = false;
            bool arithmeticExceptionFallbackRun = false;

            param
                .Handle<IOException>(() => { ioExceptionFallbackRun = true; })
                .Handle<ArithmeticException>(() => { arithmeticExceptionFallbackRun = true; });

            int count = 0;
            Action action = () =>
            {
                count++;
                if (count == 1)
                    throw new IOException();
                if (count == 2)
                    throw new ArithmeticException();
            };

            var handler = new ExceptionHandler(param);
            handler.Try(new Runner(), action);

            Assert.False(ioExceptionFallbackRun);
            Assert.False(arithmeticExceptionFallbackRun);
        }

        [Fact]
        public void Try_CountTimes_Succeed_ActionRuns_LessOrEqualsTo_CountTimes()
        {
            int upperBound = 10;
            var param = new ExceptionHandlerParameters(upperBound);
            param.Handle<ArithmeticException>();

            int actual = 0;
            Action action = () =>
            {
                actual++;
                if (actual <= 5)
                    throw new ArithmeticException();
            };

            var handler = new ExceptionHandler(param);
            handler.Try(new Runner(), action);

            Assert.True(actual <= upperBound);
        }

        [Fact]
        public void Try_CountTimes_Failed_ActionRuns_CountTimes()
        {
            int upperBound = 10;
            int expected = upperBound;
            var param = new ExceptionHandlerParameters(upperBound);
            param.Handle<ArithmeticException>();

            int actual = 0;
            Action action = () =>
            {
                actual++;
                throw new ArithmeticException();
            };

            var handler = new ExceptionHandler(param);
            handler.Try(new Runner(), action);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Try_Failed_RunRelevantFallback()
        {
            var param = new ExceptionHandlerParameters(2);

            bool ioExceptionFallbackRun = false;
            bool arithmeticExceptionFallbackRun = false;

            param
                .Handle<IOException>(() => { ioExceptionFallbackRun = true; })
                .Handle<ArithmeticException>(() => { arithmeticExceptionFallbackRun = true; });

            int count = 0;
            Action action = () =>
            {
                count++;
                if (count == 1)
                    throw new IOException();
                if (count == 2)
                    throw new ArithmeticException();
            };

            var handler = new ExceptionHandler(param);
            handler.Try(new Runner(), action);

            Assert.False(ioExceptionFallbackRun);
            Assert.True(arithmeticExceptionFallbackRun);
        }

        [Fact]
        public void Try_RunnerFailed_RunsAppropriateFallback()
        {
            bool hadRun = false;
            bool hadRunArithmeticEx = false;

            var param = new ExceptionHandlerParameters(2);
            param
                .Handle<ArithmeticException>(() => hadRunArithmeticEx = true)
                .HandleFailedRun(() => hadRun = true);

            var handler = new ExceptionHandler(param);
            handler.Try(new TimeoutRunner(10), () => Thread.Sleep(100));

            Assert.True(hadRun);
            Assert.False(hadRunArithmeticEx);
        }

        [Fact]
        public void Try_RunnerSucceed_DoNotRunAnyFallback()
        {
            bool hadRun = false;
            bool hadRunArithmeticEx = false;

            var param = new ExceptionHandlerParameters(2);
            param
                .Handle<ArithmeticException>(() => hadRunArithmeticEx = true)
                .HandleFailedRun(() => hadRun = true);

            var handler = new ExceptionHandler(param);
            handler.Try(new TimeoutRunner(10), () => { });

            Assert.False(hadRun);
            Assert.False(hadRunArithmeticEx);
        }
    }
}