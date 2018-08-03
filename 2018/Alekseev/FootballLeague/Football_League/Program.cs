using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

namespace Football_League
{
    internal enum LeagueType
    {
        SingleElumination = 1,
        DoubleElumination = 2
    };

    internal enum GridDrawingType
    {
        Vertical = 1,
        Horizontal = 2
    };
    internal enum MenuChoiceType
    {
        StartNewLeague = 1,
        ChooseWinners = 2,
        DisplayResults = 3,
        Load = 4,
        Exit = 5
    }
    internal class Program
    {
        private static LeagueType _leagueType;
        private static FullGrid Grid = new FullGrid();

        private static void Main()
        {
            _leagueType = ConsoleWorker.ChooseLeagueType() == 1 ? LeagueType.SingleElumination : LeagueType.DoubleElumination;

            while (true)
            {
                ConsoleWorker.Menu();
                bool successfulOperation = false;
                while (!successfulOperation)
                    successfulOperation = RunChoice((MenuChoiceType)int.Parse(ConsoleWorker.MenuChoice())) != 0;
            }
        }
        public static int RunChoice(MenuChoiceType choice)
        {
            switch (choice)
            {
                case MenuChoiceType.StartNewLeague:
                {
                        CreateNewLeague();
                        SaveCurrentSession();
                        break;
                    }
                case MenuChoiceType.ChooseWinners:
                    {
                        Grid.PlayRound();
                        SaveCurrentSession();
                        break;
                    }
                case MenuChoiceType.DisplayResults:
                    {
                        if (ConsoleWorker.ChooseDrawingType() == 1)
                        {
                            VerticalDrawer verticalGrid = new VerticalDrawer();
                            verticalGrid.MakeVerticalGrid(Grid);
                            verticalGrid.PrintGrid();
                        }
                        else
                        {
                            HorizontalDrawer horizontalDrawer = new HorizontalDrawer();
                            horizontalDrawer.MakeHorintalGrid(Grid);
                            horizontalDrawer.PrintGrid();
                        }
                        break;
                    }
                case MenuChoiceType.Load:
                    {
                        LoadLastSave();
                        break;
                    }
                case MenuChoiceType.Exit:
                    {
                        bool saveAsked = ConsoleWorker.SaveProcessQuestion() == 1;
                        if (saveAsked)
                            SaveCurrentSession();
                        Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        ConsoleWorker.IncorrectMenuChoice();
                        ConsoleWorker.Menu();
                        return 0;
                    }
            }
            return 1;
        }

        public static void CreateNewLeague()
        {
            Grid = new FullGrid();
            Grid.SetGridTreesNumber((int)_leagueType);
            Grid.InitialiseGrid(AddPlayersToCompetition(ConsoleWorker.GetNumberOfPlayers()));
        }

        public static List<Contestant> AddPlayersToCompetition(int num)
        {
            var players = new List<Contestant>();
            for (int i = 0; i < num; i++)
                players.Add(new Contestant(ConsoleWorker.GetPlayerName()));

            Randomize(ref players);
            return players;
        }
        public static void Randomize(ref List<Contestant> players)
        {
            Random rng = new Random();
            int n = players.Count();
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Contestant player = players[k];
                players[k] = players[n];
                players[n] = player;
            }
        }
        public static void SaveCurrentSession()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(FullGrid));

            var fileToOpen = _leagueType == LeagueType.SingleElumination ? "gridSingleElumination.xml" : "gridDoubleElumination.xml";

            using (FileStream fs = new FileStream(fileToOpen, FileMode.Create))
            {
                formatter.Serialize(fs,Grid);
                ConsoleWorker.Saved();
            }
        }

        public static void LoadLastSave()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(FullGrid));

            var fileToOpen = _leagueType == LeagueType.SingleElumination ? "gridSingleElumination.xml" : "gridDoubleElumination.xml";

            try
            {
                using (FileStream fs = new FileStream(fileToOpen, FileMode.Open))
                {
                    Grid = (FullGrid)formatter.Deserialize(fs);
                    ConsoleWorker.Loaded();
                }
            }
            catch (FileNotFoundException)
            {
                ConsoleWorker.FileNotFoundError();
            }          
        }
    }
}
