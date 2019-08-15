using System;

namespace TournamentLibrary
{
    [Serializable]
    public abstract class Grid
    {
        public Match[][] Matches;
        public Player Winner;
    }
}
