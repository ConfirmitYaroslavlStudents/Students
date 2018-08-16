using System;
using System.Collections.Generic;

namespace Championship
{
    [Serializable]
    public class DoubleEliminationGrid : IGrid
    {
        public SingleEliminationGrid WinnersTour;
        public SingleEliminationGrid LosersTour;
        public Game FinalGame = null;
        public Team Champion { get; set; }

        public DoubleEliminationGrid(List<Team> teams)
        {
            WinnersTour = new SingleEliminationGrid(teams);
        }

        public void StartNextTour()
        {
            SetFinalGame();

            if (WinnersTour.Champion != null)
            {
                LosersTour.StartNextTour();
                return;
            }

            var teams = GetLoserTeamsFromWinnersTour();

            if (LosersTour != null)
            {
                LosersTour.Tours[LosersTour.CountTours]._extraTeams.AddRange(teams);
                LosersTour.StartNextTour();
            }
            else
            {
                LosersTour = new SingleEliminationGrid(teams);
            }

            WinnersTour.StartNextTour();
        }

        private List<Team> GetLoserTeamsFromWinnersTour()
        {
            List<Team> teams = new List<Team>();

            foreach (var game in WinnersTour.Tours[WinnersTour.CountTours].Games)
            {
                if (game != null)
                {
                    game.SetWinner();

                    if (game.Winner != null)
                    {
                        Team loserTeam = game.GetLoser();

                        if (loserTeam != null)
                            teams.Add(loserTeam);
                    }
                }
            }

            return teams;
        }

        private void SetFinalGame()
        {
            if (FinalGame != null)
                SetChampion();
            else
                if (WinnersTour.Champion != null && LosersTour.Champion != null)
                FinalGame = new Game(WinnersTour.Champion, LosersTour.Champion);
        }

        public void SetChampion()
        {
            if (FinalGame != null)
                Champion = FinalGame.Winner;
        }
    }
}
