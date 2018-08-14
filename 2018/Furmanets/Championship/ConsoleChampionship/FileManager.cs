using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Championship;

namespace ConsoleChampionship
{
    internal class FileManager
    {
        private const string _nameFile = "tournament";

        public static void WriteTournamentInFile(Tournament tournament)
        {
            var binaryFormatter = new BinaryFormatter();
            using (var file = new FileStream(_nameFile, FileMode.Create))
            {
                binaryFormatter.Serialize(file, tournament);
            }
        }

        public static Tournament DownloadTournamentFromFile()
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

        public static void RemoveFile()
        {
            if (File.Exists(_nameFile))
            {
                File.Delete(_nameFile);
            }
        }
    }
}
