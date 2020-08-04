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

            Position a = new Position
            {
                Name = "a",
                Price = 2,
                Quantity = 10
            };
            Position b = new Position
            {
                Name = "b",
                Price = 3,
                Quantity = 20
            };
            Position c = new Position
            {
                Name = "c",
                Price = 4,
                Quantity = 30
            };
            var customer = new Customer
            {
                Orders = new List<Order>
                {
                    new Order
                    {
                        Position = a
                    },
                    new Order
                    {
                        Position = b
                       
                    },
                    new Order
                    {
                       Position = c
                    }
                }
            };

            a.Orders = new List<Order>();
            a.Orders.Add(new Order
            {
                Position = a
            });
            a.Orders.Add(new Order
            {
                Position = a
            });

            b.Orders = new List<Order>();
            b.Orders.Add(new Order
            {
                Position = b
            });
            b.Orders.Add(new Order
            {
                Position = b
            });
            b.Orders.Add(new Order
            {
                Position = b
            });
            c.Orders = new List<Order>();
            c.Orders.Add(new Order
            {
                Position = c
            });


            var expected = new List<Position>
                {
                   new Position
                        {
                            Name = "a",
                            Price = 10m,
                        },

                   new Position
                        {
                            Name = "b",
                            Price = 20m,
                        },
                    new  Position
                    {
                            Name = "c",
                            Price = 120m,
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
