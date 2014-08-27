using System;
using HospitalLib.Data;
using HospitalLib.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hospital.Tests
{
    [TestClass]
    public class HospitalTests
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AnalysisGetData_ConstructorWithNullParameters_ExceptionThrown()
        {
            var analysis = new Analysis(null, null, null);
            analysis.GetData(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AnalysisGetPersonId_ConstructorWithNullParameters_ExceptionThrown()
        {
            var analysis = new Analysis(null, null, null);
            analysis.GetPersonId();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AnalysisGetTemplateId_ConstructorWithNullParameters_ExceptionThrown()
        {
            var analysis = new Analysis(null, null, null);
            analysis.GetTemplateId();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AnalysisToJson_ConstructorWithNullParameters_ExceptionThrown()
        {
            var analysis = new Analysis(null, null, null);
            analysis.ToJson();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PersonBirthDateProperty_ConstructorWithNullParameters_ExceptionThrown()
        {
            var person = new Person(new NewIdProvider(new DatabaseProvider()), "test", "test", "test", new DateTime());
            const string uncorrectDate = "01/08/3000";
            person.BirthDate = Convert.ToDateTime(uncorrectDate);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AnalysisProvider_ConstructorWithNullParameter_ExceptionThrown()
        {
            new AnalysisProvider(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AnalysisProviderGetCount_ConstructorWithNullParameter_ExceptionThrown()
        {
            var analysisProvider = new AnalysisProvider(null);
            analysisProvider.GetCount();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AnalysisLoad_ConstructorWithNullParameter_ExceptionThrown()
        {
            var analysisProvider = new AnalysisProvider(null);
            analysisProvider.Load(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AnalysisLoad_ConstructorWithUncorrectParameters_ExceptionThrown()
        {
            var analysisProvider = new AnalysisProvider(null);
            var person = new Person(new NewIdProvider(new DatabaseProvider()), "test", "test", "test", new DateTime());
            analysisProvider.Load(person);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AnalysisSave_ConstructorWithUncorrectParameters_ExceptionThrown()
        {
            var analysisProvider = new AnalysisProvider(null);
            analysisProvider.Save(new Analysis(null, null, null));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AnalysisUpdate_ConstructorWithUncorrectParameters_ExceptionThrown()
        {
            var analysisProvider = new AnalysisProvider(null);
            analysisProvider.Update(null);
        }
    }
}
