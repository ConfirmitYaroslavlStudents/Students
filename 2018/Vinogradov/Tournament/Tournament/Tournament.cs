using System;
using System.Collections.Generic;

namespace Tournament
{
    [Serializable]
    public class Tournament
    {
        private MatchCoords _mainWinnerCoords = new MatchCoords(false, -1, -1, -1);
        private MatchCoords _losersWinnerCoords = new MatchCoords(true, -1, -1, -1);

        public bool DoubleElimination;
        public Dictionary<string, MatchCoords> PlayersCoords;
        public MainGrid Main;
        public LoserGrid Losers;
        public string Champion;

        public Tournament(string[] playerNames, bool isDoubleElimination)
        {
            if (playerNames.Length < 2)
            {
                throw new ArgumentException("At least 2 players are required.");
            }

            playerNames = Shuffle(playerNames);
            DoubleElimination = isDoubleElimination;
            PlayersCoords = new Dictionary<string, MatchCoords>();
            Main = new MainGrid(playerNames);
            Champion = string.Empty;

            if (DoubleElimination)
            {
                Losers = new LoserGrid(Main.Matches);
            }

            for (int i = 0; i < playerNames.Length; i++)
            {
                PlayersCoords.Add(playerNames[i], new MatchCoords(false, 0, i / 2, i % 2));
            }

            if (Main.ExtraPlayerTour != -1)
            {
                PlayersCoords[playerNames[playerNames.Length - 1]] = new MatchCoords(false, Main.ExtraPlayerTour, Main.Matches[Main.ExtraPlayerTour].Length - 1, 1);
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

        public void PlayGame(string winner)
        {
            var match = PlayersCoords[winner];

            if (match == null)
            {
                throw new InvalidOperationException("This match was already played.");
            }

            if (match == _mainWinnerCoords || match == _losersWinnerCoords)
            {
                PlayFinale(match);
            }
            else
            {
                Grid grid = Main;

                if (match.IsLoserGrid)
                {
                    grid = Losers;
                }

                var currentMatch = grid.Matches[match.Tour][match.MatchNumber];

                if (!currentMatch.PlayersReady)
                {
                    throw new InvalidOperationException("Players are not ready.");
                }

                string loser = currentMatch.Opponents[(match.Player + 1) % 2];

                if (match.Tour == grid.Matches.Length - 1)
                {
                    currentMatch.Winner = match.Player;
                    grid.Winner = winner;
                    PlayersCoords[winner] = _mainWinnerCoords;

                    if (match.IsLoserGrid)
                    {
                        PlayersCoords[winner] = _losersWinnerCoords;
                    }

                    if (DoubleElimination && !(match.IsLoserGrid))
                    {
                        MoveToLosers(loser);
                    }
                    else
                    {
                        PlayersCoords[loser] = null;
                    }
                }
                else
                {
                    if (match.IsLoserGrid)
                    {
                        MoveLoser(winner, match);
                        PlayersCoords[loser] = null;
                    }
                    else
                    {
                        MoveWinner(winner, match);

                        if (DoubleElimination)
                        {
                            MoveToLosers(loser);
                        }
                        else
                        {
                            PlayersCoords[loser] = null;
                        }
                    }
                }
            }
        }

        private void MoveLoser(string player, MatchCoords match)
        {
            var currentMatch = Losers.Matches[match.Tour][match.MatchNumber];
            currentMatch.Winner = match.Player;
            SetValues(player, new MatchCoords(true, match.Tour + 1, match.MatchNumber / 2, match.MatchNumber % 2));
        }

        private void MoveWinner(string player, MatchCoords match)
        {
            var currentMatch = Main.Matches[match.Tour][match.MatchNumber];
            currentMatch.Winner = match.Player;
            bool extraPlayer = (Main.Matches[match.Tour].Length > 1 && Main.Matches[match.Tour].Length % 2 == 1 && match.MatchNumber == Main.Matches[match.Tour].Length - 1);

            if (extraPlayer)
            {
                MoveWinnerExtra(player, match.Tour);
            }
            else
            {
                SetValues(player, new MatchCoords(false, match.Tour + 1, match.MatchNumber / 2, match.MatchNumber % 2));
            }
        }

        private void MoveWinnerExtra(string player, int tour)
        {
            for (int i = tour + 1; i < Main.Matches.Length; i++)
            {
                if (Main.Matches[i].Length % 2 == 1)
                {
                    SetValues(player, new MatchCoords(false, i + 1, Main.Matches[i + 1].Length - 1, 1));
                    break;
                }
                else if (i == Main.ExtraPlayerTour)
                {
                    SetValues(player, new MatchCoords(false, i, Main.Matches[i].Length - 1, 0));
                    break;
                }
            }
        }

        private void MoveToLosers(string loser)
        {
            var match = PlayersCoords[loser];
            int position = match.MatchNumber;

            if (match.Tour > 0)
            {
                position += Losers.Matches[match.Tour - 1].Length;
            }

            int indexInPair = position % 2;
            position /= 2;

            if (position >= Losers.Matches[match.Tour].Length)
            {
                MoveToLosersExtra(loser, match.Tour);
            }
            else
            {
                SetValues(loser, new MatchCoords(true, match.Tour, position, indexInPair));
            }
        }

        private void MoveToLosersExtra(string player, int tour)
        {
            if (tour + 1 == Losers.Matches.Length)
            {
                Losers.Winner = player;
            }
            else
            {
                for (int i = tour + 1; i < Losers.Matches.Length; i++)
                {
                    int numberOfPlayers = Losers.Matches[i - 1].Length;

                    if (i < Main.Matches.Length)
                    {
                        numberOfPlayers += Main.Matches[i].Length;
                    }

                    if (numberOfPlayers % 2 == 1)
                    {
                        SetValues(player, new MatchCoords(true, i, Losers.Matches[i].Length - 1, 1));
                        break;
                    }
                }
            }
        }

        private void SetValues(string player, MatchCoords match)
        {
            Grid grid = Main;

            if (match.IsLoserGrid)
            {
                grid = Losers;
            }

            grid.Matches[match.Tour][match.MatchNumber].Opponents[match.Player] = player;
            PlayersCoords[player] = match;
        }

        private void PlayFinale(MatchCoords winner)
        {
            if (Main.Winner == string.Empty || Losers.Winner == string.Empty)
            {
                throw new InvalidOperationException("Players are not ready.");
            }

            if (winner == _mainWinnerCoords)
            {
                Champion = Main.Winner;
            }
            else
            {
                Champion = Losers.Winner;
            }

            PlayersCoords[Main.Winner] = null;
            PlayersCoords[Losers.Winner] = null;
        }
    }
}
