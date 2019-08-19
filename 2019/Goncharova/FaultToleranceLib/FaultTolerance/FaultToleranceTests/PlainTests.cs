﻿using FaultTolerance;
using System;
using Xunit;

namespace FaultToleranceTests
{
    public class PlainTests
    {
        [Fact]
        public void Plain_ActionThrowsException_ShouldThrow()
        {
            var tolerance = BuildTolerance.Plain();

            void action() => tolerance.Execute(() => throw new NotImplementedException());

            Assert.Throws<NotImplementedException>(action);
        }

        [Fact]
        public void Plain_ActionDoesNotThrowException_ShouldNotThrow()
        {
            var tolerance = BuildTolerance.Plain();

            tolerance.Execute(() => { });
        }

    }
}
