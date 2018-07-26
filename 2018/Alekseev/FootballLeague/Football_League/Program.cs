using System;
using System.Collections.Generic;
using System.Linq;

namespace Football_League
{
    internal enum LeagueType
    {
        SingleElumination,
        DoubleElumination
    };
    internal class Program
    {
        private static List<Contestant> _currentPlayersGrid1 = new List<Contestant>();
        private static List<Contestant> _currentPlayersGrid2 = new List<Contestant>();
        private static List<Match> _currentMatchesGrid1;
        private static List<Match> _currentMatchesGrid2;
        private static Scoreboard _scoreboard = new Scoreboard();

        private static LeagueType _leagueType;
        private static void Main()
        {
            _leagueType = ConsoleWorker.ChooseLeagueType() == 1 ? LeagueType.SingleElumination : LeagueType.DoubleElumination;

            while (true)
            {
                ConsoleWorker.Menu();
                bool successfulOperation = false;
                while (!successfulOperation)
                    successfulOperation = RunChoice() != 0;
            }
        }      
        public static int RunChoice()
        {
            var choice = ConsoleWorker.MenuChoice();
            switch (choice)
            {
                case "1":
                    {
                        CreateNewLeague();
                        break;
                    }
                case "2":
                {
                        if(!CheckPlayerNumber())
                            break;
                        ChooseWinners();
                        break;
                    }
                case "3":
                    {
                        _scoreboard.Print();
                        break;
                    }
                case "4":
                    {
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
            _scoreboard = new Scoreboard();
            List<Contestant> newPlayers = new List<Contestant>();

            var count = ConsoleWorker.GetNumberOfPlayers();
            for (int i = 0; i < count; i++)
            {
              
                var newPlayer = new Contestant(ConsoleWorker.GetPlayerName(),0);              
                newPlayers.Add(newPlayer);
            }

            Randomize(ref newPlayers);

            List<Contestant> newPlayersRandomized = new List<Contestant>();
            foreach (var contestant in newPlayers)
            {
                var newPlayer = new Contestant(contestant.Name, _scoreboard.CurrentPlayerPosition);
                _scoreboard.CurrentPlayerPosition += newPlayer.Name.Length + 1;
                newPlayersRandomized.Add(newPlayer);
            }

            EndRound(newPlayersRandomized);            
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

        public static void UpdateCurrentPlayers(List<Contestant> playersGrid1,List<Contestant> playersGrid2 = null)
        {
            _currentPlayersGrid1 = new List<Contestant>();
            _currentPlayersGrid2 = new List<Contestant>();


            foreach (var player in playersGrid1)
            {
                _currentPlayersGrid1.Add(player);
            }
            if (playersGrid2 != null)
            {
                foreach (var player in playersGrid2)
                {
                    _currentPlayersGrid2.Add(player);
                }
            }
        }

        public static void UpdateCurrentMatches()
        {
            _currentMatchesGrid1 = UpdateMatchesGrid(_currentPlayersGrid1);
            _currentMatchesGrid2 = UpdateMatchesGrid(_currentPlayersGrid2);         
        }

        private static List<Match> UpdateMatchesGrid(List<Contestant> currentPlayersGrid)
        {
            List<Match> currentMatchesGrid = new List<Match>();
            int countMatches = currentPlayersGrid.Count / 2;

            for (int i = 0; i < countMatches; i++)
            {
                currentMatchesGrid.Add(new Match(currentPlayersGrid[i * 2], currentPlayersGrid[i * 2 + 1]));
            }
            return currentMatchesGrid;
        }

        public static void ChooseWinners()
        {
            if (_currentPlayersGrid1.Count == _currentPlayersGrid2.Count && _currentPlayersGrid2.Count == 1)
            {
                _currentMatchesGrid1.Add(new Match(_currentPlayersGrid1[0], _currentPlayersGrid2[0]));
                _currentMatchesGrid1 = ChooseWinnerOfMatchesInGrid(_currentMatchesGrid1);

                List<Contestant> winnerOfTwoGrids = new List<Contestant>
                {
                    _currentMatchesGrid1[0].Winner
                };
                EndRound(winnerOfTwoGrids, new List<Contestant>());
                return;
            }

            _currentMatchesGrid1 = ChooseWinnerOfMatchesInGrid(_currentMatchesGrid1);
            _currentMatchesGrid2 = ChooseWinnerOfMatchesInGrid(_currentMatchesGrid2);

            List<Contestant> winners = new List<Contestant>();
            List<Contestant> winnersInLosers = new List<Contestant>();

            foreach (Match match in _currentMatchesGrid2)
                winnersInLosers.Add(match.Winner);
            if (_currentPlayersGrid2.Count % 2 != 0)
                winnersInLosers.Add(_currentPlayersGrid2[_currentPlayersGrid2.Count - 1]);

            foreach (Match match in _currentMatchesGrid1)
            {
                winners.Add(match.Winner);
                winnersInLosers.Add(match.Loser);
            }
            if (_currentPlayersGrid1.Count % 2 != 0)
                winners.Add(_currentPlayersGrid1[_currentPlayersGrid1.Count - 1]);

            if (_currentMatchesGrid1.Count == 0)
                winners = _currentPlayersGrid1;

            int currentGrid2Position = 0;
            foreach (var player in winnersInLosers)
            {
                player.Position = currentGrid2Position;
                currentGrid2Position += player.Name.Length + 1;
            }

            if (_leagueType == LeagueType.SingleElumination)
                EndRound(winners);
            else EndRound(winners, winnersInLosers);
        }
        private static void EndRound(List<Contestant> winnersGrid1,List<Contestant> winnersGrid2 = null)
        { 
            _scoreboard.AddResults(winnersGrid1, winnersGrid2);
            UpdateCurrentPlayers(winnersGrid1,winnersGrid2);
            UpdateCurrentMatches();
        }

        private static List<Match> ChooseWinnerOfMatchesInGrid(List<Match> matchesInGrid)
        {
            foreach (var match in matchesInGrid)
            {
                bool firstPlayerChosen = ConsoleWorker.ChooseMatchWinner(match) == 1; 
                match.SetWinner(firstPlayerChosen ? 1 : 2);
            }
            return matchesInGrid;
        }
        public static bool CheckPlayerNumber()
        {
            if (_currentPlayersGrid1.Count == 1 && _currentPlayersGrid2.Count == 0 || 
                _currentPlayersGrid1.Count == 0 && _currentPlayersGrid2.Count == 1)
            {
                ConsoleWorker.OnePlayerLeft();
                return false;
            }
            if (_currentPlayersGrid1.Count == 0 && _currentPlayersGrid2.Count == 0)
            {
                ConsoleWorker.NoPlayersLeft();
                return false;
            }
            return true;
        }
    }
}
