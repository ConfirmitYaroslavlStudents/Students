using System;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using HospitalLib.Database;
using HospitalLib.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hospital.Tests
{
    [TestClass]
    public class HospitalTests
    {
        [TestMethod]
        public void ParserLoad_TemplateIsProperlyParsed()
        {
            var parser = new Parser();

            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("Debug","");
            var testFile = Path.Combine(baseDir.Replace("bin",""), "TestFiles", "Template.txt");

            var template = parser.Load(testFile);
            var actualContentOfTemplate = String.Empty;
            foreach (var line in template.Lines)
            {
                foreach (var element in line)
                {
                    actualContentOfTemplate += element.Text + "|";
                }
            }

            const string expectedContentOfTemplate = "Группа крови|На рукаве|Порядковый номер|На рукаве|";
            Assert.AreEqual(expectedContentOfTemplate, actualContentOfTemplate);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ParserLoad_UncorrectPath_ExceptionThrown()
        {
            var parser = new Parser();
            parser.Load(@"FooBar");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ParserLoad_UnsupportedFormatOfFile_ExceptionThrown()
        {
            var parser = new Parser();
            parser.Load(@"FooBar.pdf");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ParserLoad_UnclosedTextBoxTag_ExceptionThrown()
        {
            var parser = new Parser();
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("Debug", "");
            var testFile = Path.Combine(baseDir.Replace("bin", ""), "TestFiles", "TemplateWithUnclosedTextBoxTag.txt");

            parser.Load(testFile);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ParserLoad_UnclosedLabelTag_ExceptionThrown()
        {
            var parser = new Parser();
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("Debug", "");
            var testFile = Path.Combine(baseDir.Replace("bin", ""), "TestFiles", "TemplateWithUnclosedLabelTag.txt");
            parser.Load(testFile);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ParserLoad_TemplateWithoutSemicolonAfterProperty_ExceptionThrown()
        {
            var parser = new Parser();
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("Debug", "");
            var testFile = Path.Combine(baseDir.Replace("bin", ""), "TestFiles", "TemplateWithoutSemicolonAfterProperty.txt");
            parser.Load(testFile);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ParserLoad_TemplateWithInvalidElement_ExceptionThrown()
        {
            var parser = new Parser();
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("Debug", "");
            var testFile = Path.Combine(baseDir.Replace("bin", ""), "TestFiles", "TemplateWithInvalidElement.txt");
            parser.Load(testFile);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void StorageSave_DatabasePathIsNull_ExceptionThrown()
        {
            var storage = new Storage();
            storage.DatabasePath = null;
            storage.Save(new HospitalLib.Template.Template(null,""), new Person());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void StorageSave_DatabasePathIsEmptyString_ExceptionThrown()
        {
            var storage = new Storage();
            storage.DatabasePath = "";
            storage.Save(new HospitalLib.Template.Template(null,""), new Person());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void PersonProviderSave_DatabasePathIsNull_ExceptionThrown()
        {
            var personProvider = new PersonProvider();
            personProvider.DatabasePath = null;
            personProvider.Save(new Person());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void PersonProviderSave_DatabasePathIsEmptyString_ExceptionThrown()
        {
            var personProvider = new PersonProvider();
            personProvider.DatabasePath = "";
            personProvider.Save(new Person());
        }
    }
}
