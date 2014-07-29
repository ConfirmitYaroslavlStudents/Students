using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefactoringSample;

namespace RefactoringSampleTests
{
    [TestClass]
    public class RefactoringTests
    {
        public bool IsInStatement(string statement,string value)
        {
            if (statement.IndexOf(value, StringComparison.Ordinal) != -1)
            {
                    return true;
            }
            return false;
        }

        public List<string> GetStatementsToString(Customer customer)
        {
            var statementsToString = new List<string>
            {
                new Statement(customer).StandartString(),
                new Statement(customer).JsonToString()
            };
            return statementsToString;
        }
        [TestMethod]
        public void Customer_RentedNothing_Shouldpass()
        {
            const string name = "Nemo";


            var statementsToString = GetStatementsToString(new Customer(name));

            foreach (var statement in statementsToString)
            {
                Assert.IsTrue(IsInStatement(statement, name));
                Assert.IsTrue(IsInStatement(statement, 0.ToString(CultureInfo.InvariantCulture)));
                Assert.IsTrue(IsInStatement(statement, 0.ToString(CultureInfo.InvariantCulture))); 
            }

            //Old test
            var expectedstatement = String.Format("Учет аренды для {0}{1}", name, Environment.NewLine);
            expectedstatement += String.Format("Сумма задолженности составляет {0}{1}", 0.ToString(CultureInfo.InvariantCulture), Environment.NewLine);
            expectedstatement += string.Format("Вы заработали {0} за активность", 0.ToString(CultureInfo.InvariantCulture));

            Assert.AreEqual(expectedstatement, new Statement(new Customer(name)).StandartString());
        }

        [TestMethod]
        public void Customer_RentedSingleMovie_Shouldpass()
        {
            const string name = "Nemo";

            var customerNemo = new Customer(name);
            var mrNobody = new Rental(new PriceRegular(), "Mr.Nobody");
            mrNobody.DaysRented = 1;
            customerNemo.Rentals.Add(mrNobody);


            var statementsToString = GetStatementsToString(customerNemo);

            foreach (var statement in statementsToString)
            {
                Assert.IsTrue(IsInStatement(statement, name));
                Assert.IsTrue(IsInStatement(statement, mrNobody.Movie.Title));
                Assert.IsTrue(IsInStatement(statement, 2.ToString(CultureInfo.InvariantCulture)));
                Assert.IsTrue(IsInStatement(statement, 1.ToString(CultureInfo.InvariantCulture)));
            }

            //Old test
            var expectedstatement = String.Format("Учет аренды для {0}{1}", name, Environment.NewLine);
            expectedstatement += String.Format("\t {0} \t {1} {2}", mrNobody.Movie.Title, 2.ToString(CultureInfo.InvariantCulture), Environment.NewLine);

            expectedstatement += String.Format("Сумма задолженности составляет {0}{1}", 2.ToString(CultureInfo.InvariantCulture), Environment.NewLine);
            expectedstatement += String.Format("Вы заработали {0} за активность", 1.ToString(CultureInfo.InvariantCulture));

            Assert.AreEqual(expectedstatement, new Statement(customerNemo).StandartString());
        }

        [TestMethod]
        public void Customer_RentedMultipleMovies_Shouldpass()
        {
            const string name = "Nemo";

            var customerNemo = new Customer(name);
            var mrNobody = new Rental(new PriceRegular(), "Mr.Nobody");
            mrNobody.DaysRented = 5;
            customerNemo.Rentals.Add(mrNobody);

            var movie2 = new Rental(new PriceNewRelease(), "Another movie for Nemo");
            movie2.DaysRented = 10;
            customerNemo.Rentals.Add(movie2);

            var movie3 = new Rental(new PriceChildren(), "Third Movie for Nemo");
            movie3.DaysRented = 23;
            customerNemo.Rentals.Add(movie3);


            var statementsToString = GetStatementsToString(customerNemo);

            foreach (var statement in statementsToString)
            {
                Assert.IsTrue(IsInStatement(statement, name));
                Assert.IsTrue(IsInStatement(statement, mrNobody.Movie.Title));
                Assert.IsTrue(IsInStatement(statement, movie2.Movie.Title));
                Assert.IsTrue(IsInStatement(statement, movie3.Movie.Title));
                Assert.IsTrue(IsInStatement(statement, 6.5.ToString(CultureInfo.InvariantCulture)));
                Assert.IsTrue(IsInStatement(statement, 30.ToString(CultureInfo.InvariantCulture)));
                Assert.IsTrue(IsInStatement(statement, 31.5.ToString(CultureInfo.InvariantCulture)));
                Assert.IsTrue(IsInStatement(statement, 68.ToString(CultureInfo.InvariantCulture)));
                Assert.IsTrue(IsInStatement(statement, 4.ToString(CultureInfo.InvariantCulture)));
            }

            //Old test
            var expectedstatement = String.Format("Учет аренды для {0}{1}", name, Environment.NewLine);

            expectedstatement += String.Format("\t {0} \t {1} {2}", mrNobody.Movie.Title, 6.5.ToString(CultureInfo.InvariantCulture), Environment.NewLine);
            expectedstatement += String.Format("\t {0} \t {1} {2}", movie2.Movie.Title, 30.ToString(CultureInfo.InvariantCulture), Environment.NewLine);
            expectedstatement += String.Format("\t {0} \t {1} {2}", movie3.Movie.Title, 31.5.ToString(CultureInfo.InvariantCulture), Environment.NewLine);

            expectedstatement += String.Format("Сумма задолженности составляет {0}{1}", 68.ToString(CultureInfo.InvariantCulture), Environment.NewLine);
            expectedstatement += String.Format("Вы заработали {0} за активность", 4.ToString(CultureInfo.InvariantCulture));

            Assert.AreEqual(expectedstatement, new Statement(customerNemo).StandartString());
        }

