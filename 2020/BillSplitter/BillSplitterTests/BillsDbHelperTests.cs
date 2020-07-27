using System.Collections.Generic;
using System.Linq;
using BillSplitter.Data;
using BillSplitter.Models;
using Xunit;

namespace BillSplitterTests
{
    public class BillsDbHelperTests
    {
        [Fact]
        public void AddBill_AddsNewBill()
        {
            using var db = InMemoryContextBuilder.Build();
            var helper = new BillsDbAccessor(db);

            var bill = new Bill()
            {
                Id = 1
            };

            helper.AddBill(bill);

            Assert.True(db.Bill.Contains(bill));
        }

        [Fact]
        public void GetBillById_ReturnsRightBill()
        {
            using var db = InMemoryContextBuilder.Build();
            var helper = new BillsDbAccessor(db);

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
            var actual = helper.GetBillById(2);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetPositionsById_ReturnsPositionList()
        {
            using var db = InMemoryContextBuilder.Build();
            var helper = new BillsDbAccessor(db);

            var expected = new List<Position>()
            {
                new Position() {Name = "a"},
                new Position() {Name = "b"}
            };

            var bill1 = new Bill()
            {
                Id = 1,
                Positions = expected
            };

            db.Bill.AddRange(bill1);
            db.SaveChanges();

            var actual = helper.GetPositionsById(1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetCustomersById_ReturnsListOfCustomers()
        {
            using var db = InMemoryContextBuilder.Build();
            var helper = new BillsDbAccessor(db);

            var expected = new List<Customer>()
            {
                new Customer() {Name = "a"},
                new Customer() {Name = "b"}
            };

            var bill1 = new Bill()
            {
                Id = 1,
                Customers = expected
            };

            db.Bill.AddRange(bill1);
            db.SaveChanges();

            var actual = helper.GetCustomersById(1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DbContains_DbContainsBill_ReturnsTrue()
        {
            using var db = InMemoryContextBuilder.Build();
            var helper = new BillsDbAccessor(db);

            var bill = new Bill()
            {
                Id = 1
            };

            helper.AddBill(bill);

            Assert.True(helper.DbContains(1));
        }
    }
}