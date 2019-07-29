using System;
using System.IO;
using Xunit;

namespace FaultTolerance.Tests
{
    public class ExceptionHandlerParametersTests
    {
        [Fact]
        public void SetNonPositiveNumberOfTries_ThrowsArgumentException()
        {
            Action action = () => { new ExceptionHandlerParameters(-1); };

            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void AddAlreadyAddedException_ThrowsArumentException()
        {
            Action action = () =>
            {
                var param = new ExceptionHandlerParameters(3);
                param.Handle<IOException>()
                    .Handle<ArithmeticException>()
                    .Handle<DivideByZeroException>()
                    .Handle<IOException>();
            };

            Assert.Throws<ArgumentException>(action);
        }
    }
}