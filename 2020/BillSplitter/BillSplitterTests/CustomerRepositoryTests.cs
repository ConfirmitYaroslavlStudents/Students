using BillSplitter.Data;
using BillSplitter.Models;
using Xunit;

namespace BillSplitterTests
{
    public class CustomerRepositoryTests
    {
        [Fact]
        public void AddCustomer_AddNewCustomerInDb()
        {
            using var db = new InMemoryContextBuilder().Build();
            var uow = new UnitOfWork(db);

            var repo = uow.Customers;

            var customer = new Customer
            {
                Id = 1
            };

            repo.Add(customer);
            uow.Save();

            Assert.True(repo.GetById(1) != null);
        }

        [Fact]
        public void GetCustomerById_ReturnsRightCustomer()
        {
            using var db = new InMemoryContextBuilder().Build();
            var uow = new UnitOfWork(db);

            var repo = uow.Customers;

            var customer1 = new Customer
            {
                Id = 1
            };

            var customer2 = new Customer
            {
                Id = 2
            };

            repo.Add(customer1);
            repo.Add(customer2);
            uow.Save();

            var expected = customer2;
            var actual = repo.GetById(2);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetCustomerById_ReturnsNull()
        {
            using var db = new InMemoryContextBuilder().Build();
            var uow = new UnitOfWork(db);

            var repo = uow.Customers;

            var actual = repo.GetById(1);

            Assert.Null(actual);
        }

        [Fact]
        public void DeleteById_DeletesRightCustomer()
        {
            using var db = new InMemoryContextBuilder().Build();
            var uow = new UnitOfWork(db);

            var repo = uow.Customers;

            var customer1 = new Customer
            {
                Id = 1
            };

            var customer2 = new Customer
            {
                Id = 2
            };

            repo.Add(customer1);
            repo.Add(customer2);
            uow.Save();

            repo.DeleteById(1);
            uow.Save();

            var actual = repo.GetById(1);
            Assert.Null(actual);
        }
    }
}
