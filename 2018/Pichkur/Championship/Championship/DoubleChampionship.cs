using System;

namespace Championship
{
    [Serializable]
    public class DoubleChampionship : IChampionship
    {
        public bool IsTeamInput { get; set; }
        public IGrid Grid { get; set; }
        public Team Champion { get; set; }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitDoubleChampionship(this);
        }

        public void InputTeams()
        {
            Grid = new DoubleEliminationGrid(DataInput.InputTeams());
            IsTeamInput = true;
            SaveLoadSystem.Save(this);
        }

        public void PlayTour()
        {
            var indexOfLastTour = (Grid as DoubleEliminationGrid).WinnersTour.CountTours;
            var lastTour = (Grid as DoubleEliminationGrid).WinnersTour.Tours[indexOfLastTour];

            DataInput.InputTourGamesScores(lastTour);

            if ((Grid as DoubleEliminationGrid).LosersTour != null)
            {
                indexOfLastTour = (Grid as DoubleEliminationGrid).LosersTour.CountTours;
                lastTour = (Grid as DoubleEliminationGrid).LosersTour.Tours[indexOfLastTour];

                DataInput.InputTourGamesScores(lastTour);
            }

            Grid.StartNextTour();
            SaveLoadSystem.Save(this);

            if ((Grid as DoubleEliminationGrid).FinalGame != null)
            {
                DataInput.InputGameScore((Grid as DoubleEliminationGrid).FinalGame);
            }

            Champion = (Grid as DoubleEliminationGrid).Champion;
            SaveLoadSystem.Save(this);
        }
    }
}
