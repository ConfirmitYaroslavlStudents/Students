using System.IO;
using System.Runtime.Serialization.Json;

namespace FilmService.KindsOfGenerators
{
    public class StatementGeneratorJSON : StatementGenerator
    {
        public override void Generate(string path)
        {
            var streamForJSON = new MemoryStream();
            var JsonSerializer = new DataContractJsonSerializer(typeof(DataForStatement));
            JsonSerializer.WriteObject(streamForJSON, CurrentData);
            streamForJSON.Position = 0;
            var SerializeStramReader = new StreamReader(streamForJSON);
            using (var output = new StreamWriter(path, false))
            {
                output.Write(SerializeStramReader.ReadToEnd());
            }
            SerializeStramReader.Close();
        }
    }
}
