using System;

namespace Championship
{
    [Serializable]
    class ChampionshipData
    {
        public SingleEliminationGrid _singleTournamentGrid;
        public DoubleEliminationGrid _doubleTournamentGrid;
        public GridType _gridType = GridType.StandardGrid;
        public bool isTeamInput;
        public Action PlayTour;
        public Action InputTeams;
        public Action DrawGrid;
    }
}
