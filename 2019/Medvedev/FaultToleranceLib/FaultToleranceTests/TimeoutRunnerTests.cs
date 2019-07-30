using System;
using System.Threading;
using Xunit;

namespace FaultTolerance.Tests
{
    public class TimeoutRunnerTests
    {
        [Fact]
        public void Run_RunsAction()
        {
            bool hadRun = false;
            Action action = () => hadRun = true;

            var runner = new TimeoutRunner(10000);
            runner.Run(action);

            Assert.True(hadRun);
        }

        [Fact]
        public void Run_SucceedRun_ReturnsTrue()
        {
            var runner = new TimeoutRunner(10000);

            Assert.True(runner.Run(() => { }));
        }

        [Fact]
        public void Run_FailedByExceptionIsAction_ThrowsException()
        {
            Action action = () => throw new ArithmeticException();

            var runner = new TimeoutRunner(1000);

            Assert.Throws<ArithmeticException>(() => runner.Run(action));
        }

        [Fact]
        public void Run_FailedByTimeout_ReturnsFalse()
        {
            var runner = new TimeoutRunner(10);
            Assert.False(runner.Run(() => Thread.Sleep(100)));
        }

        [Fact]
        public void SetNonPositiveTimeout_ThrowsArgumentException()
        {
            Action actoin = () => new TimeoutRunner(-1);
            Assert.Throws<ArgumentException>(actoin);
        }
    }
}