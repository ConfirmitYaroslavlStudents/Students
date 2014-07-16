using System;
using System.Globalization;
using System.Threading;
using Xunit;

namespace RefreshingCache.Tests
{
    public class RefreshingCacheTests
    {
        private class Computer : IComputer<int, string>
        {
            public string GetData(int key)
            {
                return key.ToString(CultureInfo.InvariantCulture);
            }
        }

        [Fact]
        public void RefreshingCacheIndexer_SimpleIntegerValue_ShouldPass()
        {
            const int timeLowerThanMax = 400;

            var cache = new RefreshingCache<int, string>(new Computer());
            var actual = cache[0];
            Thread.Sleep(timeLowerThanMax);

            Assert.Equal("0", actual);
        }

        [Fact]
        public void RefreshingCacheCount_WhenTimeIsUp_ShouldPass()
        {
            const int timeUpperThanMax = 600;

            var cache = new RefreshingCache<int, string>(new Computer());
            for (int i = 0; i < 5; ++i)
            {
                var temp = cache[i];
            }
            Thread.Sleep(timeUpperThanMax);

            Assert.Equal(0, cache.Count);
        }

        [Fact]
        public void RefreshingCacheIndexer_WhenCacheSizeIncreaseUpperMaxSize_ShouldPass()
        {
            const int maxCacheSize = 16;
            var cache = new RefreshingCache<int, string>(new Computer());
            for (int i = 0; i < 20; ++i)
            {
                var temp = cache[i];
            }

            Assert.Equal(maxCacheSize, cache.Count);
        }

        [Fact]
        public void RefreshingCache_NullReferenceInIComputerParam_ArgumentNullExceptionThrown()
        {
            Assert.Throws(typeof(ArgumentNullException), () =>
            {
                Computer computer = null;
                var cache = new RefreshingCache<int, string>(computer);
            });
        }
    }
}
