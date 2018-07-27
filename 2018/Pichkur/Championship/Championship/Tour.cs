using System.Collections.Generic;

namespace Championship
{
    public class Tour
    {
        public List<Game> Games = new List<Game>();
        public List<Team> _extraTeams = new List<Team>();
        private int _countGames = 1;
        public Tour(List<Team> teams)
        {
            SetCountGames(teams.Count);

            if (_countGames == 0 || _countGames >= teams.Count / 2)
            {
                DistributeAllCommands(teams);
            }
            else
            {
                DistributeExtraCommands(teams);
            }

            _extraTeams = teams;
        }

        public Tour(Tour tour)
        {
            List<Team> teams = new List<Team>();

            foreach (var game in tour.Games)
            {
                game.SetWinner();

                if (game.Winner != null)
                {
                    teams.Add(new Team(game.Winner));
                }
                else
                {
                    if (tour._extraTeams.Count > 0)
                    {
                        teams.Add(tour._extraTeams[0]);
                        tour._extraTeams.RemoveAt(0);
                    }
                }
            }

            teams.AddRange(tour._extraTeams);

            SetCountGames(teams.Count);
            DistributeAllCommands(teams);
        }

        private void DistributeAllCommands(List<Team> teams)
        {
            while (teams.Count > 1)
            {
                Games.Add(new Game(teams[0], teams[1]));
                teams.RemoveRange(0, 2);
            }
        }
        private void DistributeExtraCommands(List<Team> teams)
        {
            for (int i = 0; i < _countGames; i++)
            {
                Games.Add(new Game(teams[0], teams[1]));
                Games.Add(new Game(new Team(), new Team()));
                teams.RemoveRange(0, 2);
            }
        }

        private void SetCountGames(int countTeams)
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
