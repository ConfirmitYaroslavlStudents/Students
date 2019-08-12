using System;
using FaultTolerance.Extensions;
using FaultTolerance.Extensions.Logger;
using Xunit;

namespace FaultToleranceTest.Extensions
{
    public class ExtensionsRunnerTests
    {
        [Fact]
        public void Fallback()
        {
            Builder.BuildFallback()
                .Or<NullReferenceException>()
                .Or<NotImplementedException>()
                .WithFallback(() => { })
                .Execute(() => throw new NotImplementedException());
        }

        [Fact]
        public void Retry()
        {
            Builder.BuildRetry()
                .WithRetryCount(1)
                .Execute(() => throw new NotImplementedException());
        }

        [Fact]
        public void Wrap()
        {
            Builder.BuildRetry(1)
                .WithRetryCount(1)
                .Wrap(Builder.BuildFallback().Or<NotImplementedException>())
                .Execute(() => throw new NullReferenceException());
        }

        [Fact]
        public void UnWrap()
        {
            Builder.BuildRetry(1)
                .WithRetryCount(1)
                .Wrap(Builder.BuildFallback().Or<NotImplementedException>())
                .UnWrap()
                .Execute(() => throw new NullReferenceException());
        }

        [Fact]
        public void Decorator()
        {
            Builder.BuildRetry(1)
                .WithRetryCount(1)
                .WithDecorator((builder) => new LoggerDecorator(builder))
                .Execute(() => throw new NullReferenceException());
        }
    }
}