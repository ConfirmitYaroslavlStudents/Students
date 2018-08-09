using System;

namespace Championship
{
    [Serializable]
    public class SingleChampionship : IChampionship
    {
        public bool IsTeamInput { get; set; }
        public IGrid Grid { get; set; }
        public Team Champion { get; set; }

        public SingleChampionship()
        {
            IsTeamInput = false;
        }

        public void InputTeams()
        {
            Grid = new SingleEliminationGrid(DataInput.InputTeams());
            IsTeamInput = true;
            SaveLoadSystem.Save(this);
        }

        public void PlayTour()
        {
            var indexOfLastTour = (Grid as SingleEliminationGrid).CountTours;
            var lastTour = (Grid as SingleEliminationGrid).Tours[indexOfLastTour];

            DataInput.InputTourGamesScores(lastTour);
            Grid.StartNextTour();
            Champion = (Grid as SingleEliminationGrid).Champion;
            SaveLoadSystem.Save(this);
        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitSingleChampionship(this);
        }

    }
}
