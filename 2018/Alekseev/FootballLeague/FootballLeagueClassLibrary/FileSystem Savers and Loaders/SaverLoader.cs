using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using FootballLeagueClassLibrary.Structure;

namespace FootballLeagueClassLibrary.FileSystem_Savers_and_Loaders
{
    public class SaverLoader
    {
        public static void SaveCurrentInputPlayers(List<Contestant> players)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            var fileToOpen = "InputPlayers.dat";

            using (FileStream fs = new FileStream(fileToOpen, FileMode.Create))
            {
                formatter.Serialize(fs,players);
            }
        }

        public static List<Contestant> LoadCurrentInputPlayers()
        {

            BinaryFormatter formatter = new BinaryFormatter();

            var fileToOpen = "InputPlayers.dat";

            try
            {
                using (FileStream fs = new FileStream(fileToOpen, FileMode.Open))
                {
                    List<Contestant> players = (List<Contestant>)formatter.Deserialize(fs);
                    return players;
                }
            }
            catch (FileNotFoundException) { }
            return null;
        }
        public static void SaveCurrentSession(int leagueType, FullGrid grid)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            var fileToOpen = leagueType == 1 ? "gridSingleElumination.dat" : "gridDoubleElumination.dat";

            using (FileStream fs = new FileStream(fileToOpen, FileMode.Create))
            {
                formatter.Serialize(fs, grid);
            }
        }

        public static FullGrid LoadLastSave(int leagueType)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            var fileToOpen = leagueType == 1 ? "gridSingleElumination.dat" : "gridDoubleElumination.dat";

            try
            {
                using (FileStream fs = new FileStream(fileToOpen, FileMode.Open))
                {
                    FullGrid grid = (FullGrid)formatter.Deserialize(fs);
                    return grid;
                }
            }
            catch (FileNotFoundException) { }
            return null;
        }
    }
}
