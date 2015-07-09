using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring;

namespace VideoService.UnitTests
{
    [TestClass]
    public class UnitTests
    {
        private readonly StringStatementBuilder _statementBuilder = new StringStatementBuilder();
        
        [TestMethod]
        public void GetJsonStatement_CustomerIsProperlySerializedAndDeserialized()
        {
            const string customerName = "TestCustomer";
            var customer = new Customer(customerName);
            var movies = new[] { new Movie("Inception"), new Movie("HarryPotter") };
            var rentalList = new List<Rental>
            {
                new RentalForNewReleaseMovie(daysRented: 1) { Movie = movies[0] },
                new RentalForChildrenMovie(daysRented: 4) { Movie = movies[1] }
            };
            customer.Rentals = rentalList;

            var jsonString = customer.GetStatement(new JsonStatement());
            var serializer = new DataContractJsonSerializer(typeof(Customer));
            Customer deserializedCustomer;
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                deserializedCustomer = (Customer)serializer.ReadObject(stream);
            }

            Assert.AreEqual(customerName, deserializedCustomer.Name);
            var firstMovieName = deserializedCustomer.Rentals[0].Movie.Title;
            var secondMovieName = deserializedCustomer.Rentals[1].Movie.Title;
            Assert.AreEqual(movies[0].Title, firstMovieName);
            Assert.AreEqual(movies[1].Title, secondMovieName);
        }

        [TestMethod]
        public void GetStatement_StringStatementIsProperlyGenerated_EmptyListOfMovies()
        {
            var result = _statementBuilder.GetStringOfCustomerName("TestCustomer");
            result += _statementBuilder.GetStringOfTotalRental(0);
            result += _statementBuilder.GetStringOfFrequentRenterPoints(0);

            var customer = new Customer("TestCustomer");
            Assert.IsTrue(result.Equals(customer.GetStatement(new StringStatement())));
        }

        [TestMethod]
        public void GetStatement_StringStatementIsProperlyGenerated_OneDayRentalOfNewReleaseMovie()
        {
            var result = _statementBuilder.GetStringOfCustomerName("TestCustomer");
            result += _statementBuilder.GetStringOfRentalForCurrentMovie("Inception", 3);
            result += _statementBuilder.GetStringOfTotalRental(3);
            result += _statementBuilder.GetStringOfFrequentRenterPoints(1);

            var movie = new Movie("Inception");
            var customer = new Customer("TestCustomer");
            var rentalList = new List<Rental> { new RentalForNewReleaseMovie(daysRented: 1) { Movie = movie } };
            customer.Rentals = rentalList;
            Assert.IsTrue(result.Equals(customer.GetStatement(new StringStatement())));
        }

        [TestMethod]
        public void GetStatement_StringStatementIsProperlyGenerated_ThreeDaysRentalOfNewReleaseMovie()
        {
            var result = _statementBuilder.GetStringOfCustomerName("TestCustomer");
            result += _statementBuilder.GetStringOfRentalForCurrentMovie("Inception", 9);
            result += _statementBuilder.GetStringOfTotalRental(9);
            result += _statementBuilder.GetStringOfFrequentRenterPoints(2);

            var movie = new Movie("Inception");
            var customer = new Customer("TestCustomer");
            var rentalList = new List<Rental> { new RentalForNewReleaseMovie(daysRented: 3) { Movie = movie } };
            customer.Rentals = rentalList;
            Assert.IsTrue(result.Equals(customer.GetStatement(new StringStatement())));
        }

        [TestMethod]
        public void GetStatement_StringStatementIsProperlyGenerated_OneDayRentalOfRegularMovie()
        {
            var result = _statementBuilder.GetStringOfCustomerName("TestCustomer");
            result += _statementBuilder.GetStringOfRentalForCurrentMovie("Inception", 2);
            result += _statementBuilder.GetStringOfTotalRental(2);
            result += _statementBuilder.GetStringOfFrequentRenterPoints(1);

            var movie = new Movie("Inception");
            var customer = new Customer("TestCustomer");
            var rentalList = new List<Rental> { new RentalForRegularMovie(daysRented: 1) { Movie = movie } };
            customer.Rentals = rentalList;
            Assert.IsTrue(result.Equals(customer.GetStatement(new StringStatement())));
        }

        [TestMethod]
        public void GetStatement_StringStatementIsProperlyGenerated_ThreeDaysRentalOfChildrenMovie()
        {
            var result = _statementBuilder.GetStringOfCustomerName("TestCustomer");
            result += _statementBuilder.GetStringOfRentalForCurrentMovie("Inception", 1.5);
            result += _statementBuilder.GetStringOfTotalRental(1.5);
            result += _statementBuilder.GetStringOfFrequentRenterPoints(1);

            var movie = new Movie("Inception");
            var customer = new Customer("TestCustomer");
            var rentalList = new List<Rental> { new RentalForChildrenMovie(daysRented: 3) { Movie = movie } };
            customer.Rentals = rentalList;
            Assert.IsTrue(result.Equals(customer.GetStatement(new StringStatement())));
        }
    }
}
