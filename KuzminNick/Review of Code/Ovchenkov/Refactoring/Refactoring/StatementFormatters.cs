using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Refactoring
{
    public class StringStatement : IStatementFormater
    {
        public string GetStatement(Statement statement)
        {
            var result = String.Format("Учет аренды для {0}{1}", statement.Name, Environment.NewLine);

            foreach (var movie in statement.Movies)
            {
                result += String.Format("\t {0} \t {1} {2}", movie.Key,
                    movie.Value.ToString(CultureInfo.InvariantCulture), Environment.NewLine);
            }

            result += String.Format("Сумма задолженности составляет {0}{1}",
                statement.TotalAmount.ToString(CultureInfo.InvariantCulture), Environment.NewLine);
            result += String.Format("Вы заработали {0} за активность",
                statement.FrequentRenterPoints.ToString(CultureInfo.InvariantCulture));

            return result;
        }
    }

    public class JsonStatement : IStatementFormater
    {
        public string GetStatement(Statement statement)
        {
            using (var stream = new MemoryStream())
            {
                var ser = new DataContractJsonSerializer(typeof(Statement));
                ser.WriteObject(stream, statement);
                stream.Position = 0;
                var sr = new StreamReader(stream);

                return sr.ReadToEnd();
            }
        }
    }
}
    

