using System;
using System.Collections.Generic;

namespace TournamentLibrary
{
    [Serializable]
    public class MainGrid : Grid
    {
        public int TourForExtraPlayer;

        public MainGrid(string[] playerNames)
        {
            Winner = null;
            int extraPlayerExists = playerNames.Length % 2;
            List<int> matchesInEachTour = CalculateTourSizes(playerNames.Length, extraPlayerExists);
            Matches = new Match[matchesInEachTour.Count][];
            Matches[0] = new Match[matchesInEachTour[0]];

            for (int i = 1; i < playerNames.Length; i += 2)
            {
                var playerOne = new Player(playerNames[i - 1], new PlayerCoords(false, 0, i / 2, 0));
                var playerTwo = new Player(playerNames[i], new PlayerCoords(false, 0, i / 2, 1));
                Matches[0][i / 2] = new Match(playerOne, playerTwo);
            }

            TourForExtraPlayer = -1;
            FindPlaceForExtraPlayer(0, extraPlayerExists);

            for (int i = 1; i < Matches.Length; i++)
            {
                Matches[i] = new Match[matchesInEachTour[i]];

                for (int j = 0; j < Matches[i].Length; j++)
                {
                    Matches[i][j] = new Match(null, null);
                }

                FindPlaceForExtraPlayer(i, extraPlayerExists);
            }

            if (extraPlayerExists == 1)
            {
                var extraPlayer = new Player(playerNames[playerNames.Length - 1], new PlayerCoords(false, TourForExtraPlayer, Matches[TourForExtraPlayer].Length - 1, 1));
                var matchForExtraPlayer = Matches[TourForExtraPlayer][Matches[TourForExtraPlayer].Length - 1];
                matchForExtraPlayer.Opponents[1] = extraPlayer;
            }
        }

        private List<int> CalculateTourSizes(int numberOfPlayers, int extra)
        {
            var matchesInEachTour = new List<int>();
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
                matchesInEachTour.Add(numberOfPlayers);
            }

            return matchesInEachTour;
        }

        private void FindPlaceForExtraPlayer(int tour, int extraPlayerExists)
        {
            bool lastPlayerNeedsPlacement = (extraPlayerExists == 1 && TourForExtraPlayer == -1 && Matches[tour].Length % 2 == 1);

            if (lastPlayerNeedsPlacement)
            {
                TourForExtraPlayer = tour + 1;
            }
        }
    }
}
