using System;
using System.Collections.Generic;
using System.Configuration;
using HospitalConnectedLayer;
using Shared;
using Xunit;
using Xunit.Extensions;

namespace Hospital.Tests
{
    public class HospitalDALTests
    {
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        private readonly string _dataProvider = ConfigurationManager.AppSettings["provider"];

        public static IEnumerable<object[]> TestPersonsData
        {
            get
            {
                yield return
                    new object[] { new Person("Igor", "Shein", new DateTime(1994, 4, 20), "Yaroslavl", "123456789") };
                yield return
                    new object[] { new Person("Vasya", "Pupkin", new DateTime(1990, 4, 21), "Yaroslavl", "723456780") };
                yield return
                    new object[] { new Person("Sergey", "Bykov", new DateTime(1956, 2, 2), "Moscow", "723499780") };
            }
        }

        public static IEnumerable<object[]> TestTemplatesData
        {
            get
            {
                yield return new object[] { new Template(new List<string> { "hemoglobin", "erythrocytes" }, "Blood Test") };
                yield return new object[] { new Template(new List<string> { "alcohol", "drugs" }, "Alcho Test") };
            }
        }

        public static IEnumerable<object[]> TestAnalyzesData
        {
            get
            {
                yield return
                    new object[] { "123456789", new Analysis(new List<string> { "20", "50" }, "Blood Test", new DateTime(2014, 08, 3)) };
                yield return
                    new object[] { "123488889", new Analysis(new List<string> { "20", "40" }, "Alcho Test", new DateTime(2014, 08, 10)) }
                    ;
            }
        }

        [Theory]
        [PropertyData("TestPersonsData")]
        public void AddandGetPerson_ThreeDifferentPersons_ShouldPass(Person person)
        {
            var dataStorage = new HospitalDAL(_dataProvider, _connectionString);
            dataStorage.AddPerson(person);
            int personsCount = dataStorage.GetPersons(person.FirstName, person.LastName, person.PolicyNumber).Count;
            Assert.True(personsCount >= 1);
        }

        [Theory]
        [PropertyData("TestTemplatesData")]
        public void AddandGetTemplate_TwoTemplates_ShouldPass(Template template)
        {
            var dataStorage = new HospitalDAL(_dataProvider, _connectionString);
            dataStorage.AddTemplate(template);
            Template expected = dataStorage.GetTemplate(template.Title);
            Assert.NotEqual(expected, null);
        }

        [Fact]
        public void GetTemplates_DBWithValues_ShouldPass()
        {
            var dataStorage = new HospitalDAL(_dataProvider, _connectionString);
            dataStorage.AddTemplate(new Template(new List<string> { "alcohol", "drugs" }, "Alcho2 Test"));
            int templatesCount = dataStorage.GetTemplates().Count;
            Assert.True(templatesCount >= 1);
        }

        [Theory]
        [PropertyData("TestAnalyzesData")]
        public void AddandGetAnalyzes_TwoAnalyzes_ShouldPass(string policyNumber, Analysis analysis)
        {
            var dataStorage = new HospitalDAL(_dataProvider, _connectionString);
            dataStorage.AddAnalysis(policyNumber, analysis);
            int analyzesCount = dataStorage.GetAnalyzes(policyNumber).Count;
            Assert.True(analyzesCount >= 1);
        }
    }
}