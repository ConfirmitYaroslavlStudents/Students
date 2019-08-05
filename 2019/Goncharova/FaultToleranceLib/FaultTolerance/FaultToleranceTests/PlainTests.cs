using FaultTolerance;
using System;
using Xunit;

namespace FaultToleranceTests
{
    public class PlainTests
    {
        [Fact]
        public void Plain_ActionThrowsException_ShouldThrow()
        {
            var strategy = Strategy.Plain();

            void action() => strategy.Execute(() => throw new NotImplementedException());

            Assert.Throws<NotImplementedException>(action);
        }

        [Fact]
        public void Plain_ActionDoesNotThrowException_ShouldNotThrow()
        {
            var strategy = Strategy.Plain();

            strategy.Execute(() => { });
        }
    }
}
