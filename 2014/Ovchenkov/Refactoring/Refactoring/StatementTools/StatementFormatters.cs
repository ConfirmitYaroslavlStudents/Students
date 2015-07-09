using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml;

namespace Refactoring.StatementTools
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

        public void GetStatement(Action<object> delegateWriter, Statement statement)
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
            delegateWriter(statementTemplate.InnerText);
        }
    }

    public class JsonStatement : IStatementFormater
    {
        public void GetStatement(Action<object> delegateWriter, Statement statement)
        {
            using (var stream = new MemoryStream())
            {
                var ser = new DataContractJsonSerializer(typeof(Statement));
                ser.WriteObject(stream, statement);
                stream.Position = 0;
                var sr = new StreamReader(stream);
                delegateWriter(sr.ReadToEnd());
            }
        }
    }

    public class UniversalStatement : IStatementFormater
    {
        private readonly IDictionary<IStatementFormater, Action<object>> _statemnetFormatters = new Dictionary<IStatementFormater, Action<object>>();

        public void AddFormatter(IStatementFormater formatter, Action<object> write)
        {
            _statemnetFormatters[formatter] = write;
        }

        public void GetStatement(Action<object> write, Statement statement)
        {
            foreach (var keyValuePair in _statemnetFormatters)
            {
                var delegateWriter = keyValuePair.Value;
                var formater = keyValuePair.Key;
                formater.GetStatement(delegateWriter, statement);
            }
        }
    }
}
    

