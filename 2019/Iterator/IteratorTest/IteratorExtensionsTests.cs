using System;
using System.Collections.Generic;
using IteratorLib;
using Xunit;

namespace IteratorTest
{
    public class IteratorExtensionsTests
    {
        [Fact]
        public void MapIteration()
        {
            Func<int, int> func = (int x) => 2 * x;

            ArrayIterator<int> actual = new ArrayIterator<int>(new[] { 1, 2, 3 });

            IIterator<int> expected = actual.Map(func);
            while (actual.MoveNext() && expected.MoveNext())
                Assert.Equal(expected.Current, 2 * actual.Current);
        }

        [Fact]
        public void MapIteration2()
        {
            Func<int, int> func = (int x) => 2 * x;

            ArrayIterator<int> it = new ArrayIterator<int>(new[] { 1, 2, 3 });
            it.MoveNext();
            it.MoveNext();

            IIterator<int> expected = it.Map(func);
            expected.MoveNext();

            Assert.Equal(expected.Current, 6);
        }

        [Fact]
        public void FilterIteration()
        {
            Func<int, bool> func = (int x) => x % 2 == 0;

            ArrayIterator<int> it = new ArrayIterator<int>(new[] { 1, 2, 3 });

            IIterator<int> actual = it.Filter(func);
            actual.MoveNext();

            Assert.Equal(2, actual.Current);
        }

        [Fact]
        public void FilterAndMapComposition()
        {
            //                                            1  2  0  1  2  0  1
            var iterator = new ArrayIterator<int>(new [] {1, 2, 3, 4, 5, 6, 7}).Map(x => x % 3).Filter(x => x == 1);

            var expected = new List<int> {1, 1, 1};
            var actual = new List<int>();

            while (iterator.MoveNext())
                actual.Add(iterator.Current);

            Assert.Equal(expected, actual);
        }
    }
}