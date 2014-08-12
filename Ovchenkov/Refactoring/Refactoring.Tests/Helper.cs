using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace Refactoring.Tests
{
    public static class Helper
    {
        public static Dictionary<string, double> GetMoviesWithPrice(Rental[] rentals)
        {
            var movies = new Dictionary<string, double>();

            foreach (var rental in rentals)
            {
                var priceOfMovie = rental.GetPrice();
                movies[rental.Movie.Title] = priceOfMovie;
            }
            return movies;
        }

        private static void Replace(XmlNode statementTemplate, string tag, string value)
        {
            var xmlNode = statementTemplate.SelectSingleNode(tag);
            if (xmlNode != null)
                xmlNode.InnerText = value;
        }

        private static string GetMoviesString(Dictionary<string, double> movies)
        {
            string moviesString = null;
            foreach (var movie in movies)
            {
                moviesString += String.Format("\t {0} \t {1} {2}", movie.Key,
                    movie.Value.ToString(CultureInfo.InvariantCulture), Environment.NewLine);
            }
            return moviesString;
        }

        public static bool CheckStatement(Statement statement, Dictionary<string, double> movies, string name, double totalAmount, int frequencyPoints)
        {
            return statement.Movies.SequenceEqual(movies) && statement.Name == name && statement.TotalAmount.CompareTo(totalAmount) == 0 &&
                   statement.FrequentRenterPoints == frequencyPoints;
        }

        public static bool CheckStringStatement(string statement, Dictionary<string, double> movies, string name, double totalAmount, int frequencyPoints)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(Properties.Resources.StatementTemplate);
            var result = xmlDocument.SelectSingleNode("statementTemplate");

            if (result == null)
            {
                throw new InvalidDataException("statementTemplate");
            }
            Replace(result, "CustomerName", name);
            Replace(result, "TotalAmount", totalAmount.ToString(CultureInfo.InvariantCulture));
            Replace(result, "FrequentRenterPoints", frequencyPoints.ToString(CultureInfo.InvariantCulture));
            Replace(result, "Movies", GetMoviesString(movies));

            return result.InnerText == statement;
        }

        public static bool CheckJsonStatement(string statement, Dictionary<string, double> movies, string name, double totalAmount, int frequencyPoints)
        {
            using (Stream stream = new MemoryStream())
            {
                var data = Encoding.UTF8.GetBytes(statement);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                var dataContractSerializer = new DataContractJsonSerializer(typeof(Statement));

                var customer = dataContractSerializer.ReadObject(stream) as Statement;

                return (CheckStatement(customer, movies, name, totalAmount, frequencyPoints));
            }
        }

        public static bool CheckUniversalStatement(StatementData statement, Dictionary<string, double> movies, string name,
            double totalAmount, int frequencyPoints)
        {
            var StringDataValidation = CheckStringStatement(statement.StringData, movies, name, totalAmount, frequencyPoints);
            var JsonDataValidation = CheckJsonStatement(statement.JsongData, movies, name, totalAmount, frequencyPoints);
            return StringDataValidation && JsonDataValidation;
        }
    }
}
