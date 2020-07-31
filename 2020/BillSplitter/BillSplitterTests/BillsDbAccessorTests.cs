using System.Linq;
using BillSplitter.Data;
using BillSplitter.Models;
using Xunit;


namespace BillSplitterTests
{
    public class BillsDbAccessorTests
    {
        [Fact]
        public void AddBill_AddsNewBillInDb()
        {
            using var db = InMemoryContextBuilder.Build();

            var accessor = new BillsDbAccessor(db);

            var bill = new Bill()
            {
                Id = 1
            };

            accessor.AddBill(bill);

            Assert.True(db.Bill.Contains(bill));
        }

        [Fact]
        public void GetBillById_ReturnsRightBill()
        {
            using var db = InMemoryContextBuilder.Build();

            var accessor = new BillsDbAccessor(db);

            var bill1 = new Bill()
            {
                Id = 1
            };
            var bill2 = new Bill()
            {
                Id = 2
            };

            db.Bill.AddRange(bill1, bill2);
            db.SaveChanges();

            var expected = bill2;
            var actual = accessor.GetBillById(2);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetBillById_ReturnsNull()
        {
            using var db = InMemoryContextBuilder.Build();

            var accessor = new BillsDbAccessor(db);

            var actual = accessor.GetBillById(1);

            Assert.Null(actual);
        }

        [Fact]
        public void DbContains_ReturnsTrue()
        {
            using var db = InMemoryContextBuilder.Build();

            var accessor = new BillsDbAccessor(db);

            var bill = new Bill()
            {
                Id = 1
            };

            accessor.AddBill(bill);

            Assert.True(accessor.DbContains(1));
        }
        [Fact]
        public void DbContains_ReturnsFalse()
        {
            using var db = InMemoryContextBuilder.Build();

            var accessor = new BillsDbAccessor(db);

            var bill1 = new Bill()
            {
                Id = 1
            };

            accessor.AddBill(bill1);

            Assert.False(accessor.DbContains(2));
        }
    }
}