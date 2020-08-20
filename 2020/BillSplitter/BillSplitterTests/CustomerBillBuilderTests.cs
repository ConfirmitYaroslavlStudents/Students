using BillSplitter.Data;
using BillSplitter.Models;
using System.Collections.Generic;
using Xunit;

namespace BillSplitterTests
{
    public class CustomerBillBuilderTests
    {
        [Fact]
        public void Build_ReturnsCorrectPrices()
        {
            var positions = new PositionSeeder().Seed();
            var customer = new Member
            {
                Orders = new List<Order>
                {
                    new Order
                    {
                        Position = positions[2],
                        Quantity = null
                    },
                    new Order
                    {
                        Position = positions[2],
                        Quantity = null
                       
                    },
                    new Order
                    {
                       Position = positions[2],
                       Quantity = 1.5m
                    }
                }
            };

            positions[2].Orders = customer.Orders;

            var expected = new List<Position>()
            {
                new Position()
                {
                    Price = 0.75m
                },
                new Position()
                {
                    Price = 0.75m
                },
                new Position()
                {
                    Price = 1.5m
                }
            };

            var actual = new MemberBillBuilder().Build(customer);

            for (int i = 0; i < 3; i++)
                Assert.Equal(expected[i].Price, actual[i].Price);
        }
    }
}
