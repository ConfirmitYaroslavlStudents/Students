using System;

namespace TournamentLibrary
{
    [Serializable]
    public class PlayerCoords
    {
        public readonly bool IsLoserGrid;
        public readonly int Tour;
        public readonly int MatchNumber;
        public readonly int IndexInPair;

        public PlayerCoords(bool loserGrid, int tour, int match, int playerIndex)
        {
            IsLoserGrid = loserGrid;
            Tour = tour;
            MatchNumber = match;
            IndexInPair = playerIndex;
        }
    }
}
