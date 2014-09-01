using System;
using System.Collections.Generic;
using System.IO;
using Shared;
using TemplateFillerLibrary;
using Xunit;

namespace Hospital.Tests
{
    public class TemplateFillerTests
    {
        [Fact]
        public void FillTemplate_HtmlTemplate_ShouldPass()
        {
            var template = new Template(new List<string> {"hemoglobin", "erythrocytes"}, "BloodTest");
            var person = new Person("Igor", "Shein", new DateTime(1994, 4, 20), "Yaroslavl", "123456789");
            var analysis = new Analysis(new List<string> {"20", "50"}, "Blood Test", new DateTime(2014, 08, 3));

            var templateFiller = new TemplateFiller("Blood Test.html");
            string actual = templateFiller.FillTemplate(person, analysis, template);
            //File.WriteAllText("expected.html", actual);
            string expected = File.ReadAllText("expected.html");
            Assert.Equal(expected, actual);
        }
    }
}