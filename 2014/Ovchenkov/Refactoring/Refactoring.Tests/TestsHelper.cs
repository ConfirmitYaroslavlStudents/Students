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
    public static class TestsHelper
    {
        private static string _name;
        private static double _totalAmount;
        private static int _frequencyPoints;
        private static Dictionary<string, double> _movies;

        public static void SetCustomerInformation(string name, double totalAmount, int frequencyPoints)
        {
            _name = name;
            _totalAmount = totalAmount;
            _frequencyPoints = frequencyPoints;
        }

        public static void SetMovieInformation(Dictionary<string, double> movies)
        {
            _movies = movies;
        }

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

        public static bool CheckStatement(StatementTools.Statement statement)
        {
            return statement.Movies.SequenceEqual(_movies) && statement.Name == _name && statement.TotalAmount.CompareTo(_totalAmount) == 0 &&
                   statement.FrequentRenterPoints == _frequencyPoints;
        }

        public static bool CheckStringStatement(string statement)
        {
            var xmlDocument = new XmlDocument();
            var resources = Properties.Resources.StatementTemplate;

            xmlDocument.LoadXml(resources);
            var result = xmlDocument.SelectSingleNode("statementTemplate");

            if (result == null)
            {
                throw new InvalidDataException("statementTemplate");
            }
            Replace(result, "CustomerName", _name);
            Replace(result, "TotalAmount", _totalAmount.ToString(CultureInfo.InvariantCulture));
            Replace(result, "FrequentRenterPoints", _frequencyPoints.ToString(CultureInfo.InvariantCulture));
            Replace(result, "Movies", GetMoviesString(_movies));

            return result.InnerText == statement;
        }

        public static bool CheckJsonStatement(string statement)
        {
            using (Stream stream = new MemoryStream())
            {
                var data = Encoding.UTF8.GetBytes(statement);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                var dataContractSerializer = new DataContractJsonSerializer(typeof(StatementTools.Statement));

                var customer = dataContractSerializer.ReadObject(stream) as StatementTools.Statement;

                return (CheckStatement(customer));
            }
        }

        public static bool CheckUniversalStatement(string[] statements)
        {
            var stringDataValidation = CheckStringStatement(statements[0]);
            var jsonDataValidation = CheckJsonStatement(statements[1]);

            return stringDataValidation && jsonDataValidation;
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
    }
}
