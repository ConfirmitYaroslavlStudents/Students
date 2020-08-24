using System.Collections.Generic;
using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BillSplitterTests.RepositoryTests
{
    public class CustomerRepositoryTests : TestsBase
    {
        [Fact]
        public void AddCustomer_AddNewCustomerInDb()
        {
            var db = new UnitOfWork(Context);
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
            var db = new UnitOfWork(Context);
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
            var db = new UnitOfWork(Context);
            var repo = db.Members;

            var actual = repo.GetById(1);

            Assert.Null(actual);
        }

        [Fact]
        public void DeleteById_DeletesRightCustomer()
        {
            var db = new UnitOfWork(Context);
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
