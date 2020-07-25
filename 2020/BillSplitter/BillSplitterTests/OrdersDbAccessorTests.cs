using BillSplitter.Data;
using BillSplitter.Models;
using System;
using System.Linq;
using Xunit;

namespace BillSplitterTests
{
    public class OrdersDbAccessorTests
    {
        [Fact]
        public void AddOrder_AddNewOrdernDB()
        {
            using var db = InMemoryContextBuilder.Build();
            var accessor = new OrdersDbAccessor(db);

            var order = new Order()
            {
                Id = 1,
            };

            accessor.AddOrder(order);

            Assert.True(db.Orders.Contains(order));
        }

    }
}
