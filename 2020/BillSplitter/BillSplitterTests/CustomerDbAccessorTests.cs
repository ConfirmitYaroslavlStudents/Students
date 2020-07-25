using BillSplitter.Data;
using BillSplitter.Models;
using System;
using System.Linq;
using Xunit;

namespace BillSplitterTests
{
    public class CustomerDbAccessorTests
    {
        [Fact]
        public void AddCustomer_AddNewCustomerInDB()
        {
            using var db = InMemoryContextBuilder.Build();
            var accessor = new CustomerDbAccessor(db);

            var customer = new Customer()
            {
                Id = 1
            };

            accessor.AddCustomer(customer);

            Assert.True(db.Customer.Contains(customer));
        }

        [Fact]
        public void GetCustomerById_ReturnsRightCustomer()
        {
            using var db = InMemoryContextBuilder.Build();
            var accessor = new CustomerDbAccessor(db);

            var customer1 = new Customer()
            {
                Id = 1
            };
            var customer2 = new Customer()
            {
                Id = 2
            };

            db.Customer.AddRange(customer1, customer2);
            db.SaveChanges();

            var expected = customer2;
            var actual = accessor.GetCustomerById(2);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetCustomerById_ReturnsNull()
        {
            using var db = InMemoryContextBuilder.Build();
            var accessor = new CustomerDbAccessor(db);

            Customer expected = null;
            var actual = accessor.GetCustomerById(1);

            Assert.Equal(expected, actual);
        }
    }
}
