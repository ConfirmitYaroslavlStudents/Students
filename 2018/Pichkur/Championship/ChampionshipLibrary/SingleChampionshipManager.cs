using System;

namespace ChampionshipLibrary
{
    [Serializable]
    public class SingleChampionshipManager : IChampionship
    {
        public bool IsTeamInput { get; set; }
        public IGrid Grid { get; set; }
        public Team Champion { get; set; }
        [NonSerialized]
        public DataInput dataInput;

        public SingleChampionshipManager(DataInput dataInput)
        {
            IsTeamInput = false;
            this.dataInput = dataInput;
        }

        public void InputTeams()
        {
            Grid = new SingleEliminationGrid(dataInput.InputTeams());
            IsTeamInput = true;
            SaveLoadSystem.Save(this);
        }

        public void PlayTour()
        {
            var indexOfLastTour = (Grid as SingleEliminationGrid).CountTours;
            var lastTour = (Grid as SingleEliminationGrid).Tours[indexOfLastTour];

            dataInput.InputTourGamesScores(lastTour);
            Grid.StartNextTour();
            Champion = (Grid as SingleEliminationGrid).Champion;
            SaveLoadSystem.Save(this);
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
