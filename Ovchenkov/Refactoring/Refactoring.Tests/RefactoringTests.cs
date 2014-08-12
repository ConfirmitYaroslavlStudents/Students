using System.Collections.Generic;
using Xunit;
using Xunit.Extensions;

namespace Refactoring.Tests
{
    public class RefactoringTests
    {
        const string CustomerName = "John Rambo";
        const string NewReleaseMovieTitle = "Rembo : XX";
        const string RegularMovieTitle = "Rembo :First Blood";
        const string ChildrensMovieTitle = "Rembo : Baby edition"; 

        [Fact]
        public void Statement_WhenCustomerHaveNoRentals_ShouldPass()
        {
            const double expectedTotalAmount = 0;
            const int expectedFrequentRenterPoints = 0;

            var customer = new Customer(CustomerName);
            var statement = new Statement(customer);

            Assert.True(Helper.CheckStatement(statement, new Dictionary<string, double>(), CustomerName, expectedTotalAmount, expectedFrequentRenterPoints));
            Assert.True(Helper.CheckStringStatement(statement.GetStatement(new StringStatement()).StringData, new Dictionary<string, double>(), CustomerName, expectedTotalAmount, expectedFrequentRenterPoints));
            Assert.True(Helper.CheckJsonStatement(statement.GetStatement(new JsonStatement()).JsongData, new Dictionary<string, double>(), CustomerName, expectedTotalAmount, expectedFrequentRenterPoints));
            Assert.True(Helper.CheckUniversalStatement(statement.GetStatement(new UniversalStatement()), new Dictionary<string, double>(), CustomerName, expectedTotalAmount, expectedFrequentRenterPoints));
        }

        [Theory]
        [InlineData(0, 0, 0, 3.5, 3)]
        [InlineData(1, 1, 1, 6.5, 3)]
        [InlineData(3, 1, 1, 8, 3)]
        [InlineData(1, 4, 1, 8, 3)]
        [InlineData(1, 1, 2, 9.5, 4)]
        [InlineData(5, 4, 3, 18.5, 4)]
        [InlineData(2, 3, 1, 6.5, 3)]
        public void Statement__WhenCustomerHaveMultipleRentals_Shouldpass(int regularMovieDays, int childrensMovieDays, int newReleaseMovieDays,
            double expectedTotalAmount, int expectedFrequentRenterPoints)
        {
            var customerRentals = new[]
            {
                RentalFactory.Build(TypeOfMovie.NewRelease, NewReleaseMovieTitle, newReleaseMovieDays),
                RentalFactory.Build(TypeOfMovie.Regular, RegularMovieTitle, regularMovieDays),
                RentalFactory.Build(TypeOfMovie.Childrens, ChildrensMovieTitle, childrensMovieDays)
            };

            var customer = new Customer(CustomerName, customerRentals);
            var statement = new Statement(customer);

            Assert.True(Helper.CheckStatement(statement, Helper.GetMoviesWithPrice(customerRentals), CustomerName, expectedTotalAmount, expectedFrequentRenterPoints));
            Assert.True(Helper.CheckStringStatement(statement.GetStatement(new StringStatement()).StringData, Helper.GetMoviesWithPrice(customerRentals), CustomerName, expectedTotalAmount, expectedFrequentRenterPoints));
            Assert.True(Helper.CheckJsonStatement(statement.GetStatement(new JsonStatement()).JsongData, Helper.GetMoviesWithPrice(customerRentals), CustomerName, expectedTotalAmount, expectedFrequentRenterPoints));
            Assert.True(Helper.CheckUniversalStatement(statement.GetStatement(new UniversalStatement()), Helper.GetMoviesWithPrice(customerRentals), CustomerName, expectedTotalAmount, expectedFrequentRenterPoints));
        }

        [Fact]
        public void Statement_WhenCustomerHaveSingleRental_Shouldpass()
        {
            const int daysRented = 3;
            const double expectedTotalAmount = 9;
            const int expectedFrequentRenterPoints = 2;

            var customerRentals = new[]
            {
                RentalFactory.Build(TypeOfMovie.NewRelease, NewReleaseMovieTitle, daysRented)
            };
            var customer = new Customer(CustomerName, customerRentals[0]);
            var statement = new Statement(customer);

            Assert.True(Helper.CheckStatement(statement, Helper.GetMoviesWithPrice(customerRentals), CustomerName, expectedTotalAmount, expectedFrequentRenterPoints));
            Assert.True(Helper.CheckStringStatement(statement.GetStatement(new StringStatement()).StringData, Helper.GetMoviesWithPrice(customerRentals), CustomerName, expectedTotalAmount, expectedFrequentRenterPoints));
            Assert.True(Helper.CheckJsonStatement(statement.GetStatement(new JsonStatement()).JsongData, Helper.GetMoviesWithPrice(customerRentals), CustomerName, expectedTotalAmount, expectedFrequentRenterPoints));
            Assert.True(Helper.CheckUniversalStatement(statement.GetStatement(new UniversalStatement()), Helper.GetMoviesWithPrice(customerRentals), CustomerName, expectedTotalAmount, expectedFrequentRenterPoints));
        }
    }
}
