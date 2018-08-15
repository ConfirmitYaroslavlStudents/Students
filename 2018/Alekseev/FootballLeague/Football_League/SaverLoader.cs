using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Football_League
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
                ConsoleWorker.Saved();
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
                    ConsoleWorker.Loaded();
                    return players;
                }
            }
            catch (FileNotFoundException)
            {
                ConsoleWorker.FileNotFoundError();
            }
            return null;
        }
        public static void SaveCurrentSession(LeagueType leagueType, FullGrid grid)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            var fileToOpen = leagueType == LeagueType.SingleElumination ? "gridSingleElumination.dat" : "gridDoubleElumination.dat";

            using (FileStream fs = new FileStream(fileToOpen, FileMode.Create))
            {
                formatter.Serialize(fs, grid);
                ConsoleWorker.Saved();
            }
        }

        public static FullGrid LoadLastSave(LeagueType leagueType)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            var fileToOpen = leagueType == LeagueType.SingleElumination ? "gridSingleElumination.dat" : "gridDoubleElumination.dat";

            try
            {
                using (FileStream fs = new FileStream(fileToOpen, FileMode.Open))
                {
                    FullGrid grid = (FullGrid)formatter.Deserialize(fs);
                    ConsoleWorker.Loaded();
                    return grid;
                }
            }
            catch (FileNotFoundException)
            {
                ConsoleWorker.FileNotFoundError();
            }
            return null;
        }
    }
}
