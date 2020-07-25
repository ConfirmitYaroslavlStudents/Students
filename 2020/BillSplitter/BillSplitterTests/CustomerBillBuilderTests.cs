using BillSplitter.Data;
using BillSplitter.Models;
using System.Collections.Generic;
using Xunit;

namespace BillSplitterTests
{
    public class CustomerBillBuilderTests
    {
        [Fact]
        public void Build_ReturnsRightBuildedPositions()
        {
            var customer = new Customer
            {
                Orders = new List<Order>
                {
                    new Order
                    {
                        Position = new Position
                        {
                            Name = "a",
                            Price = 2,
                            Quantity = 10
                        },
                        Quantity = 0.5
                    },
                    new Order
                    {
                        Position = new Position
                        {
                            Name = "b",
                            Price = 3,
                            Quantity = 20
                        },
                        Quantity = 0.5
                    },
                    new Order
                    {
                        Position = new Position
                        {
                            Name = "c",
                            Price = 4,
                            Quantity = 30
                        },
                        Quantity = 0.5
                    }
                }
            };

            var expected = new List<Position>
                {
                   new Position
                        {
                            Name = "a",
                            Price = 1.0m,
                        },

                   new Position
                        {
                            Name = "b",
                            Price = 1.5m,
                        },
                    new  Position
                    {
                            Name = "c",
                            Price = 2.0m,
                    }

                };

            var actual = new CustomerBillBuilder().Build(customer);

            for (int i = 0; i < 3; i++)
            {
                Assert.Equal(expected[i].Name, actual[i].Name);
                Assert.Equal(expected[i].Price, actual[i].Price);
            }

        }
    }
}
