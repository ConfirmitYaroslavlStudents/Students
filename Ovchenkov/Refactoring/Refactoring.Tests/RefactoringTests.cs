using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Refactoring.Tests
{
    [TestClass]
    public class RefactoringTests
    {
        [TestMethod]
        public void Customer_Statement_WhenCustomerHaveNothing()
        {
            var expected = "Учет аренды для " + "Джон Рембо" + "\n";
            expected += "Сумма задолженности составляет " + "0" + "\n";
            expected += "Вы заработали " + "0" + " за активность";

            var customerRembo = new Customer("Джон Рембо");
            var actual = customerRembo.Statement();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Customer_Statement_WhenCustomerHaveNewRelease()
        {
            const int daysRented = 3;
            var remboFirstBlood = new Movie {PriceCode = TypeOfMovie.NewRelease, Title = "Rembo : II"};
            var remboRental = new Rental
            {
                Movie = remboFirstBlood,
                DaysRented = daysRented
            };

            var expected = "Учет аренды для " + "Джон Рембо" + "\n";
            expected += "\t" + "Rembo : II" + "\t" + (daysRented * 3).ToString(CultureInfo.InvariantCulture) + "\n";
            expected += "Сумма задолженности составляет " + (daysRented * 3).ToString(CultureInfo.InvariantCulture) + "\n";
            expected += "Вы заработали " + 2.ToString(CultureInfo.InvariantCulture) + " за активность";

            var customerRembo = new Customer("Джон Рембо");
            customerRembo.AddRental(remboRental);
            var actual = customerRembo.Statement();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Customer_Statement_WhenCustomerHaveRegularDaysRentedMoreThanTwo()
        {
            const int daysRented = 3;
            var remboFirstBlood = new Movie { PriceCode = TypeOfMovie.Regular, Title = "Rembo :First Blood" };
            var remboRental = new Rental
            {
                Movie = remboFirstBlood,
                DaysRented = daysRented
            };

            var expected = "Учет аренды для " + "Джон Рембо" + "\n";
            expected += "\t" + "Rembo :First Blood" + "\t" + (2 + (daysRented - 2) * 15).ToString(CultureInfo.InvariantCulture) + "\n";
            expected += "Сумма задолженности составляет " + (2 + (daysRented - 2) * 15).ToString(CultureInfo.InvariantCulture) + "\n";
            expected += "Вы заработали " + 1.ToString(CultureInfo.InvariantCulture) + " за активность";

            var customerRembo = new Customer("Джон Рембо");
            customerRembo.AddRental(remboRental);
            var actual = customerRembo.Statement();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Customer_Statement_WhenCustomerHaveRegularDaysRentedLessThanTwo()
        {
            const int daysRented = 1;
            var remboFirstBlood = new Movie { PriceCode = TypeOfMovie.Regular, Title = "Rembo :First Blood" };
            var remboRental = new Rental
            {
                Movie = remboFirstBlood,
                DaysRented = daysRented
            };

            var expected = "Учет аренды для " + "Джон Рембо" + "\n";
            expected += "\t" + "Rembo :First Blood" + "\t" + (2).ToString(CultureInfo.InvariantCulture) + "\n";
            expected += "Сумма задолженности составляет " + (2).ToString(CultureInfo.InvariantCulture) + "\n";
            expected += "Вы заработали " + 1.ToString(CultureInfo.InvariantCulture) + " за активность";

            var customerRembo = new Customer("Джон Рембо");
            customerRembo.AddRental(remboRental);
            var actual = customerRembo.Statement();

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Customer_Statement_WhenCustomerHaveChildrensDaysRentedLessThanThree()
        {
            const int daysRented = 1;
            var remboFirstBlood = new Movie { PriceCode = TypeOfMovie.Childrens, Title = "Rembo : Baby edition" };
            var remboRental = new Rental
            {
                Movie = remboFirstBlood,
                DaysRented = daysRented
            };

            var expected = "Учет аренды для " + "Джон Рембо" + "\n";
            expected += "\t" + "Rembo : Baby edition" + "\t" + (1.5).ToString(CultureInfo.InvariantCulture) + "\n";
            expected += "Сумма задолженности составляет " + (1.5).ToString(CultureInfo.InvariantCulture) + "\n";
            expected += "Вы заработали " + 1.ToString(CultureInfo.InvariantCulture) + " за активность";

            var customerRembo = new Customer("Джон Рембо");
            customerRembo.AddRental(remboRental);
            var actual = customerRembo.Statement();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Customer_Statement_WhenCustomerHaveChildrensDaysRentedMoreThanThree()
        {
            const int daysRented = 4;
            var remboFirstBlood = new Movie { PriceCode = TypeOfMovie.Childrens, Title = "Rembo : Baby edition" };
            var remboRental = new Rental
            {
                Movie = remboFirstBlood,
                DaysRented = daysRented
            };

            var expected = "Учет аренды для " + "Джон Рембо" + "\n";
            expected += "\t" + "Rembo : Baby edition" + "\t" + (1.5 + (daysRented - 3) * 1.5).ToString(CultureInfo.InvariantCulture) + "\n";
            expected += "Сумма задолженности составляет " + (1.5 + (daysRented - 3) * 1.5).ToString(CultureInfo.InvariantCulture) + "\n";
            expected += "Вы заработали " + 1.ToString(CultureInfo.InvariantCulture) + " за активность";

            var customerRembo = new Customer("Джон Рембо");
            customerRembo.AddRental(remboRental);
            var actual = customerRembo.Statement();

            Assert.AreEqual(expected, actual);
        }
    }
}
