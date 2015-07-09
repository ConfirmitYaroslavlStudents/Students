using System.IO;
using System.Runtime.Serialization.Json;

namespace FilmService.KindsOfStatements
{
    public class StatementInJSON : Statement
    {
        public StatementInJSON()
        {
            postfix = ".JSON";
        }
        public override void Serialize(string path, DataStore currentData)
        {
            var streamForJSON = new MemoryStream();
            var jsonSerializer = new DataContractJsonSerializer(typeof(DataStore));
            jsonSerializer.WriteObject(streamForJSON, currentData);
            streamForJSON.Position = 0;
            var serializeStramReader = new StreamReader(streamForJSON);
            using (var output = new StreamWriter(path + postfix, false))
            {
                output.Write(serializeStramReader.ReadToEnd());
            }
            serializeStramReader.Close();
        }

        public override DataStore Deserialize(string path)
        {
            var jsonDeserializer = new DataContractJsonSerializer(typeof(DataStore));
            using (var input = File.OpenRead(path + postfix))
            {
                return jsonDeserializer.ReadObject(input) as DataStore;
            }
        }
    }
}
