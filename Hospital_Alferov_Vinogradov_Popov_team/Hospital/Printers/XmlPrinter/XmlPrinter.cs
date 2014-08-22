using System.Linq;
using System.Xml.Linq;
using Shared;
using Shared.Interfaces;

namespace XmlPrinter
{
    public class XmlPrinter : IPrinter
    {
        public void Print(Person person, Analysis analysis, string pathToFile)
        {
            var xdoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("person",
                    new XElement("FirstName", person.FirstName),
                    new XElement("LastName", person.LastName),
                    new XElement("DateOfBirth", person.DateOfBirth),
                    new XElement("Address", person.Address),
                    new XElement("Age", person.Age),
                    new XElement("PolicyNumber", person.FirstName),
                    new XElement("analysis",
                    new XElement("Title", analysis.TemplateTitle),
                    new XElement("Date", analysis.Date),
                    from item in analysis.Data select new XElement(item.Key, item.Value))));
            xdoc.Save(pathToFile + ".xml");
        }
    }
}
