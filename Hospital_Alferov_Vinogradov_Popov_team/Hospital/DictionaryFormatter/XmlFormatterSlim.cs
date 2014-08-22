using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DictionaryFormatter
{
    public class XmlFormatterSlim : IDictionaryFormatter<string, string>
    {
        public string Serialize(Dictionary<string, string> data)
        {
            var xdoc = new XDocument
            (
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("fields", from item in data select new XElement(item.Key, item.Value))
            );

            return xdoc.Declaration.ToString() + xdoc;
        }

        public Dictionary<string, string> Deserialize(string serializedData)
        {
            var xdoc = XDocument.Load(new StringReader(serializedData));
            var rootNodes = xdoc.Root.DescendantNodes().OfType<XElement>();
            var result = rootNodes.ToDictionary(v => v.Name.ToString(), v => v.Value);
            return result;
        }
    }
}
