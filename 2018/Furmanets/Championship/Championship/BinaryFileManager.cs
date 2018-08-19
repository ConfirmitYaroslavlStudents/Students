using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Championship
{
    public class BinaryFileManager : IFileManager
    {
        private const string _nameFile = "tournament";

        public void WriteToFile(Tournament tournament)
        {
            var binaryFormatter = new BinaryFormatter();
            using (var file = new FileStream(_nameFile, FileMode.Create))
            {
                binaryFormatter.Serialize(file, tournament);
            }
        }

        public Tournament ReadFromFile()
        {
            if (!File.Exists(_nameFile))
            {
                return null;
            }
            Tournament tournament;

            var binaryFormatter = new BinaryFormatter();
            using (var file = new FileStream(_nameFile, FileMode.Open))
            {
                tournament = (Tournament)binaryFormatter.Deserialize(file);
            }
            return tournament;
        }

        public void DeleteFile()
        {
            if (File.Exists(_nameFile))
            {
                File.Delete(_nameFile);
            }
        }
    }
}
