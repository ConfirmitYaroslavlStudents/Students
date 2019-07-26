using System;
using System.IO;
using Xunit;

namespace FaultTolerance.Tests
{
    public class ActionRunnerTests
    {
        [Fact]
        public void Try_Succeed_NoFallbackRun()
        {
            var param = new ActionRunnerParameters(3);

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

            var runner = new ActionRunner(param);
            runner.Try(action);

            Assert.False(ioExceptionFallbackRun);
            Assert.False(arithmeticExceptionFallbackRun);
        }

        [Fact]
        public void Try_CountTimes_Succeed_ActionRuns_LessOrEqualsTo_CountTimes()
        {
            int upperBound = 10;
            var param = new ActionRunnerParameters(upperBound);
            param.Handle<ArithmeticException>();

            int actual = 0;
            Action action = () =>
            {
                actual++;
                if (actual <= 5)
                    throw new ArithmeticException();
            };

            var runner = new ActionRunner(param);
            runner.Try(action);

            Assert.True(actual <= upperBound);
        }

        [Fact]
        public void Try_CountTimes_Failed_ActionRuns_CountTimes()
        {
            int upperBound = 10;
            int expected = upperBound;
            var param = new ActionRunnerParameters(upperBound);
            param.Handle<ArithmeticException>();

            int actual = 0;
            Action action = () =>
            {
                actual++;
                throw new ArithmeticException();
            };

            var runner = new ActionRunner(param);
            runner.Try(action);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Try_Failed_RunRelevantFallback()
        {
            var param = new ActionRunnerParameters(2);

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

            var runner = new ActionRunner(param);
            runner.Try(action);

            Assert.False(ioExceptionFallbackRun);
            Assert.True(arithmeticExceptionFallbackRun);
        }
    }
}