        [TestMethod]
        public void ManyCustomers_RentedMultipleMovies_Shouldpass()
        {
            const string name = "Nemo";
            const string name2 = "Neo";

            var customerNemo = new Customer(name);
            var customerNeo = new Customer(name2);

            var mrNobody = new Rental(new PriceRegular(), "Mr.Nobody");
            mrNobody.DaysRented = 1;


            var movie2 = new Rental(new PriceNewRelease(), "Another movie for Nemo");
            movie2.DaysRented = 1;

            var matrix = new Rental(new PriceChildren(), "The Matrix");
            matrix.DaysRented = 1;

            customerNemo.Rentals.Add(mrNobody);
            customerNemo.Rentals.Add(movie2);

            customerNeo.Rentals.Add(matrix);
            customerNeo.Rentals.Add(movie2);


            var statementsToString = GetStatementsToString(customerNemo);

            foreach (var statement in statementsToString)
            {
                Assert.IsTrue(IsInStatement(statement, name));
                Assert.IsTrue(IsInStatement(statement, mrNobody.Movie.Title));
                Assert.IsTrue(IsInStatement(statement, movie2.Movie.Title));
                Assert.IsTrue(IsInStatement(statement, 2.ToString(CultureInfo.InvariantCulture)));
                Assert.IsTrue(IsInStatement(statement, 3.ToString(CultureInfo.InvariantCulture)));
                Assert.IsTrue(IsInStatement(statement, 5.ToString(CultureInfo.InvariantCulture)));
            }

            statementsToString = GetStatementsToString(customerNeo);

            foreach (var statement in statementsToString)
            {
                Assert.IsTrue(IsInStatement(statement, name2));
                Assert.IsTrue(IsInStatement(statement, matrix.Movie.Title));
                Assert.IsTrue(IsInStatement(statement, movie2.Movie.Title));
                Assert.IsTrue(IsInStatement(statement, 1.5.ToString(CultureInfo.InvariantCulture)));
                Assert.IsTrue(IsInStatement(statement, 3.ToString(CultureInfo.InvariantCulture)));
                Assert.IsTrue(IsInStatement(statement, 4.5.ToString(CultureInfo.InvariantCulture)));
                Assert.IsTrue(IsInStatement(statement, 2.ToString(CultureInfo.InvariantCulture)));
            }


            //Old test
            var statementNemo = String.Format("Учет аренды для {0}{1}", name, Environment.NewLine);

            statementNemo += String.Format("\t {0} \t {1} {2}", mrNobody.Movie.Title, 2.ToString(CultureInfo.InvariantCulture), Environment.NewLine);
            statementNemo += String.Format("\t {0} \t {1} {2}", movie2.Movie.Title, 3.ToString(CultureInfo.InvariantCulture), Environment.NewLine);

            statementNemo += String.Format("Сумма задолженности составляет {0}{1}", 5.ToString(CultureInfo.InvariantCulture), Environment.NewLine);
            statementNemo += String.Format("Вы заработали {0} за активность", 2.ToString(CultureInfo.InvariantCulture));

            Assert.AreEqual(statementNemo, new Statement(customerNemo).StandartString());


            var statementNeo = String.Format("Учет аренды для {0}{1}", name2, Environment.NewLine);

            statementNeo += String.Format("\t {0} \t {1} {2}", matrix.Movie.Title, 1.5.ToString(CultureInfo.InvariantCulture), Environment.NewLine);
            statementNeo += String.Format("\t {0} \t {1} {2}", movie2.Movie.Title, 3.ToString(CultureInfo.InvariantCulture), Environment.NewLine);

            statementNeo += String.Format("Сумма задолженности составляет {0}{1}", 4.5.ToString(CultureInfo.InvariantCulture), Environment.NewLine);
            statementNeo += String.Format("Вы заработали {0} за активность", 2.ToString(CultureInfo.InvariantCulture));

            Assert.AreEqual(statementNeo, new Statement(customerNeo).StandartString());
        }
    }
}
