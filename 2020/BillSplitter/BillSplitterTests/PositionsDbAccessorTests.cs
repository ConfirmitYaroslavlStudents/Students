using BillSplitter.Data;
using BillSplitter.Models;
using System;
using System.Linq;
using Xunit;

namespace BillSplitterTests
{
    public class PositionsDbAccessorTests
    {
        [Fact]
        public void AddPosition_AddNewPositionInDb()
        {
            using var db = InMemoryContextBuilder.Build();

            var accessor = new PositionsDbAccessor(db);

            var position = new Position()
            {
                Id = 1
            };

            accessor.Add(position);

            Assert.True(db.Positions.Contains(position));
        }
    }
}
