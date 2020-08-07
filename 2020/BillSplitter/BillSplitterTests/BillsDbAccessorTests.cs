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

            var accessor = new BillRepository(db);

            var bill = new Bill()
            {
                Id = 1
            };

            accessor.Add(bill);
            _uow.Save();
            Assert.True(db.Bills.Contains(bill));
        }

        [Fact]
        public void GetBillById_ReturnsRightBill()
        {
            using var db = InMemoryContextBuilder.Build();

            var accessor = new BillRepository(db);

            var bill1 = new Bill()
            {
                Id = 1
            };
            var bill2 = new Bill()
            {
                Id = 2
            };

            db.Bills.AddRange(bill1, bill2);
            db.SaveChanges();

            var expected = bill2;
            var actual = accessor.GetBillById(2);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetBillById_ReturnsNull()
        {
            using var db = InMemoryContextBuilder.Build();

            var accessor = new BillRepository(db);

            var actual = accessor.GetBillById(1);

            Assert.Null(actual);
        }

        [Fact]
        public void DbContains_ReturnsTrue()
        {
            using var db = InMemoryContextBuilder.Build();

            var accessor = new BillRepository(db);

            var bill = new Bill()
            {
                Id = 1
            };

            accessor.Add(bill);

            Assert.True(accessor.Exist(1));
        }
        [Fact]
        public void DbContains_ReturnsFalse()
        {
            using var db = InMemoryContextBuilder.Build();

            var accessor = new BillRepository(db);

            var bill1 = new Bill()
            {
                Id = 1
            };

            accessor.Add(bill1);

            Assert.False(accessor.Exist(2));
        }
    }
}