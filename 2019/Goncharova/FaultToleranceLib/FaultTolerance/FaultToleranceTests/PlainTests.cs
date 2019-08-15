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
            var Tolerance = Tolerance.Plain();

            void action() => Tolerance.Execute(() => throw new NotImplementedException());

            Assert.Throws<NotImplementedException>(action);
        }

        [Fact]
        public void Plain_ActionDoesNotThrowException_ShouldNotThrow()
        {
            var Tolerance = Tolerance.Plain();

            Tolerance.Execute(() => { });
        }

    }
}
