using System;
using Xunit;

namespace FaultTolerance.Tests
{
    public class RunnerTests
    {
        [Fact]
        public void Run_RunsAction()
        {
            bool hadRun = false;
            Action action = () => hadRun = true;

            var runner = new Runner();
            runner.Run(action);

            Assert.True(hadRun);
        }

        [Fact]
        public void Run_SucceedRun_ReturnsTrue()
        {
            var runner = new Runner();

            Assert.True(runner.Run(() => { }));
        }
    }
}