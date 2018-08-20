using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tournament
{
    public static class BinarySaver
    {
        public const string NameOfSinlgeEliminationFile = "SingleElimination";
        public const string NameOfDoubleEliminationFile = "DoubleElimination";

        public static void SaveDoubleToBinnary(DoubleEliminationTournament serializableObjects)
        {
            using (FileStream fs = File.Create(NameOfDoubleEliminationFile))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, serializableObjects);
            }

            if (File.Exists(NameOfSinlgeEliminationFile))
                File.Delete(NameOfSinlgeEliminationFile);
        }

        public static DoubleEliminationTournament LoadDoubleFromBinnary()
        {
            if (File.Exists(NameOfDoubleEliminationFile))
            {
                using (FileStream fs = File.Open(NameOfDoubleEliminationFile, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (DoubleEliminationTournament)formatter.Deserialize(fs);
                }
            }
            return null;
        }

        public static void SaveSingleToBinnary(SingleEliminationTournament serializableObjects)
        {
            using (FileStream fs = File.Create(NameOfSinlgeEliminationFile))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, serializableObjects);
            }

            if (File.Exists(NameOfDoubleEliminationFile))
                File.Delete(NameOfDoubleEliminationFile);
        }

        public static SingleEliminationTournament LoadSingleFromBinnary()
        {
            if (File.Exists(NameOfSinlgeEliminationFile))
            {
                using (FileStream fs = File.Open(NameOfSinlgeEliminationFile, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (SingleEliminationTournament)formatter.Deserialize(fs);
                }
            }
            return null;
        }

    }
}
