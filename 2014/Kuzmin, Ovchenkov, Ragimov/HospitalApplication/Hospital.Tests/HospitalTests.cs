using System;
using System.IO;
using System.Linq;
using HospitalLib.Data;
using HospitalLib.Providers;
using HospitalLib.Utils;
using Xunit;
using Xunit.Extensions;

namespace Hospital.Tests
{
    public class HospitalTests
    {
        static readonly DatabaseProvider DatabaseProvider = new DatabaseProvider();
        readonly PersonProvider _personProvider = new PersonProvider(DatabaseProvider);
        readonly TemplateProvider _templateProvider = new TemplateProvider(DatabaseProvider);
        readonly AnalysisProvider _analysisProvider = new AnalysisProvider(DatabaseProvider);

        private bool PersonsAreEqual(Person first, Person second)
        {
            return
                first.BirthDate.Equals(second.BirthDate) & 
                first.FirstName.Equals(second.FirstName) & 
                first.MiddleName.Equals(second.MiddleName);
        }

        private bool TemplatesAreEqual(Template first, Template second)
        {
            return first.Name.Equals(second.Name) &&
                   first.HtmlTemplate.Equals(second.HtmlTemplate);

        }

        private bool AnalyzesAreEqual(Analysis first, Analysis second)
        {
            bool result = true;
            result &= TemplatesAreEqual(first.Template, second.Template);
            result &= PersonsAreEqual(first.Person, second.Person);
            result &= (first.CreationTime.CompareTo(second.CreationTime) == 1);

            return result;
        }

        private string GetTemplate(string path)
        {
            string result;
            path = Environment.CurrentDirectory + @"\Templates\" + path;
            using (var streamReader = new StreamReader(path))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }

        [Theory,
        InlineData("Ваня", "Сидоров", "Нестеренкович", "1994.4.20"),
        InlineData("Петя", "Иванов", "Конфермитович", "1994.4.20"),
        InlineData("Дима", "Сидоров", "Магаз", "1994.4.20"),
        InlineData("Сашка", "Серый", "ХозяинКругович", "1994.4.20"),
        InlineData("Катя", "Клеп", "Клопович", "1994.4.20")]
        public void PersonProvider_SaveAndSearch_ShouldPass(string firstName, string lastName, string middleName, string birthDate)
        {
            var person = new Person(firstName, lastName, middleName, DateTime.Parse(birthDate));
            _personProvider.Save(ref person);
            var loadedPerson = _personProvider.Search(firstName, lastName);
            _personProvider.Remove(person);

            Assert.True(PersonsAreEqual(person, loadedPerson[0]));
        }

        [Theory,
         InlineData("Ваня", "Сидоров", "Нестеренкович", "1994.4.20"),
         InlineData("Петя", "Иванов", "Конфермитович", "1994.4.20"),
         InlineData("Дима", "Сидоров", "Магаз", "1994.4.20"),
         InlineData("Сашка", "Серый", "ХозяинКругович", "1994.4.20"),
         InlineData("Катя", "Клеп", "Клопович", "1994.4.20")]
        public void Analysis_ToAndFromJson_AnalysisIsProperlyDeserialized(string firstName, string lastName,
            string middleName, string birthDate)
        {
            var template = new Template("имя шаблона", "содержимое");
            var person = new Person(firstName, lastName, middleName, DateTime.Parse(birthDate));
            var jsonFormatter = new JsonFormatter();
            var expectedAnalysis = new Analysis(template, person);
            var actualAnalysis = jsonFormatter.FromJson<Analysis>(jsonFormatter.ToJson(expectedAnalysis));

            Assert.True(AnalyzesAreEqual(expectedAnalysis, actualAnalysis));
        }

        [Theory,
         InlineData("Группа крови.html", "Группа крови.txt"),
         InlineData("Общий анализ крови.html", "Общий анализ крови.txt"),
         InlineData("Сахар.html", "Сахар.txt")]
        public void HtmlToTextParser_HtmlTemplateIsProperlyParsed_ShouldPass(string pathToHtmlTemplate, string pathToTxtTemplate)
        {
            var htmlParser = new HtmlToTextParser();
            var htmlTemplate = GetTemplate(pathToHtmlTemplate);
            var actualTextTemplate = htmlParser.Parse(htmlTemplate).Replace("\r\n", "").Replace("\n", "");
            var expectedTextTemplate = GetTemplate(pathToTxtTemplate).Replace("\r\n", "").Replace("\n", "");

            Assert.Equal(expectedTextTemplate, actualTextTemplate);
        }

        [Theory,
           InlineData("Ваня", "Сидоров", "Нестеренкович", "1994.4.20"),
           InlineData("Петя", "Иванов", "Конфермитович", "1994.4.20"),
           InlineData("Дима", "Сидоров", "Магаз", "1994.4.20"),
           InlineData("Сашка", "Серый", "ХозяинКругович", "1994.4.20"),
           InlineData("Катя", "Клеп", "Клопович", "1994.4.20")]
        public void AnalysisProvider_PersonAnalyzesArePropelyLoaded_ShouldPass(string firstName, string lastName, string middleName, string birthDate)
        {
            var person = new Person(firstName, lastName, middleName, DateTime.Parse(birthDate));
            _personProvider.Save(ref person);

            var template = new Template("name" + firstName, "content");
            _templateProvider.Save(template);

            var expectedAnalysis = new Analysis(template, person);
            _analysisProvider.Save(ref expectedAnalysis);

            var analyzes = _analysisProvider.Load(person);

            _analysisProvider.Remove(expectedAnalysis);
            _personProvider.Remove(person);
            _templateProvider.Remove(template);

            var actualAnalysis = analyzes[0];

            Assert.True(analyzes.Count() == 1);
            Assert.True(AnalyzesAreEqual(expectedAnalysis, actualAnalysis));
        }

        [Theory,
           InlineData("Ваня", "Сидоров"),
           InlineData("Петя", "Конфермитович"),
           InlineData("Дима", "Сидоров"),
           InlineData("Сашка", "Серый"),
           InlineData("Катя", "Клеп")]
        public void TemplateProvider_PersonAnalyzesArePropelyLoaded_ShouldPass(string name, string content)
        {
            var expectedTemplate = new Template(name, content);
            _templateProvider.Save(expectedTemplate);
            var templates = _templateProvider.Load();
            _templateProvider.Remove(expectedTemplate);

            var actualTemplate = templates[0];

            Assert.True(TemplatesAreEqual(expectedTemplate, actualTemplate));
        }
    }
}
