using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FaultTolerance.Tests
{
    public class ExceptionHandlerTests
    {
        [Fact]
        public void Run_Succeed_NoFallbackRun()
        {
            bool ioExceptionFallbackRun = false;
            bool arithmeticExceptionFallbackRun = false;

            int count = 0;
            Action action = () =>
            {
                count++;
                if (count == 1)
                    throw new IOException();
                if (count == 2)
                    throw new ArithmeticException();
            };

            var handler = new ExceptionHandler();
            handler
                .Handle<IOException>(() => { ioExceptionFallbackRun = true; })
                .Handle<ArithmeticException>(() => { arithmeticExceptionFallbackRun = true; })
                .Repeat(3)
                .Run(action);

            Assert.False(ioExceptionFallbackRun);
            Assert.False(arithmeticExceptionFallbackRun);
        }

        [Fact]
        public void Run_CountTimes_Succeed_ActionRuns_LessOrEqualsTo_CountTimes()
        {
            int upperBound = 10;

            int actual = 0;
            Action action = () =>
            {
                actual++;
                if (actual <= 5)
                    throw new ArithmeticException();
            };

            var handler = new ExceptionHandler();
            handler.Handle<ArithmeticException>().Repeat(upperBound).Run(action);

            Assert.True(actual <= upperBound);
        }

        [Fact]
        public void Run_CountTimes_Failed_ActionRuns_CountTimes()
        {
            int upperBound = 10;
            int expected = upperBound;

            int actual = 0;
            Action action = () =>
            {
                actual++;
                throw new ArithmeticException();
            };

            var handler = new ExceptionHandler();
            handler.Handle<ArithmeticException>().Repeat(upperBound).Run(action);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Run_Failed_RunRelevantFallback()
        {
            bool ioExceptionFallbackRun = false;
            bool arithmeticExceptionFallbackRun = false;

            int count = 0;
            Action action = () =>
            {
                count++;
                if (count == 1)
                    throw new IOException();
                if (count == 2)
                    throw new ArithmeticException();
            };


            var handler = new ExceptionHandler();

            handler
                .Handle<IOException>(() => { ioExceptionFallbackRun = true; })
                .Handle<ArithmeticException>(() => { arithmeticExceptionFallbackRun = true; })
                .Repeat(2)
                .Run(action);

            Assert.False(ioExceptionFallbackRun);
            Assert.True(arithmeticExceptionFallbackRun);
        }

        [Fact]
        public void Run_RunnerFailed_RunsAppropriateFallback()
        {
            bool hadRun = false;
            bool hadRunArithmeticEx = false;

            var handler = new ExceptionHandler();
            handler
                .Handle<ArithmeticException>(() => hadRunArithmeticEx = true)
                .HandleFailedRun(() => hadRun = true)
                .WithTimeout(10)
                .Repeat(2)
                .Run(() => Thread.Sleep(100));

            Assert.True(hadRun);
            Assert.False(hadRunArithmeticEx);
        }

        [Fact]
        public void Run_RunnerSucceed_DoNotRunAnyFallback()
        {
            bool hadRun = false;
            bool hadRunArithmeticEx = false;

            var handler = new ExceptionHandler();

            handler
                .Handle<ArithmeticException>(() => hadRunArithmeticEx = true)
                .HandleFailedRun(() => hadRun = true)
                .WithTimeout(10)
                .Repeat(2)
                .Run(() => { });

            Assert.False(hadRun);
            Assert.False(hadRunArithmeticEx);
        }

        [Fact]
        public void MultipleRun_DifferentTimeouts()
        {
            bool failedRun = false;
            var handler = new ExceptionHandler();
            handler
                .WithTimeout(10)
                .HandleFailedRun(() => failedRun = true)
                .Repeat(2)
                .Run(() => Thread.Sleep(50))
                .WithTimeout(-1)
                .Run(() => Thread.Sleep(100));

            Assert.True(failedRun);
        }

        [Fact]
        public void MultipleRun_DifferentCountOfRepeats()
        {
            int actual1 = 0;
            int expected1 = 2;
            int actual2 = 0;
            int expected2 = 3;

            Action action1 = () =>
            {
                actual1++;
                throw new Exception();
            };
            Action action2 = () =>
            {
                actual2++;
                throw new Exception();
            };

            var hanler = new ExceptionHandler();
            hanler
                .Handle<Exception>()
                .Repeat(expected1)
                .Run(action1)
                .Repeat(expected2)
                .Run(action2);

            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
        }

        [Fact]
        public void MultipleRun_StressTest()
        {
            bool failedRun = false;
            var handler = new ExceptionHandler();
            handler
                .WithTimeout(20)
                .HandleFailedRun(() => failedRun = true)
                .Repeat(2)
                .Run(() => Thread.Sleep(9))
                .Run(() => Thread.Sleep(9))
                .Run(() => Thread.Sleep(9))
                .Run(() => Thread.Sleep(9))
                .Run(() => Thread.Sleep(9))
                .Run(() => Thread.Sleep(9))
                .Run(() => Thread.Sleep(9))
                .Run(() => Thread.Sleep(9))
                .Run(() => Thread.Sleep(9))
                .Run(() => Thread.Sleep(9));

            Assert.False(failedRun);
        }

        [Fact]
        public void Deadlock_SucceedRun()
        {
            var thread = Thread.CurrentThread;
            Action action = () =>
            {
                thread.Join();
            };

            bool runHadFailed = false;

            var handler = new ExceptionHandler();

            handler.WithTimeout(1000).HandleFailedRun(() => runHadFailed = true).Run(action);
            Assert.True(runHadFailed);
        }

        [Fact]
        public void Deadlock2_SucceedRun()
        {
            var thread = Thread.CurrentThread;

            Action action2 = () =>
            {
                thread.Join();
            };

            Action action1 = () =>
            {
                var task = new Task(action2);
                task.Start();
                task.Wait();
            };

            bool runHadFailed = false;

            var handler = new ExceptionHandler();

            handler.WithTimeout(1000).HandleFailedRun(() => runHadFailed = true).Run(action1);
            Assert.True(runHadFailed);
        }
    }
}