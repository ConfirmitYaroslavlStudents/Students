using System;

namespace Tournament
{
    [Serializable]
    public class MatchCoords
    {
        public readonly bool IsLoserGrid;
        public readonly int Tour;
        public readonly int MatchNumber;
        public readonly int Player;

        public MatchCoords(bool loserGrid, int tour, int match, int player)
        {
            IsLoserGrid = loserGrid;
            Tour = tour;
            MatchNumber = match;
            Player = player;
        }
    }
}
