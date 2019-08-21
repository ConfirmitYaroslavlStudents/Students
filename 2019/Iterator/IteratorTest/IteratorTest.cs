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
            var it = new ArrayIterator<int>(array);

            var expected = new List<int>() { 1, 2, 3 };
            var actual = new List<int>();

            while (it.MoveNext())
                actual.Add(it.Current);

            Assert.Equal(expected, actual);
        }
    }
}
