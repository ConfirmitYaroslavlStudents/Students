using System;
using Xunit;
using IteratorLib;
using System.Collections.Generic;

namespace IteratorTest
{
    public class IteratorTest
    {
        [Fact]
        public void SimpleIteration()
        {
            var array = new int[] { 1, 2, 3 };
            var it = new Iterator<int>(array);

            var expected = new List<int>() { 1, 2, 3 };
            var actual = new List<int>();

            while (it.MoveNext())
                actual.Add(it.Current);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MapIteration()
        {
            Func<int, int> func = (int x) => 2 * x;

            Iterator<int> actual = new Iterator<int>(new[] { 1, 2, 3 });

            var expected = actual.Map(func);
            while (actual.MoveNext() && expected.MoveNext())
                Assert.Equal(expected.Current, 2 * actual.Current);
        }

        [Fact]
        public void MapIteration2()
        {
            Func<int, int> func = (int x) => 2 * x;

            Iterator<int> it = new Iterator<int>(new[] { 1, 2, 3 });
            it.MoveNext();
            it.MoveNext();

            var expected = it.Map(func);
            expected.MoveNext();

            Assert.Equal(6, expected.Current);
        }

        [Fact]
        public void FilterIteration()
        {
            Predicate<int> func = (int x) => x % 2 == 0;

            Iterator<int> it = new Iterator<int>(new[] { 1, 2, 3 });

            var actual = it.Filter(func);
            actual.MoveNext();

            Assert.Equal(2, actual.Current);
        }
    }
}
