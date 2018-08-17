using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ChampionshipLibrary
{
    public static class SaveLoadSystem
    {
        private static BinaryFormatter _formatter = new BinaryFormatter();
        private static string _fileName = "Championship.dat";

        public static void Save(IChampionship championship)
        {
            using (FileStream fs = new FileStream(_fileName, FileMode.OpenOrCreate))
            {
                _formatter.Serialize(fs, championship);
            }
        }

        public static IChampionship Load()
        {
            IChampionship championship;

            using (FileStream fs = new FileStream(_fileName, FileMode.OpenOrCreate))
            {
                try
                {
                    championship = (IChampionship)_formatter.Deserialize(fs);
                }
                catch
                {
                    throw new Exception();
                }
            }
            return championship;
        }
    }
}
