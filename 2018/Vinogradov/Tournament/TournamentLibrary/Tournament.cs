using System;
using System.Collections.Generic;

namespace TournamentLibrary
{
    [Serializable]
    public class Tournament
    {
        private PlayerCoords _mainWinnerCoords = new PlayerCoords(false, -1, -1, -1);
        private PlayerCoords _losersWinnerCoords = new PlayerCoords(true, -1, -1, -1);

        public bool DoubleElimination;
        public Dictionary<string, Player> Players;
        public MainGrid Main;
        public LoserGrid Losers;
        public Player Champion;

        public Tournament(string[] playerNames, bool isDoubleElimination)
        {
            if (playerNames.Length < 2)
            {
                throw new ArgumentException("At least 2 players are required.");
            }

            playerNames = Shuffle(playerNames);
            DoubleElimination = isDoubleElimination;
            Players = new Dictionary<string, Player>();
            Main = new MainGrid(playerNames);
            Champion = null;

            if (DoubleElimination)
            {
                Losers = new LoserGrid(Main.Matches);
            }

            for (int i = 0; i < Main.Matches[0].Length; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    var player = Main.Matches[0][i].Opponents[j];
                    Players.Add(player.Name, player);
                }
            }

            if (Main.TourForExtraPlayer != -1)
            {
                var matchForExtraPlayer = Main.Matches[Main.TourForExtraPlayer][Main.Matches[Main.TourForExtraPlayer].Length - 1];
                var extraPlayer = matchForExtraPlayer.Opponents[1];
                Players.Add(extraPlayer.Name, extraPlayer);
            }
        }
        
        private string[] Shuffle(string[] names)
        {
            Random numberGenerator = new Random();

            for (int i = names.Length - 1; i > 0; i--)
            {
                int j = numberGenerator.Next(i + 1);

                if (i != j)
                {
                    string tmp = names[i];
                    names[i] = names[j];
                    names[j] = tmp;
                }
            }

            return names;
        }

        public void PlayGame(string winnerName)
        {
            var winner = Players[winnerName];

            if (winner.Coords == null)
            {
                throw new InvalidOperationException("This match was already played.");
            }

            if (winner.Coords == _mainWinnerCoords || winner.Coords == _losersWinnerCoords)
            {
                PlayFinale(winner);
            }
            else
            {
                Grid grid = Main;

                if (winner.Coords.IsLoserGrid)
                {
                    grid = Losers;
                }

                var currentMatch = grid.Matches[winner.Coords.Tour][winner.Coords.MatchNumber];

                if (!currentMatch.PlayersReady)
                {
                    throw new InvalidOperationException("Players are not ready.");
                }

                var loser = currentMatch.Opponents[(winner.Coords.IndexInPair + 1) % 2];

                if (winner.Coords.Tour == grid.Matches.Length - 1)
                {
                    currentMatch.WinnerIndex = winner.Coords.IndexInPair;
                    grid.Winner = winner;

                    if (winner.Coords.IsLoserGrid)
                    {
                        winner.Coords = _losersWinnerCoords;
                    }
                    else
                    {
                        winner.Coords = _mainWinnerCoords;
                    }

                    if (DoubleElimination && !(winner.Coords.IsLoserGrid))
                    {
                        MoveToLosers(loser);
                    }
                    else
                    {
                        loser.Coords = null;
                    }
                }
                else
                {
                    if (winner.Coords.IsLoserGrid)
                    {
                        MoveWinnerInLoserGrid(winner);
                        loser.Coords = null;
                    }
                    else
                    {
                        MoveWinner(winner);

                        if (DoubleElimination)
                        {
                            MoveToLosers(loser);
                        }
                        else
                        {
                            loser.Coords = null;
                        }
                    }
                }
            }
        }

        private void MoveWinnerInLoserGrid(Player player)
        {
            var currentMatch = Losers.Matches[player.Coords.Tour][player.Coords.MatchNumber];
            currentMatch.WinnerIndex = player.Coords.IndexInPair;
            SetValues(player, new PlayerCoords(true, player.Coords.Tour + 1, player.Coords.MatchNumber / 2, player.Coords.MatchNumber % 2));
        }

        private void MoveWinner(Player player)
        {
            var currentMatch = Main.Matches[player.Coords.Tour][player.Coords.MatchNumber];
            currentMatch.WinnerIndex = player.Coords.IndexInPair;
            bool oddNumberOfMatches = (Main.Matches[player.Coords.Tour].Length % 2 == 1);
            bool isLastMatchInTour = (player.Coords.MatchNumber == Main.Matches[player.Coords.Tour].Length - 1);
            bool extraPlayer = (Main.Matches[player.Coords.Tour].Length > 1 && oddNumberOfMatches && isLastMatchInTour);

            if (extraPlayer)
            {
                MoveWinnerExtra(player);
            }
            else
            {
                SetValues(player, new PlayerCoords(false, player.Coords.Tour + 1, player.Coords.MatchNumber / 2, player.Coords.MatchNumber % 2));
            }
        }

        private void MoveWinnerExtra(Player player)
        {
            for (int i = player.Coords.Tour + 1; i < Main.Matches.Length; i++)
            {
                if (Main.Matches[i].Length % 2 == 1)
                {
                    SetValues(player, new PlayerCoords(false, i + 1, Main.Matches[i + 1].Length - 1, 1));
                    break;
                }
                else if (i == Main.TourForExtraPlayer)
                {
                    SetValues(player, new PlayerCoords(false, i, Main.Matches[i].Length - 1, 0));
                    break;
                }
            }
        }

        private void MoveToLosers(Player player)
        {
            int position = player.Coords.MatchNumber;

            if (player.Coords.Tour > 0)
            {
                position += Losers.Matches[player.Coords.Tour - 1].Length;
            }

            int indexInPair = position % 2;
            position /= 2;

            if (position >= Losers.Matches[player.Coords.Tour].Length)
            {
                MoveToLosersExtra(player);
            }
            else
            {
                SetValues(player, new PlayerCoords(true, player.Coords.Tour, position, indexInPair));
            }
        }

        private void MoveToLosersExtra(Player player)
        {
            if (player.Coords.Tour + 1 == Losers.Matches.Length)
            {
                Losers.Winner = player;
            }
            else
            {
                for (int i = player.Coords.Tour + 1; i < Losers.Matches.Length; i++)
                {
                    int numberOfPlayers = Losers.Matches[i - 1].Length;

                    if (i < Main.Matches.Length)
                    {
                        numberOfPlayers += Main.Matches[i].Length;
                    }

                    if (numberOfPlayers % 2 == 1)
                    {
                        SetValues(player, new PlayerCoords(true, i, Losers.Matches[i].Length - 1, 1));
                        break;
                    }
                }
            }
        }

        private void SetValues(Player player, PlayerCoords match)
        {
            Grid grid = Main;

            if (match.IsLoserGrid)
            {
                grid = Losers;
            }

            grid.Matches[match.Tour][match.MatchNumber].Opponents[match.IndexInPair] = player;
            player.Coords = match;
        }

        private void PlayFinale(Player winner)
        {
            if (Main.Winner == null || Losers.Winner == null)
            {
                throw new InvalidOperationException("Players are not ready.");
            }

            if (winner.Coords == _mainWinnerCoords)
            {
                Champion = Main.Winner;
            }
            else
            {
                Champion = Losers.Winner;
            }

            Main.Winner.Coords = null;
            Losers.Winner.Coords = null;
        }
    }
}
