using System.Collections.Generic;
using BillSplitter.Calculators;
using BillSplitter.Models;
using Xunit;

namespace BillSplitterTests.CalculatorsTests
{
    public class BalanceCalculatorTests
    {
        [Fact]
        public void CalculateBalances_CalculatesRightCountOfBalances()
        {
            var luke = new Member { Name = "Luke" };
            var r2d2 = new Member { Name = "R2D2" };
            var han = new Member { Name = "Han" };

            var pizza = new Position { Name = "Pizza", ManagingMember = r2d2, Quantity = 1, Price = 1 };
            var coffee = new Position { Name = "Coffee", ManagingMember = luke, Quantity = 2, Price = 1 };

            var lukePizza = new Order { Member = luke, Position = pizza, Quantity = null };
            var hanPizza = new Order { Member = han, Position = pizza, Quantity = null };
            var hanCoffee = new Order { Member = han, Position = coffee, Quantity = 1 };

            pizza.Orders = new List<Order> { lukePizza, hanPizza };
            coffee.Orders = new List<Order> { hanCoffee };

            luke.Orders = new List<Order> { lukePizza };
            han.Orders = new List<Order> { hanPizza, hanCoffee };
            luke.ManagedPositions = new List<Position> { coffee };
            r2d2.ManagedPositions = new List<Position> { pizza };

            var balanceCalc = new BalanceCalculator(new OrderPriceCalculator());

            var expected = 2;
            var actual = balanceCalc.CalculateBalances(luke).Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateBalances_CalculatesRightBalances()
        {
            var luke = new Member { Name = "Luke" };
            var r2d2 = new Member { Name = "R2D2" };
            var han = new Member { Name = "Han" };

            var pizza = new Position { Name = "Pizza", ManagingMember = r2d2, Quantity = 1, Price = 1 };
            var coffee = new Position { Name = "Coffee", ManagingMember = luke, Quantity = 2, Price = 1 };

            var lukePizza = new Order { Member = luke, Position = pizza, Quantity = null };
            var hanPizza = new Order { Member = han, Position = pizza, Quantity = null };
            var hanCoffee = new Order { Member = han, Position = coffee, Quantity = 1 };

            pizza.Orders = new List<Order> { lukePizza, hanPizza };
            coffee.Orders = new List<Order> { hanCoffee };

            luke.Orders = new List<Order> { lukePizza };
            han.Orders = new List<Order> { hanPizza, hanCoffee };
            luke.ManagedPositions = new List<Position> { coffee };
            r2d2.ManagedPositions = new List<Position> { pizza };

            var balanceCalc = new BalanceCalculator(new OrderPriceCalculator());

            var actualBalance = balanceCalc.CalculateBalances(luke);

            var expectedPayment = -1.0m;
            var expectedDebt = 0.5m;

            Assert.Equal(expectedPayment, actualBalance["Han"]);
            Assert.Equal(expectedDebt, actualBalance["R2D2"]);
        }

        [Fact]
        public void CalculateDebts_CalculatesRightCountOfDebts()
        {
            var luke = new Member { Name = "Luke" };
            var r2d2 = new Member { Name = "R2D2" };
            var han = new Member { Name = "Han" };

            var pizza = new Position { Name = "Pizza", ManagingMember = r2d2, Quantity = 1, Price = 1 };
            var coffee = new Position { Name = "Coffee", ManagingMember = luke, Quantity = 2, Price = 1 };

            var lukePizza = new Order { Member = luke, Position = pizza, Quantity = null };
            var hanPizza = new Order { Member = han, Position = pizza, Quantity = null };
            var hanCoffee = new Order { Member = han, Position = coffee, Quantity = 1 };
            var r2d2Coffee = new Order { Member = r2d2, Position = coffee, Quantity = null };

            pizza.Orders = new List<Order> { lukePizza, hanPizza };
            coffee.Orders = new List<Order> { hanCoffee, r2d2Coffee };

            luke.Orders = new List<Order> { lukePizza };
            han.Orders = new List<Order> { hanPizza, hanCoffee };
            r2d2.Orders = new List<Order> { r2d2Coffee };
            luke.ManagedPositions = new List<Position> { coffee };
            r2d2.ManagedPositions = new List<Position> { pizza };

            var balanceCalc = new BalanceCalculator(new OrderPriceCalculator());

            var actual = balanceCalc.CalculateDebts(luke).Count;
            var expected = 2;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateDebts_CalculatesRightDebts()
        {
            var luke = new Member { Name = "Luke" };
            var r2d2 = new Member { Name = "R2D2" };
            var han = new Member { Name = "Han" };

            var pizza = new Position { Name = "Pizza", ManagingMember = r2d2, Quantity = 1, Price = 1 };
            var coffee = new Position { Name = "Coffee", ManagingMember = luke, Quantity = 2, Price = 1 };

            var lukePizza = new Order { Member = luke, Position = pizza, Quantity = null };
            var hanPizza = new Order { Member = han, Position = pizza, Quantity = null };
            var hanCoffee = new Order { Member = han, Position = coffee, Quantity = 1 };
            var r2d2Coffee = new Order { Member = r2d2, Position = coffee, Quantity = null };

            pizza.Orders = new List<Order> { lukePizza, hanPizza };
            coffee.Orders = new List<Order> { hanCoffee, r2d2Coffee };

            luke.Orders = new List<Order> { lukePizza };
            han.Orders = new List<Order> { hanPizza, hanCoffee };
            r2d2.Orders = new List<Order> { r2d2Coffee };
            luke.ManagedPositions = new List<Position> { coffee };
            r2d2.ManagedPositions = new List<Position> { pizza };

            var balanceCalc = new BalanceCalculator(new OrderPriceCalculator());

            var debts = balanceCalc.CalculateDebts(luke);

            var actualDebt1 = debts[han.Name];
            var expectedDebt1 = 1.0m;

            var actualDebt2 = debts[r2d2.Name];
            var expectedDebt2 = 0.5m;

            Assert.Equal(expectedDebt1, actualDebt1);
            Assert.Equal(expectedDebt2, actualDebt2);
        }

        [Fact]
        public void CalculatePayments_CalculatesRightCountOfPayments()
        {
            var luke = new Member { Name = "Luke" };
            var r2d2 = new Member { Name = "R2D2" };
            var han = new Member { Name = "Han" };

            var pizza = new Position { Name = "Pizza", ManagingMember = r2d2, Quantity = 1, Price = 2 };
            var coffee = new Position { Name = "Coffee", ManagingMember = luke, Quantity = 2, Price = 0.5m };

            var lukePizza = new Order { Member = luke, Position = pizza, Quantity = null };
            var hanPizza = new Order { Member = han, Position = pizza, Quantity = null };
            var hanCoffee = new Order { Member = han, Position = coffee, Quantity = 1 };
            var r2d2Coffee = new Order { Member = r2d2, Position = coffee, Quantity = null };

            pizza.Orders = new List<Order> { lukePizza, hanPizza };
            coffee.Orders = new List<Order> { hanCoffee, r2d2Coffee };

            luke.Orders = new List<Order> { lukePizza };
            han.Orders = new List<Order> { hanPizza, hanCoffee };
            r2d2.Orders = new List<Order> { r2d2Coffee };
            luke.ManagedPositions = new List<Position> { coffee };
            r2d2.ManagedPositions = new List<Position> { pizza };

            var balanceCalc = new BalanceCalculator(new OrderPriceCalculator());

            var actual = balanceCalc.CalculatePayments(luke).Count;
            var expected = 1;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculatePayments_CalculatesRightPayments()
        {
            var luke = new Member { Name = "Luke" };
            var r2d2 = new Member { Name = "R2D2" };
            var han = new Member { Name = "Han" };

            var pizza = new Position { Name = "Pizza", ManagingMember = r2d2, Quantity = 1, Price = 2 };
            var coffee = new Position { Name = "Coffee", ManagingMember = luke, Quantity = 2, Price = 0.5m };

            var lukePizza = new Order { Member = luke, Position = pizza, Quantity = null };
            var hanPizza = new Order { Member = han, Position = pizza, Quantity = null };
            var hanCoffee = new Order { Member = han, Position = coffee, Quantity = 1 };
            var r2d2Coffee = new Order { Member = r2d2, Position = coffee, Quantity = null };

            pizza.Orders = new List<Order> { lukePizza, hanPizza };
            coffee.Orders = new List<Order> { hanCoffee, r2d2Coffee };

            luke.Orders = new List<Order> { lukePizza };
            han.Orders = new List<Order> { hanPizza, hanCoffee };
            r2d2.Orders = new List<Order> { r2d2Coffee };
            luke.ManagedPositions = new List<Position> { coffee };
            r2d2.ManagedPositions = new List<Position> { pizza };

            var balanceCalc = new BalanceCalculator(new OrderPriceCalculator());

            var actual = balanceCalc.CalculatePayments(luke)[r2d2.Name];
            var expected = 0.5m;

            Assert.Equal(expected, actual);
        }
    }
}