using System.Collections.Generic;

namespace Championship
{
    public class TournamentGrid
    {
        public List<Tour> Tours = new List<Tour>();
        public int CountTours { get; private set; }
        public string Champion { get; private set; }
        public List<Team> Teams { get; private set; }

        public TournamentGrid(List<Team> teams)
        {
            Teams = new List<Team>(teams);
            Tours.Add(new Tour(teams));
            CountTours = 0;
            Champion = null;
        }

        public void StartNextTour()
        {
            if ((Tours[CountTours].Games.Count + Tours[CountTours]._extraTeams.Count) > 1)
            {
                Tours.Add(new Tour(Tours[CountTours]));
                CountTours++;
            }
            else
                PlayFinalGame();
        }

        private void PlayFinalGame()
        {
            Tours[CountTours].Games[0].SetWinner();
            Champion = Tours[CountTours].Games[0].Winner;
        }
    }
}
