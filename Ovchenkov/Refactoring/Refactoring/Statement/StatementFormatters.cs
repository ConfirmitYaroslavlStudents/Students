using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml;

namespace Refactoring
{
    public class StringStatement : IStatementFormater
    {
        private void Replace(XmlNode statementTemplate, string tag, string value)
        {
            var xmlNode = statementTemplate.SelectSingleNode(tag);
            if (xmlNode != null)
                xmlNode.InnerText = value;
        }

        private string GetMoviesString(Statement statement)
        {
            string movies = null;
            foreach (var movie in statement.Movies)
            {
                movies += String.Format("\t {0} \t {1} {2}", movie.Key,
                    movie.Value.ToString(CultureInfo.InvariantCulture), Environment.NewLine);
            }
            return movies;
        }

        public StatementData GetStatement(Statement statement)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(Properties.Resources.StatementTemplate);
            var statementTemplate = xmlDocument.SelectSingleNode("statementTemplate");

            if (statementTemplate == null)
            {
                throw new InvalidDataException("statementTemplate");
            }
            Replace(statementTemplate, "CustomerName", statement.Name);
            Replace(statementTemplate, "TotalAmount", statement.TotalAmount.ToString(CultureInfo.InvariantCulture));
            Replace(statementTemplate, "FrequentRenterPoints",
                statement.FrequentRenterPoints.ToString(CultureInfo.InvariantCulture));
            Replace(statementTemplate, "Movies", GetMoviesString(statement));

            return new StatementData { StringData = statementTemplate.InnerText };
        }
    }

    public class JsonStatement : IStatementFormater
    {
        public StatementData GetStatement(Statement statement)
        {
            using (var stream = new MemoryStream())
            {
                var ser = new DataContractJsonSerializer(typeof(Statement));
                ser.WriteObject(stream, statement);
                stream.Position = 0;
                var sr = new StreamReader(stream);

                return new StatementData { JsongData = sr.ReadToEnd() }; 
            }
        }
    }

    public class UniversalStatement : IStatementFormater
    {
        public StatementData GetStatement(Statement statement)
        {
            var stringData = new StringStatement().GetStatement(statement);
            var jsonData = new JsonStatement().GetStatement(statement);
            return new StatementData { JsongData = jsonData.JsongData, StringData = stringData.StringData}; 
        }
    }
}
    

