using System;
using System.Collections.Generic;
using DataStorageLibrary;
using Shared;
using Xunit;
using Xunit.Extensions;

namespace Hospital.Tests
{
    public class DataStorageTests
    {
        public static IEnumerable<object[]> TestPersonsData
        {
            get
            {
                yield return new object[] { new Person("Igor", "Shein", new DateTime(1994, 4, 20), "Yaroslavl", "123456789")};
                yield return new object[] { new Person("Vasya", "Pupkin", new DateTime(1990, 4, 21), "Yaroslavl", "723456780")};
                yield return new object[] { new Person("Sergey", "Bykov", new DateTime(1956, 2, 2), "Moscow", "723499780")};
                yield return new object[] { new Person("Sergey", "Bykov", new DateTime(1956, 2, 2), "Moscow", "723499780") };
            }
        }

        public static IEnumerable<object[]> TestTemplatesData
        {
            get
            {
                yield return new object[] {new Template(new Dictionary<string, string> { {"hemoglobin", string.Empty}, {"erythrocytes", string.Empty}}, "Blood Test") };
                yield return new object[] { new Template(new Dictionary<string, string> { { "alcohol", string.Empty }, { "drugs", string.Empty } }, "Alcho Test") };
            }
        }

        public static IEnumerable<object[]> TestAnalyzesData
        {
            get
            {
                yield return new object[] {"123456789", new Analysis(new Template(new Dictionary<string, string> { { "hemoglobin", "20" }, { "erythrocytes", "40" } }, "Blood Test"), new DateTime(2014, 08, 3)) };
                yield return new object[] {"123488889", new Analysis(new Template(new Dictionary<string, string> { { "alcohol", "5" }, { "drugs", "10" } }, "Alcho Test"), new DateTime(2014, 08, 10)) };
            }
        }

        [Theory]
        [PropertyData("TestPersonsData")]
        public void AddandGetPerson_ThreeDifferentPersons_ShouldPass(Person person)
        {
            var dataStorage = new DataStorage();
            dataStorage.AddPerson(person);
            int personsCount = dataStorage.GetPersons("PolicyNumber", person.PolicyNumber).Count;
            Assert.True(personsCount >= 1);
        }

        [Theory]
        [PropertyData("TestTemplatesData")]
        public void AddandGetTemplate_TwoTemplates_ShouldPass(Template template)
        {
            var dataStorage = new DataStorage();
            dataStorage.AddTemplate(template);
            var expected = dataStorage.GetTemplate(template.Title);
            Assert.NotEqual(expected, null);
        }

        [Fact]
        public void GetTemplates_DBWithValues_ShouldPass()
        {
            var dataStorage = new DataStorage();
            dataStorage.AddTemplate(
                new Template(new Dictionary<string, string> {{"alcohol", string.Empty}, {"drugs", string.Empty}},
                    "Alcho2 Test"));
            int templatesCount = dataStorage.GetTemplates().Count;
            Assert.True(templatesCount >= 1);
        }

        [Theory]
        [PropertyData("TestAnalyzesData")]
        public void AddandGetAnalyzes_TwoAnalyzes_ShouldPass(string policyNumber, Analysis analysis)
        {
            var dataStorage = new DataStorage();
            dataStorage.AddAnalysis(policyNumber, analysis);
            int analyzesCount = dataStorage.GetAnalyzes(policyNumber).Count;
            Assert.True(analyzesCount >= 1);
        }

    }
}
