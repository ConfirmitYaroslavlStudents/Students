using System.Collections.Generic;
using Refactoring.Factories;
using Xunit;
using Xunit.Extensions;

namespace Refactoring.Tests
{
    public class RefactoringTests
    {
        private const string CustomerName = "John Rambo";
        private static readonly TypeOfMovie[] Types = {TypeOfMovie.NewRelease, TypeOfMovie.Regular, TypeOfMovie.Childrens};
        private static readonly string[] Titles = {"Rembo : XX", "Rembo :First Blood", "Rembo : Baby edition"};

        [Fact]
        public void Statement_WhenCustomerHaveNoRentals_ShouldPass()
        {
            const double expectedTotalAmount = 0;
            const int expectedFrequentRenterPoints = 0;

            StatementFactory.GetCustomerRental(CustomerName, null);
            var statement = StatementFactory.BuildStatement();

            TestsHelper.SetCustomerInformation(CustomerName, expectedTotalAmount, expectedFrequentRenterPoints);
            TestsHelper.SetMovieInformation(new Dictionary<string, double>());

            Assert.True(TestsHelper.CheckStatement(statement));
            Assert.True(TestsHelper.CheckStringStatement(StatementFactory.BuildStringStatement()));
            Assert.True(TestsHelper.CheckJsonStatement(StatementFactory.BuildJsonStatement()));
            Assert.True(TestsHelper.CheckUniversalStatement(StatementFactory.BuildUniversalStatement()));
        }

        [Theory]
        [InlineData(0, 0, 0, 3.5, 3)]
        [InlineData(1, 1, 1, 6.5, 3)]
        [InlineData(3, 1, 1, 8, 3)]
        [InlineData(1, 4, 1, 8, 3)]
        [InlineData(1, 1, 2, 9.5, 4)]
        [InlineData(5, 4, 3, 18.5, 4)]
        [InlineData(2, 3, 1, 6.5, 3)]
        public void Statement__WhenCustomerHaveMultipleRentals_Shouldpass(int regularMovieDays, int childrensMovieDays,
            int newReleaseMovieDays,
            double expectedTotalAmount, int expectedFrequentRenterPoints)
        {
            var days = new[] {newReleaseMovieDays, regularMovieDays, childrensMovieDays};

            RentalFactory.GetRentalInformation(Titles, Types, days);
            var customerRentals = RentalFactory.Build();

            StatementFactory.GetCustomerRental(CustomerName, customerRentals);
            var statement = StatementFactory.BuildStatement();

            TestsHelper.SetCustomerInformation(CustomerName, expectedTotalAmount, expectedFrequentRenterPoints);
            TestsHelper.SetMovieInformation(TestsHelper.GetMoviesWithPrice(customerRentals));

            Assert.True(TestsHelper.CheckStatement(statement));
            Assert.True(TestsHelper.CheckStringStatement(StatementFactory.BuildStringStatement()));
            Assert.True(TestsHelper.CheckJsonStatement(StatementFactory.BuildJsonStatement()));
            Assert.True(TestsHelper.CheckUniversalStatement(StatementFactory.BuildUniversalStatement()));
        }

        [Fact]
        public void Statement_WhenCustomerHaveSingleRental_Shouldpass()
        {
            const int daysRented = 3;
            const double expectedTotalAmount = 9;
            const int expectedFrequentRenterPoints = 2;

            RentalFactory.GetRentalInformation(Titles[0], TypeOfMovie.NewRelease, daysRented);
            var customerRentals = RentalFactory.Build();
        
            StatementFactory.GetCustomerRental(CustomerName, customerRentals);
            var statement = StatementFactory.BuildStatement();

            TestsHelper.SetCustomerInformation(CustomerName, expectedTotalAmount, expectedFrequentRenterPoints);
            TestsHelper.SetMovieInformation(TestsHelper.GetMoviesWithPrice(customerRentals));

            Assert.True(TestsHelper.CheckStatement(statement));
            Assert.True(TestsHelper.CheckStringStatement(StatementFactory.BuildStringStatement()));
            Assert.True(TestsHelper.CheckJsonStatement(StatementFactory.BuildJsonStatement()));
            Assert.True(TestsHelper.CheckUniversalStatement(StatementFactory.BuildUniversalStatement()));
        }
    }
}
