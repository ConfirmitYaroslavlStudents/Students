using FilmService;
using FilmService.KindsOfStatements;
using FilmService.KindsOfMovies;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestsForFilmService
{
    [TestClass]
    public class FilmServiseTests
    {
        [TestMethod]
        public void SerializeDeserializeInJSONAndString_EqualsAreTrue()
        {
            var user = new Customer("Igor", StatementFactory.GetStatement());
            user.Rentals.Add(new Rental(new Movie("Edge of Tomorrow", new NewRelease()), 5));
            user.Rentals.Add(new Rental(new Movie("Gravity", new Regular()), 7));
            user.Rentals.Add(new Rental(new Movie("HOW TO TRAIN YOUR DRAGON 2", new Childrens()), 3));
            user.RequestAndSetDataStore();
            const string path = "userSerialize";

            var serializeData = user.CurrentDataStore;

            user.StatementGenerator.Serialize(path, user.CurrentDataStore);

            var deserializeData = user.StatementGenerator.Deserialize(path);

            Assert.AreEqual(serializeData, deserializeData);
        }

        [TestMethod]
        public void EqualsForGenerateAndHandmadeDataWithFewDaysRented_True()
        {
            var user = new Customer("Igor", new StatementInJSON());
            user.Rentals.Add(new Rental(new Movie("Edge of Tomorrow", new NewRelease()), 5));
            user.Rentals.Add(new Rental(new Movie("Gravity", new Regular()), 2));
            user.Rentals.Add(new Rental(new Movie("HOW TO TRAIN YOUR DRAGON 2", new Childrens()), 3));
            user.RequestAndSetDataStore();

            var generateData = user.CurrentDataStore;

            var name = "Igor";
            var rentalsData = new Dictionary<string, double>();
            rentalsData["Edge of Tomorrow"]=15;
            rentalsData["Gravity"]=2;
            rentalsData["HOW TO TRAIN YOUR DRAGON 2"]=1.5;
            double totalAmount = 18.5;
            int frequentRenterPoints = 4;
            var handmadeData = new DataStore(name, rentalsData, totalAmount, frequentRenterPoints);

            Assert.AreEqual(generateData, handmadeData);
        }

        [TestMethod]
        public void EqualsForGenerateAndHandmadeDataWithManyDaysRented_True()
        {
            var user = new Customer("Igor", new StatementInJSON());
            user.Rentals.Add(new Rental(new Movie("Edge of Tomorrow", new NewRelease()), 100));
            user.Rentals.Add(new Rental(new Movie("Gravity", new Regular()), 100));
            user.Rentals.Add(new Rental(new Movie("HOW TO TRAIN YOUR DRAGON 2", new Childrens()), 100));
            user.RequestAndSetDataStore();

            var generateData = user.CurrentDataStore;

            var name = "Igor";
            var rentalsData = new Dictionary<string, double>();
            rentalsData["Edge of Tomorrow"] = 300;
            rentalsData["Gravity"] = 149;
            rentalsData["HOW TO TRAIN YOUR DRAGON 2"] = 147;
            double totalAmount = 596;
            int frequentRenterPoints = 4;
            var handmadeData = new DataStore(name, rentalsData, totalAmount, frequentRenterPoints);

            Assert.AreEqual(generateData, handmadeData);
        }
        
    }
}
