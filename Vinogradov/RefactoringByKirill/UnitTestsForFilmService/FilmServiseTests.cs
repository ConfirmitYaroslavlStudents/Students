using System;
using FilmService;
using FilmService.KindsOfGenerators;
using FilmService.KindsOfMovies;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml;

namespace UnitTestsForFilmService
{
    //Should be more tests. Maybe use standart MS naming style
    [TestClass]
    public class FilmServiseTests
    {
        [TestMethod]
        public void SerializeAndDeserialize()
        {
            var user = new Customer("Igor", new StatementGeneratorJSON());
            user.Rentals.Add(new Rental(new Movie("Edge of Tomorrow", new CalculatorForMovieNewRelease()), 5));
            user.Rentals.Add(new Rental(new Movie("Gravity", new CalculatorForMovieRegular()), 2));
            user.CurrentStatementGenerator.FormDataForStatement(user.Name, user.Rentals);
            var path = "userSerialize123.txt";
            user.CurrentStatementGenerator.Generate(path);


            var JsonSerializer = new DataContractJsonSerializer(typeof(DataForStatement));
            DataForStatement deserializeData;
            using (FileStream input = File.OpenRead(path))
            {
                deserializeData = JsonSerializer.ReadObject(input) as DataForStatement;
            }
            Assert.AreEqual<DataForStatement>(user.CurrentStatementGenerator.CurrentData, deserializeData);
        }
    }
}
