using BillSplitter.Data;
using BillSplitter.Models;
using BillSplitter.Models.InteractionLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BillSplitterTests
{
    public class OrdersDbAccessorTests
    {
        [Fact]
        public void AddOrder_AddNewOrderInDb()
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

        [Fact]
        public void AddOrders_AddAllNewOrdersInDb()
        {
            using var db = InMemoryContextBuilder.Build();
            var accessor = new OrdersDbAccessor(db);

            var customer = new Customer()
            {
                Id = 1
            };

            var positions = new List<InteractionLevelPosition>
            {
                new InteractionLevelPosition
                {
                    Id = 1,
                    QuantityNumerator = 1,
                    QuantityDenomenator = 2
                },
                new InteractionLevelPosition
                {
                    Id = 2,
                    QuantityNumerator = 1,
                    QuantityDenomenator = 2
                }
            };

            accessor.AddOrders(customer, positions);

            foreach (var position in positions)
            {
                Assert.True(db.Orders.FirstOrDefault(x => x.PositionId == position.Id) != null);
            }
        }

        [Fact]
        public void AddOrders_AddOnlyFirstOrderInDb()
        {
            using var db = InMemoryContextBuilder.Build();
            var accessor = new OrdersDbAccessor(db);

            var customer = new Customer()
            {
                Id = 1
            };

            var positions = new List<InteractionLevelPosition>
            {
                new InteractionLevelPosition
                {
                    Id = 1,
                    QuantityNumerator = 1,
                    QuantityDenomenator = 2
                },
                new InteractionLevelPosition
                {
                    Id = 2,
                    QuantityNumerator = 0,
                    QuantityDenomenator = 2
                }
            };

            accessor.AddOrders(customer, positions);

            Assert.True(db.Orders.FirstOrDefault(x => x.PositionId == positions[0].Id) != null);

            Assert.True(db.Orders.FirstOrDefault(x => x.PositionId == positions[1].Id) == null);

        }

    }
}
