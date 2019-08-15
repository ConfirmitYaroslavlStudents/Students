using System;
using System.Collections.Generic;

namespace ChampionshipLibrary
{
    [Serializable]
    public class SingleEliminationGrid : IGrid
    {
        public List<Tour> Tours = new List<Tour>();
        public int CountTours { get; private set; }
        public Team Champion { get; set; }
        public List<Team> Teams { get; private set; }

        public SingleEliminationGrid(List<Team> teams)
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
                SetChampion();
        }

        public void SetChampion()
        {
            Tours[CountTours].Games[0].SetWinner();
            Champion = Tours[CountTours].Games[0].Winner;
        }
    }
}
