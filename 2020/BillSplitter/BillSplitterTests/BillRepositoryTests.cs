using BillSplitter.Data;
using BillSplitter.Models;
using Xunit;

namespace BillSplitterTests
{
    public class BillRepositoryTests
    {
        [Fact]
        public void AddBill_AddsNewBillInDb()
        {
            using var db = new InMemoryContextBuilder().Build();
            var uow = new UnitOfWork(db);

            var bill = new Bill()
            {
                Id = 1
            };

            uow.Bills.Add(bill);
            uow.Save();

            Assert.True(uow.Bills.Exist(1));
        }

        [Fact]
        public void GetBillById_ReturnsRightBill()
        {
            using var db = new InMemoryContextBuilder().Build();
            var uow = new UnitOfWork(db);

            var bill1 = new Bill
            {
                Id = 1
            };
            var bill2 = new Bill
            {
                Id = 2
            };

            uow.Bills.Add(bill1);
            uow.Bills.Add(bill2);
            uow.Save();

            var expected = bill2;
            var actual = uow.Bills.GetBillById(2);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetBillById_ReturnsNull()
        {
            using var db = new InMemoryContextBuilder().Build();
            var uow = new UnitOfWork(db);

            var actual = uow.Bills.GetBillById(1);

            Assert.Null(actual);
        }

        [Fact]
        public void Exist_ReturnsTrue()
        {
            using var db = new InMemoryContextBuilder().Build();
            var uow = new UnitOfWork(db);

            var bill = new Bill()
            {
                Id = 1
            };

            uow.Bills.Add(bill);
            uow.Save();

            Assert.True(uow.Bills.Exist(1));
        }

        [Fact]
        public void DontExist_ReturnsFalse()
        {
            using var db = new InMemoryContextBuilder().Build();
            var uow = new UnitOfWork(db);

            var bill = new Bill()
            {
                Id = 1
            };

            uow.Bills.Add(bill);
            uow.Save();

            Assert.False(uow.Bills.Exist(2));
        }

        [Fact]
        public void DeleteById_DeletesBill()
        {
            using var db = new InMemoryContextBuilder().Build();
            var uow = new UnitOfWork(db);

            var bill = new Bill()
            {
                Id = 1
            };

            uow.Bills.Add(bill);
            uow.Save();

            uow.Bills.DeleteById(1);
            uow.Save();

            Assert.False(uow.Bills.Exist(1));
        }
    }
}