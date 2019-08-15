using System;
using System.Collections.Generic;

namespace ChampionshipLibrary
{
    [Serializable]
    public class Tour
    {
        public List<Game> Games = new List<Game>();
        public List<Team> _extraTeams = new List<Team>();
        protected int _countGames = 1;

        public Tour(List<Team> teams)
        {
            SetCountGames(teams.Count);

            if (_countGames == 0 || _countGames >= teams.Count / 2)
            {
                DistributeAllTeams(teams);
            }
            else
            {
                DistributeExtraTeams(teams);
            }

            _extraTeams = teams;
        }

        public Tour(Tour lastTour)
        {
            List<Team> teams = new List<Team>();

            foreach (var game in lastTour.Games)
            {
                if (game != null)
                {
                    game.SetWinner();
                   teams.Add(game.Winner);                   
                }
                else
                {
                    if (lastTour._extraTeams.Count > 0)
                    {
                        teams.Add(lastTour._extraTeams[0]);
                        lastTour._extraTeams.RemoveAt(0);
                    }
                }
            }

            foreach(var team in lastTour._extraTeams)
            {
                if (!teams.Contains(team))
                    teams.Add(team);
            }

            SetCountGames(teams.Count);
            DistributeAllTeams(teams);
        }

        public void DistributeAllTeams(List<Team> teams)
        {
            while (teams.Count > 1)
            {
                Games.Add(new Game(teams[0], teams[1]));
                teams.RemoveRange(0, 2);
            }

            if (teams.Count==1)
            {
                Games.Add(new Game(teams[0],null));
            }
        }

        public void DistributeExtraTeams(List<Team> teams)
        {
            for (int i = 0; i < _countGames; i++)
            {
                Games.Add(new Game(teams[0], teams[1]));
                Games.Add(null);
                teams.RemoveRange(0, 2);
            }
        }

        public void SetCountGames(int countTeams)
        {
            int powOfTwo = 1;

            while (powOfTwo * 2 <= countTeams)
            {
                powOfTwo *= 2;
            }

            _countGames = countTeams - powOfTwo;
        }
    }
}
