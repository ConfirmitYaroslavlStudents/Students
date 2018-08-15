using System;
using System.Collections.Generic;
using System.Linq;
using Football_League;

namespace Football_League
{
    public enum LeagueType
    {
        SingleElumination = 1,
        DoubleElumination = 2
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
                        SaverLoader.SaveCurrentSession(_leagueType,Grid);
                        break;
                    }
                case MenuChoiceType.ChooseWinners:
                    {
                        List<int>[] choices = UserWinnerChoices();
                        Grid.PlayRound(choices);
                        if (Grid.IsFinished)
                            ConsoleWorker.OnePlayerLeft();
                        SaverLoader.SaveCurrentSession(_leagueType, Grid);
                        break;
                    }
                case MenuChoiceType.DisplayResults:
                    {
                        if (ConsoleWorker.ChooseDrawingType() == 1)
                        {
                            VerticalDrawer verticalGrid = new VerticalDrawer();
                            verticalGrid.MakeVerticalGrid(Grid);
                            ConsoleWorker.PrintVerticalGrid(verticalGrid.VerticalGrid);
                        }
                        else
                        {
                            HorizontalDrawer horizontalDrawer = new HorizontalDrawer();
                            horizontalDrawer.MakeHorintalGrid(Grid);
                            ConsoleWorker.PrintHorizontalGrid(horizontalDrawer.HorizontalGrid);
                        }
                        break;
                    }
                case MenuChoiceType.Load:
                    {
                        Grid = SaverLoader.LoadLastSave(_leagueType);
                        break;
                    }
                case MenuChoiceType.Exit:
                    {
                        bool saveAsked = ConsoleWorker.SaveProcessQuestion() == 1;
                        if (saveAsked)
                            SaverLoader.SaveCurrentSession(_leagueType, Grid);
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

        private static List<int>[] UserWinnerChoices()
        {
            List<int>[] choices = new List<int>[Grid.Grid.Count];
            for (int i = 0; i < Grid.Grid.Count; i++)
            {
                choices[i] = new List<int>();
                var currentMatch = Grid.Grid[i].CurrentRoundFirstMatch;
                while (currentMatch?.PlayerOne != null)
                {
                    choices[i].Add(ConsoleWorker.ChooseMatchWinner(currentMatch));
                    currentMatch = currentMatch.NextMatch;
                }
            }

            return choices;
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
            var playersCountAtStart = players.Count;

            for (
                int i = playersCountAtStart; i < num; i++)
            {
                players.Add(new Contestant(ConsoleWorker.GetPlayerName()));
                SaverLoader.SaveCurrentInputPlayers(players);
            }

            Randomize(ref players);
            return players;
        }
        public static void Randomize(ref List<Contestant> players)
        {
            Random randomGenerator = new Random();
            int n = players.Count();
            while (n > 1)
            {
                n--;
                int k = randomGenerator.Next(n + 1);
                Contestant player = players[k];
                players[k] = players[n];
                players[n] = player;
            }
        }
       
    }
}
