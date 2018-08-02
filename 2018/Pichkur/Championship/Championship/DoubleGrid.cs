using System;
using System.Collections.Generic;

namespace Championship
{
    class DoubleGrid
    {
        public TournamentGrid WinnersTour;
        public TournamentGrid LoserTour;

        public Game FinalGame = null;
        public DoubleGrid(List<Team> teams)
        {
            WinnersTour = new TournamentGrid(teams);
        }

        public void StartNextTour()
        {
            SetFinalGame();

            if (WinnersTour.Champion != null)
            {
                LoserTour.StartNextTour();
                return;
            }

            var teams = GetLoserTeamFromWinnersTour();
          
            if (LoserTour != null)
            {
                LoserTour.Tours[LoserTour.CountTours]._extraTeams.AddRange(teams);
                LoserTour.StartNextTour();
            }
            else
                LoserTour = new TournamentGrid(teams);

            WinnersTour.StartNextTour();
        }

        private List<Team> GetLoserTeamFromWinnersTour()
        {
            List<Team> teams = new List<Team>();

            foreach (var game in WinnersTour.Tours[WinnersTour.CountTours].Games)
            {
                if (game != null)
                {
                    game.SetWinner();

                    if (game.Winner != null)
                    {
                        if (game.Winner.Equals(game.FirstTeam))
                        {
                            if (game.SecondTeam != null)
                                teams.Add(game.SecondTeam);
                        }
                        else
                            teams.Add(game.FirstTeam);
                    }
                }
            }

            return teams;
        }

        public void SetFinalGame()
        {
            if (WinnersTour.Champion != null && LoserTour.Champion != null)
                FinalGame = new Game(WinnersTour.Champion, LoserTour.Champion);
        }
    }
}
