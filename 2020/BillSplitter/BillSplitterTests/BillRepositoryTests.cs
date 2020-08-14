using System;
using System.Collections.Generic;
using System.Linq;
using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BillSplitterTests
{
    public class BillRepositoryTests
    {
        [Fact]
        public void AddBill_AddsNewBillInDb()
        {
            var bills = new List<Bill>();
            var dbSetMock = DbSetMockBuilder.BuildDbSet(bills);
            var contextMock = new Mock<DbContext>();

            contextMock.Setup(c => c.Set<Bill>()).Returns(dbSetMock);

            var uow = new UnitOfWork(contextMock.Object);

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
            var bills = new List<Bill>();
            var dbSetMock = DbSetMockBuilder.BuildDbSet(bills);
            var contextMock = new Mock<DbContext>();

            contextMock.Setup(c => c.Set<Bill>()).Returns(dbSetMock);
            var uow = new UnitOfWork(contextMock.Object);

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
            var bills = new List<Bill>();
            var dbSetMock = DbSetMockBuilder.BuildDbSet(bills);
            var contextMock = new Mock<DbContext>();

            contextMock.Setup(c => c.Set<Bill>()).Returns(dbSetMock);
            var uow = new UnitOfWork(contextMock.Object);

            var actual = uow.Bills.GetBillById(1);

            Assert.Null(actual);
        }

        [Fact]
        public void Exist_ReturnsTrue()
        {
            var bills = new List<Bill>();
            var dbSetMock = DbSetMockBuilder.BuildDbSet(bills);
            var contextMock = new Mock<DbContext>();

            contextMock.Setup(c => c.Set<Bill>()).Returns(dbSetMock);
            var uow = new UnitOfWork(contextMock.Object);

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
            var bills = new List<Bill>();
            var dbSetMock = DbSetMockBuilder.BuildDbSet(bills);
            var contextMock = new Mock<DbContext>();

            contextMock.Setup(c => c.Set<Bill>()).Returns(dbSetMock);
            var uow = new UnitOfWork(contextMock.Object);

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
            var bills = new List<Bill>();
            var dbSetMock = DbSetMockBuilder.BuildDbSet(bills);
            var contextMock = new Mock<DbContext>();

            contextMock.Setup(c => c.Set<Bill>()).Returns(dbSetMock);
            var uow = new UnitOfWork(contextMock.Object);

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