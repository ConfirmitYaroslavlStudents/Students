using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefactoringSample;

namespace RefactoringSampleTests
{
    [TestClass]
    public class RefactoringTests
    {
        public StringStatementGenerator StringStatement = new StringStatementGenerator();
        public JsonStatementGenerator JsonStatement = new JsonStatementGenerator();
        public CompositeStatementGenerator Composite = new CompositeStatementGenerator();
        public bool CheckStatement(Statement statement,Dictionary<string, double> moviePrices,string name, double totalAmount, int frequencyPoints)
        {
            return statement.MoviePrices.SequenceEqual(moviePrices) && statement.Name == name && statement.TotalAmount.CompareTo(totalAmount) == 0 &&
                   statement.FrequentRenterPoints == frequencyPoints;
        }

        public bool CheckStandartStringStatement(string statement, Dictionary<string, double> moviePrices, string name, double totalAmount, int frequencyPoints)
        {
            var result = new StringBuilder();
            result.Append(String.Format("Учет аренды для {0}{1}", name, Environment.NewLine));

            foreach (var movie in moviePrices.Keys)
            {

                result.Append(String.Format("\t {0} \t {1} {2}", movie, moviePrices[movie].ToString(CultureInfo.InvariantCulture), Environment.NewLine));
            }

            result.Append(String.Format("Сумма задолженности составляет {0}{1}", totalAmount.ToString(CultureInfo.InvariantCulture), Environment.NewLine));
            result.Append(String.Format("Вы заработали {0} за активность", frequencyPoints.ToString(CultureInfo.InvariantCulture)));
            return (statement == result.ToString());
        }

        public bool CheckJson(MemoryStream statement, Dictionary<string, double> moviePrices, string name, double totalAmount, int frequencyPoints)
        {
            statement.Position = 0;
            var ser = new DataContractJsonSerializer(typeof(Statement));
            return CheckStatement((Statement)ser.ReadObject(statement), moviePrices, name,totalAmount,frequencyPoints);
        }

        public bool CheckComposite(CompositeStatement compositeStatement, string stringStatement,
            MemoryStream jsonStatement)
        {
            return (compositeStatement.StringStatement.Equals(stringStatement) &&
                    compositeStatement.JsonStatement.ToString() == jsonStatement.ToString());
        }

        [TestMethod]
        public void Customer_RentedNothing_Shouldpass()
        {
            const string name = "Nemo";

            var statement = new Statement(new Customer(name));
            var stringStatement = StringStatement.Create(statement);
            var jsonStatement = JsonStatement.Create(statement);

            Assert.IsTrue(CheckStatement(statement, new Dictionary<string, double>(), name, 0, 0));
            Assert.IsTrue(CheckStandartStringStatement(stringStatement, new Dictionary<string, double>(), name, 0, 0));
            Assert.IsTrue(CheckJson(jsonStatement, new Dictionary<string, double>(), name, 0, 0));

            Assert.IsTrue(CheckComposite(Composite.Create(statement),stringStatement,jsonStatement));
        }

        [TestMethod]
        public void Customer_RentedSingleMovie_Shouldpass()
        {
            const string name = "Nemo";

            var customerNemo = new Customer(name);
            var mrNobody = new RentalBuilder().Build(new PriceRegular(), "Mr.Nobody",1);
            customerNemo.Rentals.Add(mrNobody);

            var statement = new Statement(customerNemo);

            var movieDict = new Dictionary<string, double>();
            movieDict["Mr.Nobody"] = 2;
            const int total = 2;
            const int frequency = 1;
            var stringStatement = StringStatement.Create(statement);
            var jsonStatement = JsonStatement.Create(statement);
            Assert.IsTrue(CheckStatement(statement, movieDict, name,total,frequency));
            Assert.IsTrue(CheckStandartStringStatement(stringStatement, movieDict, name, total, frequency));
            Assert.IsTrue(CheckJson(jsonStatement, movieDict, name, total,frequency));

            Assert.IsTrue(CheckComposite(Composite.Create(statement), stringStatement, jsonStatement));
        }

