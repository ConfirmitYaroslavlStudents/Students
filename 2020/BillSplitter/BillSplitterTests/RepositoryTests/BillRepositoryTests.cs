using System.Collections.Generic;
using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BillSplitterTests.RepositoryTests
{
    public class BillRepositoryTests : TestsBase
    {
        [Fact]
        public void AddBill_AddsNewBillInDb()
        {
            var uow = new UnitOfWork(Context);

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
            var uow = new UnitOfWork(Context);

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
            var uow = new UnitOfWork(Context);

            var actual = uow.Bills.GetBillById(1);

            Assert.Null(actual);
        }

        [Fact]
        public void Exist_ReturnsTrue()
        {
            var uow = new UnitOfWork(Context);

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
            var uow = new UnitOfWork(Context);

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
            var uow = new UnitOfWork(Context);

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