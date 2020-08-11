using System.Collections.Generic;
using System.Linq;
using BillSplitter.Calculators;
using BillSplitter.Models;
using Xunit;

namespace BillSplitterTests
{
    public class OrderPriceCalculatorTests
    {
        [Fact]
        public void CalculatePriceForEquallySharedOrder_ReturnsCorrectPrice()
        {
            var position = new PositionSeeder().Seed().Last();

            var specialOrder = new Order()
            {
                Position = position,
                Quantity = 1.5m
            };

            var equallySharedOrder1 = new Order()
            {
                Position = position,
                Quantity = null
            };

            var equallySharedOrder2 = new Order()
            {
                Position = position,
                Quantity = null
            };

            position.Orders = new List<Order>{specialOrder, equallySharedOrder1, equallySharedOrder2};

            var expected = 0.75m;
            var actual = new OrderPriceCalculator().CalculatePrice(equallySharedOrder1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculatePriceForSpecialOrder_ReturnsCorrectPrice()
        {
            var position = new PositionSeeder().Seed().Last();

            var specialOrder = new Order()
            {
                Position = position,
                Quantity = 1.5m
            };

            var equallySharedOrder1 = new Order()
            {
                Position = position,
                Quantity = null
            };

            var equallySharedOrder2 = new Order()
            {
                Position = position,
                Quantity = null
            };

            position.Orders = new List<Order> { specialOrder, equallySharedOrder1, equallySharedOrder2 };

            var expected = 1.5m;
            var actual = new OrderPriceCalculator().CalculatePrice(specialOrder);

            Assert.Equal(expected, actual);
        }
    }
}