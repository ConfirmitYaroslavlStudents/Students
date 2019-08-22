using System;
using Xunit;
using FaultTolerance;
using System.Threading;

namespace FaultToleranceTests
{
    public class Tests
    {
        [Fact]
        public void Fallback()
        {
            new Fallback()
                .Handle<NotImplementedException>()
                .SetFallback(() => { })
                .Execute(() => throw new NotImplementedException());
        }

        [Fact]
        public void Retry()
        {
            new Retry().SetRetryCount(1)
                .Execute(() => throw new NotImplementedException());
        }

        [Fact]
        public void TimeOut()
        {
            int run = 0;
            new FaultTolerance.Timeout(1)
                .Execute(() => { Thread.Sleep(100); run = 100; });

            Assert.Equal(0, run);
        }

        [Fact]
        public void WrapRetryFallback()
        {
            int runs = 0;
            new Retry().SetRetryCount(3)
                .Wrap(new Fallback()
                .Handle<NotImplementedException>()
                .Handle<InvalidOperationException>())
                .Execute(() => { runs++; throw new NotImplementedException(); });

            Assert.Equal(1, runs);

            new Fallback()
                .Handle<NotImplementedException>()
                .Handle<InvalidOperationException>()
                .Wrap(new Retry(3))
                .Execute(() => { runs++; throw new NotImplementedException(); });

            Assert.Equal(4, runs);
        }

        [Fact]
        public void WrapRetryTimeOut()
        {
            int runs = 1;

            new FaultTolerance.Timeout(1000)
                .Wrap(new Retry(2))
                .Execute(() => { Thread.Sleep(runs * 600); runs++; });

            Assert.Equal(2, runs);
        }
        
        [Fact]
        public void WrapRetryFallbackTimeOut()
        {
            int runs = 0;

            new FaultTolerance.Timeout(2000)
                .Wrap(new Fallback()
                .Handle<NotImplementedException>()
                .Wrap(new Retry(5)))
                .Execute(() =>
                {
                    runs++;
                    Thread.Sleep(1000 * runs);
                });

            Assert.Equal(1, runs);
        }
    }
}
