using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FaultTolerance;
using Xunit;
using Assert = Xunit.Assert;

namespace FaultToleranceTests
{
    [TestClass]
    public class FaultToleranceTest
    {
        [Fact]
        public void RertryThrowException()
        {
            Assert.Throws<StackOverflowException>(() =>
            {
                ExceptionHandler.Retry<StackOverflowException>(3,
                    () => { throw new StackOverflowException(); });
            });
        }

        [Fact]
        public void Fallback()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                ExceptionHandler.Fallback<FormatException>(
                    () => { throw new FormatException(); },
                    () => { throw new InvalidOperationException(); });
            });
        }

    }
}
