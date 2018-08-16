using System;

namespace Championship
{
    [Serializable]
    public class DoubleChampionshipManager : IChampionship
    {
        public bool IsTeamInput { get; set; }
        public IGrid Grid { get; set; }
        public Team Champion { get; set; }
        [NonSerialized]
        public DataInput dataInput;

        public DoubleChampionshipManager(DataInput dataInput)
        {
            IsTeamInput = false;
            this.dataInput = dataInput;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void InputTeams()
        {
            Grid = new DoubleEliminationGrid(dataInput.InputTeams());
            IsTeamInput = true;
            SaveLoadSystem.Save(this);
        }

        public void PlayTour()
        {
            var indexOfLastTour = (Grid as DoubleEliminationGrid).WinnersTour.CountTours;
            var lastTour = (Grid as DoubleEliminationGrid).WinnersTour.Tours[indexOfLastTour];

            dataInput.InputTourGamesScores(lastTour);

            if ((Grid as DoubleEliminationGrid).LosersTour != null)
            {
                indexOfLastTour = (Grid as DoubleEliminationGrid).LosersTour.CountTours;
                lastTour = (Grid as DoubleEliminationGrid).LosersTour.Tours[indexOfLastTour];

                dataInput.InputTourGamesScores(lastTour);
            }

            Grid.StartNextTour();
            SaveLoadSystem.Save(this);

            if ((Grid as DoubleEliminationGrid).FinalGame != null)
            {
                dataInput.InputGameScore((Grid as DoubleEliminationGrid).FinalGame);
            }

            Champion = (Grid as DoubleEliminationGrid).Champion;
            SaveLoadSystem.Save(this);
        }
    }
}
