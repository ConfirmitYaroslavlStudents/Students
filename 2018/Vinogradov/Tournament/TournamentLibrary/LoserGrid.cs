using System;
using System.Collections.Generic;

namespace TournamentLibrary
{
    [Serializable]
    public class LoserGrid : Grid
    {
        public LoserGrid(Match[][] mainGrid)
        {
            if (mainGrid == null)
            {
                throw new InvalidOperationException("Must create the main grid first.");
            }

            Winner = null;
            var matchesInEachTour = CalculateTourSizes(mainGrid);
            Matches = new Match[matchesInEachTour.Count][];
            for (int i = 0; i < Matches.Length; i++)
            {
                Matches[i] = new Match[matchesInEachTour[i]];

                for (int j = 0; j < Matches[i].Length; j++)
                {
                    Matches[i][j] = new Match(null, null);
                }
            }
        }

        private List<int> CalculateTourSizes(Match[][] mainGrid)
        {
            var matchesInEachTour = new List<int>();
            int playersInTour = mainGrid[0].Length;
            int currentTour = 0;

            while (playersInTour > 1 || currentTour < mainGrid.Length)
            {
                int numberOfMathes = playersInTour / 2;
                int extraPlayer = playersInTour % 2;
                matchesInEachTour.Add(numberOfMathes);
                playersInTour = numberOfMathes + extraPlayer;
                currentTour++;

                if (currentTour < mainGrid.Length)
                {
                    playersInTour += mainGrid[currentTour].Length;
                }
            }

            return matchesInEachTour;
        }
    }
}
