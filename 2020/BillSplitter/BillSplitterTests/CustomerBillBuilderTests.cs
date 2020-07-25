using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
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

            Assert.Equal(expected, new CustomerBillBuilder().Build(customer));

        }
    }
}
