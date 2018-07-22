using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament
{
    public class TournamentController
    {
        private int _extraPlayerTour;

        public string[] Players;
        public Match[][] Matches;
        public int Champion;

        public TournamentController(int numberOfPlayers, string[] playerNames)
        {
            if (numberOfPlayers < 2)
            {
                throw new ArgumentException("At least 2 players are required.");
            }

            int extraPlayer = numberOfPlayers % 2;
            Players = new string[numberOfPlayers];
            List<int> matchesInEachTour = CountPairs(numberOfPlayers, extraPlayer);
            Matches = new Match[matchesInEachTour.Count][];
            Matches[0] = new Match[matchesInEachTour[0]];

            for (int i = 1; i < numberOfPlayers; i += 2)
            {
                Matches[0][i / 2] = new Match(i - 1, i);
            }

            _extraPlayerTour = -1;
            bool lastPlayerNeedsPlacement = (extraPlayer == 1 && _extraPlayerTour == -1 && Matches[0].Length % 2 == 1);

            if (lastPlayerNeedsPlacement)
            {
                _extraPlayerTour = 1;
            }

            for (int i = 1; i < Matches.Length; i++)
            {
                Matches[i] = new Match[matchesInEachTour[i]];

                for (int j = 0; j < Matches[i].Length; j++)
                {
                    Matches[i][j] = new Match(-1, -1);
                }

                lastPlayerNeedsPlacement = (extraPlayer == 1 && _extraPlayerTour == -1 && Matches[i].Length % 2 == 1);

                if (lastPlayerNeedsPlacement)
                {
                    _extraPlayerTour = i + 1;
                }
            }

            if (extraPlayer == 1)
            {
                Matches[_extraPlayerTour][Matches[_extraPlayerTour].Length - 1].Opponents[1] = numberOfPlayers - 1;
            }

            Champion = -1;
            Players = playerNames;
            Shuffle();
        }

        private List<int> CountPairs(int numberOfPlayers, int extra)
        {
            List<int> matchesOfEachTour = new List<int>();
            numberOfPlayers -= extra;

            while (extra > 0 || numberOfPlayers > 1)
            {
                if (numberOfPlayers % 2 == 1)
                {
                    if (extra == 1)
                    {
                        numberOfPlayers++;
                        extra = 0;
                    }
                    else
                    {
                        numberOfPlayers--;
                        extra = 1;
                    }
                }

                numberOfPlayers = numberOfPlayers / 2;
                matchesOfEachTour.Add(numberOfPlayers);
            }

            return matchesOfEachTour;
        }

        private void Shuffle()
        {
            Random rng = new Random();

            for (int i = Players.Length - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);

                if (i != j)
                {
                    string tmp = Players[i];
                    Players[i] = Players[j];
                    Players[j] = tmp;
                }
            }
        }

        public void PlayGame(MatchInfo match)
        {
            if (MatchParametersValidation(match))
            {
                if (match.Tour == Matches.Length - 1)
                {
                    if (Champion == -1)
                    {
                        var currentMatch = Matches[match.Tour][match.MatchNumber];
                        currentMatch.Winner = match.Winner;
                        Champion = currentMatch.Opponents[currentMatch.Winner];
                    }

                    else
                        throw new InvalidOperationException("This match was already played.");
                }
                else
                {
                    MovePlayer(match);
                }
            }
            else
                throw new ArgumentException("Invalid match parameters.");
        }

        private bool MatchParametersValidation(MatchInfo match)
        {
            bool correctTour = (match.Tour >= 0 && match.Tour < Matches.Length);
            bool correctWinner = (match.Winner == 0 || match.Winner == 1);

            if (correctTour && correctWinner)
            {
                bool correctMatch = (match.MatchNumber >= 0 && match.MatchNumber < Matches[match.Tour].Length);
                return (correctMatch && Matches[match.Tour][match.MatchNumber].PlayersReady);
            }

            return false;
        }

        private void MovePlayer(MatchInfo match)
        {
            Match currentMatch = Matches[match.Tour][match.MatchNumber];

            if (currentMatch.Winner == -1)
            {
                currentMatch.Winner = match.Winner;
                bool extraPlayer = (Matches[match.Tour].Length > 1 && Matches[match.Tour].Length % 2 == 1 && match.MatchNumber == Matches[match.Tour].Length - 1);

                if (extraPlayer)
                {
                    MoveExtraPlayer(currentMatch.Opponents[currentMatch.Winner], match.Tour);
                }
                else
                {
                    Match nextMatch = Matches[match.Tour + 1][match.MatchNumber / 2];
                    nextMatch.Opponents[match.MatchNumber % 2] = currentMatch.Opponents[currentMatch.Winner];
                }
            }
            else
                throw new InvalidOperationException("This match was already played.");
        }

        private void MoveExtraPlayer(int player, int tour)
        {
            for (int i = tour + 1; i < Matches.Length; i++)
            {
                if (Matches[i].Length % 2 == 1)
                {
                    Matches[i + 1][Matches[i + 1].Length - 1].Opponents[1] = player;
                    break;
                }
                else if (i == _extraPlayerTour)
                {
                    Matches[i][Matches[i].Length - 1].Opponents[0] = player;
                    break;
                }
            }
        }
    }
}
