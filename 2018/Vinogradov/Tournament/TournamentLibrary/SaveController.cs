using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TournamentLibrary
{
    public static class SaveController
    {
        private const string _saveName = "save.dat";

        public static void Save(Tournament tournament)
        {
            BinaryFormatter binary = new BinaryFormatter();

            using (var fs = new FileStream(_saveName, FileMode.OpenOrCreate))
            {
                binary.Serialize(fs, tournament);
            }
        }

        public static Tournament Load()
        {
            BinaryFormatter binary = new BinaryFormatter();

            using (var fs = new FileStream(_saveName, FileMode.OpenOrCreate))
            {
                try
                {
                    Tournament tournament = (Tournament)binary.Deserialize(fs);
                    return tournament;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