        [TestMethod]
        public void Customer_RentedMultipleMovies_Shouldpass()
        {
            const string name = "Nemo";

            var customerNemo = new Customer(name);
            var mrNobody = new RentalBuilder().Build(new PriceRegular(), "Mr.Nobody",5);
            mrNobody.DaysRented = 5;
            customerNemo.Rentals.Add(mrNobody);

            var movie2 = new RentalBuilder().Build(new PriceNewRelease(), "Another movie for Nemo",10);
            movie2.DaysRented = 10;
            customerNemo.Rentals.Add(movie2);

            var movie3 = new RentalBuilder().Build(new PriceChildren(), "Third Movie for Nemo", 23);
            movie3.DaysRented = 23;
            customerNemo.Rentals.Add(movie3);

            var statement = new Statement(customerNemo);

            var movieDict = new Dictionary<string, double>();
            movieDict["Mr.Nobody"] = 6.5;
            movieDict["Another movie for Nemo"] = 30;
            movieDict["Third Movie for Nemo"] = 31.5;
            const int total = 68;
            const int frequency = 4;

            var stringStatement = StringStatement.Create(statement);
            var jsonStatement = JsonStatement.Create(statement);
            Assert.IsTrue(CheckStatement(statement, movieDict, name, total, frequency));
            Assert.IsTrue(CheckStandartStringStatement(stringStatement, movieDict, name, total, frequency));
            Assert.IsTrue(CheckJson(jsonStatement, movieDict, name, total, frequency));

            Assert.IsTrue(CheckComposite(Composite.Create(statement), stringStatement, jsonStatement));
        }

        [TestMethod]
        public void ManyCustomers_RentedMultipleMovies_Shouldpass()
        {
            const string name = "Nemo";
            const string name2 = "Neo";

            var customerNemo = new Customer(name);
            var customerNeo = new Customer(name2);

            var mrNobody = new RentalBuilder().Build(new PriceRegular(), "Mr.Nobody",1);
            mrNobody.DaysRented = 1;


            var movie2 = new RentalBuilder().Build(new PriceNewRelease(), "Another movie for Nemo",1);
            movie2.DaysRented = 1;

            var matrix = new RentalBuilder().Build(new PriceChildren(), "The Matrix",1);
            matrix.DaysRented = 1;

            customerNemo.Rentals.Add(mrNobody);
            customerNemo.Rentals.Add(movie2);

            customerNeo.Rentals.Add(matrix);
            customerNeo.Rentals.Add(movie2);

            var statementNemo = new Statement(customerNemo);

            var movieDict = new Dictionary<string, double>();
            movieDict["Mr.Nobody"] = 2;
            movieDict["Another movie for Nemo"] = 3;
            const int total = 5;
            const int frequency = 2;

            var stringStatementNemo = StringStatement.Create(statementNemo);
            var jsonStatementNemo = JsonStatement.Create(statementNemo);

            Assert.IsTrue(CheckStatement(statementNemo, movieDict, name, total, frequency));
            Assert.IsTrue(CheckStandartStringStatement(stringStatementNemo, movieDict, name, total, frequency));
            Assert.IsTrue(CheckJson(jsonStatementNemo, movieDict, name, total, frequency));

            Assert.IsTrue(CheckComposite(Composite.Create(statementNemo), stringStatementNemo, jsonStatementNemo));

            var statementNeo = new Statement(customerNeo);

            var movieDictNeo = new Dictionary<string, double>();
            movieDictNeo["The Matrix"] = 1.5;
            movieDictNeo["Another movie for Nemo"] = 3;
            const double totalNeo = 4.5;
            const int frequencyNeo = 2;

            var stringStatementNeo = StringStatement.Create(statementNeo);
            var jsonStatementNeo = JsonStatement.Create(statementNeo);

            Assert.IsTrue(CheckStatement(statementNeo, movieDictNeo, name2, totalNeo, frequencyNeo));
            Assert.IsTrue(CheckStandartStringStatement(stringStatementNeo, movieDictNeo, name2, totalNeo, frequencyNeo));
            Assert.IsTrue(CheckJson(jsonStatementNeo, movieDictNeo, name2, totalNeo, frequencyNeo));

            Assert.IsTrue(CheckComposite(Composite.Create(statementNeo), stringStatementNeo, jsonStatementNeo));
        }
    }
}
