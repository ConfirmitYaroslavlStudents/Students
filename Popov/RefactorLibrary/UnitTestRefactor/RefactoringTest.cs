using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefactorLibrary;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace UnitTestRefactor
{
    [TestClass]
    public class RefactoringTest
    {
        [TestMethod]
        public void TestEqualsStatement()
        {
            var obj1 = new Customer("Sergey", new List<Rental>());
            obj1.Rentals.Add(new Rental(new Movie("Noi", new NewReleasePrice()), 10));
            var st1 = new Statement(obj1);
            

            var obj2 = new Customer("Sergey", new List<Rental>());
            obj2.Rentals.Add(new Rental(new Movie("Noi", new NewReleasePrice()), 10));
            var st2 = new Statement(obj2);

            Assert.AreEqual(st1, st2);

        }

        [TestMethod]
        public void TestEqualsSimpleString()
        {
            var obj1 = new Customer("Sergey", new List<Rental>());
            obj1.Rentals.Add(new Rental(new Movie("Noi", new NewReleasePrice()), 10));
            var st1 = new Statement(obj1).ToSimpleString();

            var obj2 = new Customer("Sergey", new List<Rental>());
            obj2.Rentals.Add(new Rental(new Movie("Noi", new NewReleasePrice()), 10));
            var st2 = new Statement(obj2).ToSimpleString();

            Assert.AreEqual(st1, st2);

        }

        [TestMethod]
        public void TestEqualsJsonString()
        {
            var obj1 = new Customer("Sergey", new List<Rental>());
            obj1.Rentals.Add(new Rental(new Movie("Noi", new NewReleasePrice()), 10));
            var st1 = new Statement(obj1).ToJson();

            var obj2 = new Customer("Sergey", new List<Rental>());
            obj2.Rentals.Add(new Rental(new Movie("Noi", new NewReleasePrice()), 10));
            var st2 = new Statement(obj2).ToJson();

            Assert.AreEqual(st1, st2);

        }

        [TestMethod]
        public void TestJsonString()
        {
            var obj = new Customer("Sergey", new List<Rental>());
            obj.Rentals.Add(new Rental(new Movie("Noi", new NewReleasePrice()), 10));
            var jsonString = new Statement(obj).ToJson();
            var jsonConvertString = JsonConvert.SerializeObject(new Statement(obj), Formatting.Indented);
            Assert.AreEqual(jsonString, jsonConvertString);

        }

        [TestMethod]
        public void TestEqualsName()
        {
            const string nameClient = "Sergey";
            var client = new Customer(nameClient, new List<Rental>());
            Assert.AreEqual(client.Name, nameClient);
        }

        [TestMethod]
        public void TestEqualsRental()
        {
            var rentals = new List<Rental>
            {
                new Rental(new Movie("Noi", new NewReleasePrice()), 10),
                new Rental(new Movie("Macho and Botan", new RegularPrice()), 1)
            };

            var client = new Customer("Unknow", rentals);
            client.Rentals.Add(new Rental(new Movie("Noi", new NewReleasePrice()), 10));
            client.Rentals.Add(new Rental(new Movie("Macho and Botan", new RegularPrice()), 1));

            Assert.AreEqual(client.Rentals, client.Rentals);
        }

        [TestMethod]
        public void TestEqualsMovie()
        {
            const string titleMovie = "Noi";
            var movie = new Movie(titleMovie, new NewReleasePrice());
            Assert.AreEqual(titleMovie,movie.Title);
        }

        [TestMethod]
        public void TestEqualsDaysRented()
        {
            const int daysRented = 10;
            var rental = new Rental(new Movie("unknow", new ChildrensPrice()), daysRented);
            Assert.AreEqual(rental.DaysRented, daysRented);
        }
    }
}
