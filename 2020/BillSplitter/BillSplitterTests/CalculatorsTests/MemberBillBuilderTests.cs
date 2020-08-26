using System.Collections.Generic;
using BillSplitter.Calculators;
using BillSplitter.Models;
using Xunit;

namespace BillSplitterTests.CalculatorsTests
{
    public class MemberBillBuilderTests
    {
        [Fact]
        public void Build_ReturnsRightCountOfPositions()
        {
            var pizza = new Position { Name = "Pizza", Price = 1, Quantity = 1 };
            var cola = new Position { Name = "Cola", Price = 2, Quantity = 2 };
            var coffee = new Position { Name = "Coffee", Price = 3, Quantity = 3 };

            var luke = new Member { Name = "Luke" };

            var ordPizza = new Order { Member = luke, Position = pizza };
            var ordCola = new Order { Member = luke, Position = cola };

            luke.Orders = new List<Order> { ordPizza, ordCola };

            pizza.Orders = new List<Order> { ordPizza };
            cola.Orders = new List<Order> { ordCola };

            var builder = new MemberBillBuilder(new OrderPriceCalculator());

            var expexted = 2;
            var actual = builder.Build(luke).Count;

            Assert.Equal(expexted, actual);
        }
    }
}
