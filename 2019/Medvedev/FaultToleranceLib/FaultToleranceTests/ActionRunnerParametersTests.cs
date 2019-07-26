using System;
using System.IO;
using Xunit;

namespace FaultTolerance.Tests
{
    public class ActionRunnerParametersTests
    {
        [Fact]
        public void SetNonPositiveNumberOfTries_ThrowsArgumentException()
        {
            Action action = () =>
            {
                var param = new ActionRunnerParameters(-1);
            };

            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void SetNonPositiveTimeout_ThrowsArgumentException()
        {
            Action action = () =>
            {
                var param = new ActionRunnerParameters(1, -1);
            };

            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void AddAlreadyAddedException_ThrowsArumentException()
        {
            Action action = () =>
            {
                var param = new ActionRunnerParameters(3);
                param.Handle<IOException>()
                    .Handle<ArithmeticException>()
                    .Handle<DivideByZeroException>()
                    .Handle<IOException>();
            };

            Assert.Throws<ArgumentException>(action);
        }
    }
}