using System;

namespace Tournament
{
    public class TournamentController
    {
        public bool DoubleElimination;
        public string[] Players;
        public MainGrid Main;
        public LoserGrid Losers;
        public int Champion;

        public TournamentController(int numberOfPlayers, string[] playerNames, bool isDoubleElimination)
        {
            if (numberOfPlayers < 2)
            {
                throw new ArgumentException("At least 2 players are required.");
            }

            DoubleElimination = isDoubleElimination;
            Main = new MainGrid(numberOfPlayers);
            Champion = -1;

            if (DoubleElimination)
            {
                Losers = new LoserGrid(Main.Matches);
            }

            Players = playerNames;
            Shuffle();
        }
        
        private void Shuffle()
        {
            Random numberGenerator = new Random();

            for (int i = Players.Length - 1; i > 0; i--)
            {
                int j = numberGenerator.Next(i + 1);

                if (i != j)
                {
                    string tmp = Players[i];
                    Players[i] = Players[j];
                    Players[j] = tmp;
                }
            }
        }

        public void PlayGame(MatchInfo match, bool loserGrid)
        {
            Grid grid = Main;

            if (loserGrid)
            {
                grid = Losers;
            }

            if (MatchParametersValidation(match, grid))
            {
                var currentMatch = grid.Matches[match.Tour][match.MatchNumber];

                if (currentMatch.Winner == -1)
                {
                    if (match.Tour == grid.Matches.Length - 1)
                    {
                        currentMatch.Winner = match.Winner;
                        grid.Winner = currentMatch.Opponents[currentMatch.Winner];

                        if (DoubleElimination && !(loserGrid))
                        {
                            MoveToLosers(match);
                        }
                    }
                    else
                    {
                        if (loserGrid)
                        {
                            MoveLoser(match);
                        }
                        else
                        {
                            MoveWinner(match);

                            if (DoubleElimination)
                            {
                                MoveToLosers(match);
                            }
                        }
                    }
                }
                else
                    throw new InvalidOperationException("This match was already played.");
            }
            else
                throw new ArgumentException("Invalid match parameters.");
        }

        private bool MatchParametersValidation(MatchInfo match, Grid grid)
        {
            bool correctTour = (match.Tour >= 0 && match.Tour < grid.Matches.Length);
            bool correctWinner = (match.Winner == 0 || match.Winner == 1);

            if (correctTour && correctWinner)
            {
                bool correctMatch = (match.MatchNumber >= 0 && match.MatchNumber < grid.Matches[match.Tour].Length);
                return (correctMatch && grid.Matches[match.Tour][match.MatchNumber].PlayersReady);
            }

            return false;
        }

        private void MoveLoser(MatchInfo match)
        {
            var currentMatch = Losers.Matches[match.Tour][match.MatchNumber];
            currentMatch.Winner = match.Winner;
            var nextMatch = Losers.Matches[match.Tour + 1][match.MatchNumber / 2];
            nextMatch.Opponents[match.MatchNumber % 2] = currentMatch.Opponents[currentMatch.Winner];
        }

        private void MoveWinner(MatchInfo match)
        {
            var currentMatch = Main.Matches[match.Tour][match.MatchNumber];
            currentMatch.Winner = match.Winner;
            bool extraPlayer = (Main.Matches[match.Tour].Length > 1 && Main.Matches[match.Tour].Length % 2 == 1 && match.MatchNumber == Main.Matches[match.Tour].Length - 1);

            if (extraPlayer)
            {
                MoveWinnerExtra(currentMatch.Opponents[currentMatch.Winner], match.Tour);
            }
            else
            {
                var nextMatch = Main.Matches[match.Tour + 1][match.MatchNumber / 2];
                nextMatch.Opponents[match.MatchNumber % 2] = currentMatch.Opponents[currentMatch.Winner];
            }
        }

        private void MoveWinnerExtra(int player, int tour)
        {
            for (int i = tour + 1; i < Main.Matches.Length; i++)
            {
                if (Main.Matches[i].Length % 2 == 1)
                {
                    Main.Matches[i + 1][Main.Matches[i + 1].Length - 1].Opponents[1] = player;
                    break;
                }
                else if (i == Main.ExtraPlayerTour)
                {
                    Main.Matches[i][Main.Matches[i].Length - 1].Opponents[0] = player;
                    break;
                }
            }
        }

        private void MoveToLosers(MatchInfo match)
        {
            Match currentMatch = Main.Matches[match.Tour][match.MatchNumber];
            int loser = currentMatch.Opponents[(match.Winner + 1) % 2];
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
                Losers.Matches[match.Tour][position].Opponents[indexInPair] = loser;
            }
        }

        private void MoveToLosersExtra(int player, int tour)
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
                        Losers.Matches[i][Losers.Matches[i].Length - 1].Opponents[1] = player;
                        break;
                    }
                }
            }
        }

        public void PlayFinale(int winner)
        {
            if (Main.Winner == -1 || Losers.Winner == -1)
            {
                throw new InvalidOperationException("Players are not ready.");
            }
            switch (winner)
            {
                case 0:
                    {
                        Champion = Main.Winner;
                        break;
                    }
                case 1:
                    {
                        Champion = Losers.Winner;
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("Invalid match parameters.");
                    }
            }
        }
    }
}
