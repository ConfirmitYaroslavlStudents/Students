using BillSplitter.Data;
using BillSplitter.Models;
using System.Linq;
using Xunit;

namespace BillSplitterTests
{
    public class CustomerDbAccessorTests
    {
        [Fact]
        public void AddCustomer_AddNewCustomerInDb()
        {
            using var db = InMemoryContextBuilder.Build();
            var accessor = new CustomerRepository(db);

            var customer = new Customer()
            {
                Id = 1
            };

            accessor.Add(customer);

            Assert.True(db.Customers.Contains(customer));
        }

        [Fact]
        public void GetCustomerById_ReturnsRightCustomer()
        {
            using var db = InMemoryContextBuilder.Build();
            var accessor = new CustomerRepository(db);

            var customer1 = new Customer()
            {
                Id = 1
            };
            var customer2 = new Customer()
            {
                Id = 2
            };

            db.Customers.AddRange(customer1, customer2);
            db.SaveChanges();

            var expected = customer2;
            var actual = accessor.GetById(2);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetCustomerById_ReturnsNull()
        {
            using var db = InMemoryContextBuilder.Build();
            var accessor = new CustomerRepository(db);

            Customer expected = null;
            var actual = accessor.GetById(1);

            Assert.Equal(expected, actual);
        }
    }
}
