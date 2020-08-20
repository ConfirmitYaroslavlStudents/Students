using System.Collections.Generic;
using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BillSplitterTests
{
    public class CustomerRepositoryTests
    {
        [Fact]
        public void AddCustomer_AddNewCustomerInDb()
        {
            var customers = new List<Member>();
            var dbSetMock = DbSetMockBuilder.BuildDbSet(customers);
            var contextMock = new Mock<DbContext>();
            contextMock.Setup(c => c.Set<Member>()).Returns(dbSetMock);

            var db = new UnitOfWork(contextMock.Object);
            var repo = db.Members;

            var customer = new Member
            {
                Id = 1
            };

            repo.Add(customer);
            db.Save();

            Assert.True(repo.GetById(1) != null);
        }

        [Fact]
        public void GetCustomerById_ReturnsRightCustomer()
        {
            var customers = new List<Member>();
            var dbSetMock = DbSetMockBuilder.BuildDbSet(customers);
            var contextMock = new Mock<DbContext>();
            contextMock.Setup(c => c.Set<Member>()).Returns(dbSetMock);

            var db = new UnitOfWork(contextMock.Object);
            var repo = db.Members;

            var customer1 = new Member
            {
                Id = 1
            };

            var customer2 = new Member
            {
                Id = 2
            };

            repo.Add(customer1);
            repo.Add(customer2);
            db.Save();

            var expected = customer2;
            var actual = repo.GetById(2);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetCustomerById_ReturnsNull()
        {
            var customers = new List<Member>();
            var dbSetMock = DbSetMockBuilder.BuildDbSet(customers);
            var contextMock = new Mock<DbContext>();
            contextMock.Setup(c => c.Set<Member>()).Returns(dbSetMock);

            var db = new UnitOfWork(contextMock.Object);
            var repo = db.Members;

            var actual = repo.GetById(1);

            Assert.Null(actual);
        }

        [Fact]
        public void DeleteById_DeletesRightCustomer()
        {
            var customers = new List<Member>();
            var orders = new List<Order>();

            var dbSetMock = DbSetMockBuilder.BuildDbSet(customers);
            var orderDbSet = DbSetMockBuilder.BuildDbSet(orders);

            var contextMock = new Mock<DbContext>();
            contextMock.Setup(c => c.Set<Member>()).Returns(dbSetMock);
            contextMock.Setup(c => c.Set<Order>()).Returns(orderDbSet);

            var db = new UnitOfWork(contextMock.Object);
            var repo = db.Members;

            var customer1 = new Member
            {
                Id = 1
            };

            var customer2 = new Member
            {
                Id = 2
            };

            repo.Add(customer1);
            repo.Add(customer2);
            db.Save();

            repo.DeleteById(1);
            db.Save();

            var actual = repo.GetById(1);
            Assert.Null(actual);
        }
    }
}
