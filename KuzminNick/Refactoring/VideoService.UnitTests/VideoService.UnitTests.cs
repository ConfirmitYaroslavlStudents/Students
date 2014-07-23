using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VideoService.UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void CorretnessOfStatement_ForCase_AbsenceRental()
        {
            var result = "Учет аренды для " + "TestCustomer" + "\n"
                + "Сумма задолженности составляет " + 0 + "\n"
                + "Вы заработали " + 0 + " за активность";

            var customer = new Customer("TestCustomer");
            Assert.AreEqual(result, customer.Statement());
        }

        [TestMethod]
        public void CorretnessOfStatement_ForCase_OneDayOfRentalOfNewReleaseMovie()
        {
            var movie = new Movie("Inception");
            var result = "Учет аренды для " + "TestCustomer" + "\n"
                + "\t" + "Inception" + "\t" + "3" + "\n"
                + "Сумма задолженности составляет " + 3 + "\n"
                + "Вы заработали " + 1 + " за активность";

            var customer = new Customer("TestCustomer");
            var rentalList = new List<Rental> { new RentalForNewReleaseMovie(daysRented: 1) { Movie = movie } };
            customer.Rentals = rentalList;
            Assert.AreEqual(result, customer.Statement());
        }

        [TestMethod]
        public void CorretnessOfStatementAndFrequentRenterPoints_ForCase_ThreeDaysRentalOfNewReleaseMovie()
        {
            var movie = new Movie("The Wolf of Wall Street");
            var result = "Учет аренды для " + "TestCustomer" + "\n"
                + "\t" + "The Wolf of Wall Street" + "\t" + "9" + "\n"
                + "Сумма задолженности составляет " + 9 + "\n"
                + "Вы заработали " + 2 + " за активность";

            var customer = new Customer("TestCustomer");
            var rentalList = new List<Rental> { new RentalForNewReleaseMovie(daysRented: 3) { Movie = movie } };
            customer.Rentals = rentalList;
            Assert.AreEqual(result, customer.Statement());
        }

        [TestMethod]
        public void CorretnessOfStatement_ForCase_OneDayOfRentalOfRegularMovie()
        {
            var movie = new Movie("Seven Psychopaths");
            var result = "Учет аренды для " + "TestCustomer" + "\n"
                + "\t" + "Seven Psychopaths" + "\t" + "2" + "\n"
                + "Сумма задолженности составляет " + 2 + "\n"
                + "Вы заработали " + 1 + " за активность";

            var customer = new Customer("TestCustomer");
            var rentalList = new List<Rental> { new RentalForRegularMovie(daysRented: 1) { Movie = movie } };
            customer.Rentals = rentalList;
            Assert.AreEqual(result, customer.Statement());
        }

        [TestMethod]
        public void CorretnessOfStatement_ForCase_ThreeDaysOfRentalOfRegularMovie()
        {
            var movie = new Movie("Killing Them Softly");
            var result = "Учет аренды для " + "TestCustomer" + "\n"
                + "\t" + "Killing Them Softly" + "\t" + "3.5" + "\n"
                + "Сумма задолженности составляет " + "3.5" + "\n"
                + "Вы заработали " + 1 + " за активность";

            var customer = new Customer("TestCustomer");
            var rentalList = new List<Rental> { new RentalForRegularMovie(daysRented: 3) { Movie = movie } };
            customer.Rentals = rentalList;
            Assert.AreEqual(result, customer.Statement());
        }

        [TestMethod]
        public void CorretnessOfStatement_ForCase_ThreeDaysOfRentalOfChildrenMovie()
        {
            var movie = new Movie("Cloud Atlas");
            var result = "Учет аренды для " + "TestCustomer" + "\n"
                + "\t" + "Cloud Atlas" + "\t" + "1.5" + "\n"
                + "Сумма задолженности составляет " + "1.5" + "\n"
                + "Вы заработали " + 1 + " за активность";

            var customer = new Customer("TestCustomer");
            var rentalList = new List<Rental> { new RentalForChildrenMovie(daysRented: 3) { Movie = movie } };
            customer.Rentals = rentalList;
            Assert.AreEqual(result, customer.Statement());
        }

        [TestMethod]
        public void CorretnessOfStatement_ForCase_FourDaysOfRentalOfChildrenMovie()
        {
            var movie = new Movie("The Artist");
            var result = "Учет аренды для " + "TestCustomer" + "\n"
                + "\t" + "The Artist" + "\t" + "3" + "\n"
                + "Сумма задолженности составляет " + "3" + "\n"
                + "Вы заработали " + 1 + " за активность";

            var customer = new Customer("TestCustomer");
            var rentalList = new List<Rental> { new RentalForChildrenMovie(daysRented: 4) { Movie = movie } };
            customer.Rentals = rentalList;
            Assert.AreEqual(result, customer.Statement());
        }
    }
}
