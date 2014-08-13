using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace RefactoringSample
{
    public class StringStatementGenerator
    {
        private string _phraseRental = "Учет аренды для {0}{1}";
        private string _phraseMovie = "\t {0} \t {1} {2}";
        private string _phraseTotal = "Сумма задолженности составляет {0}{1}";
        private string _phrasePoints = "Вы заработали {0} за активность";

        private void LoadFromXml()
        {
            var document = new XmlDocument();
            document.LoadXml(Properties.Resources.phrases);

            if (document.DocumentElement == null) return;

            var rental = document.DocumentElement.SelectSingleNode("Rental");
            if (rental != null) _phraseRental = rental.InnerText;
            var movie = document.DocumentElement.SelectSingleNode("Movie");
            if (movie != null) _phraseMovie = movie.InnerText.Replace("\\t","\t");
            var total = document.DocumentElement.SelectSingleNode("Total");
            if (total != null) _phraseTotal = total.InnerText;
            var points = document.DocumentElement.SelectSingleNode("Points");
            if (points != null) _phrasePoints = points.InnerText;
        }

        public string Create(Statement statement)
        {
            LoadFromXml();

            var result = new StringBuilder();
            result.Append(String.Format(_phraseRental, statement.Name, Environment.NewLine));

            foreach (var movie in statement.MoviePrices.Keys)
            {
                result.Append(String.Format(_phraseMovie, movie, statement.MoviePrices[movie].ToString(CultureInfo.InvariantCulture), Environment.NewLine));
            }

            result.Append(String.Format(_phraseTotal, statement.TotalAmount.ToString(CultureInfo.InvariantCulture), Environment.NewLine));
            result.Append(String.Format(_phrasePoints, statement.FrequentRenterPoints.ToString(CultureInfo.InvariantCulture)));
            return result.ToString();
        }
    }

    public class JsonStatementGenerator
    {
        public MemoryStream Create(Statement statement)
        {
            var stream = new MemoryStream();
            var ser = new DataContractJsonSerializer(typeof(Statement));
            ser.WriteObject(stream, statement);

            return stream;
        }
    }

    public class CompositeStatementGenerator
    {
        public CompositeStatement Create(Statement statement)
        {
            return new CompositeStatement(statement);
        }
    }
}
