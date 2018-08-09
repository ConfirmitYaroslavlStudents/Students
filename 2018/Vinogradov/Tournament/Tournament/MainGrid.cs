using System;
using System.Collections.Generic;

namespace Tournament
{
    [Serializable]
    public class MainGrid : Grid
    {
        public int ExtraPlayerTour;

        public MainGrid(string[] playerNames)
        {
            Winner = string.Empty;
            int extraPlayer = playerNames.Length % 2;
            List<int> matchesInEachTour = CalculateTourSize(playerNames.Length, extraPlayer);
            Matches = new Match[matchesInEachTour.Count][];
            Matches[0] = new Match[matchesInEachTour[0]];

            for (int i = 1; i < playerNames.Length; i += 2)
            {
                Matches[0][i / 2] = new Match(playerNames[i - 1], playerNames[i]);
            }

            ExtraPlayerTour = -1;
            FindPlaceForExtraPlayer(0, extraPlayer);

            for (int i = 1; i < Matches.Length; i++)
            {
                Matches[i] = new Match[matchesInEachTour[i]];

                for (int j = 0; j < Matches[i].Length; j++)
                {
                    Matches[i][j] = new Match(string.Empty, string.Empty);
                }

                FindPlaceForExtraPlayer(i, extraPlayer);
            }

            if (extraPlayer == 1)
            {
                Matches[ExtraPlayerTour][Matches[ExtraPlayerTour].Length - 1].Opponents[1] = playerNames[playerNames.Length - 1];
            }
        }

        private List<int> CalculateTourSize(int numberOfPlayers, int extra)
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

        private void FindPlaceForExtraPlayer(int tour, int extraPlayer)
        {
            bool lastPlayerNeedsPlacement = (extraPlayer == 1 && ExtraPlayerTour == -1 && Matches[tour].Length % 2 == 1);

            if (lastPlayerNeedsPlacement)
            {
                ExtraPlayerTour = tour + 1;
            }
        }
    }
}
