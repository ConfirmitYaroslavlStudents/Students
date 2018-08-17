using System;
using System.Collections.Generic;
using System.Linq;
using ChampionshipLibrary;

namespace GridRendererLibrary
{
    public class DoubleSideGridRenderer
    {
        public int CursorTop = 0;
        private int _lastTour;
        private SingleEliminationGrid _grid;
        private LeftSideGridRenderer _leftRenderer;
        private RightSideGridRenderer _rightRenderer;

        public DoubleSideGridRenderer(SingleEliminationGrid grid, int minCursorTop)
        {
            _grid = grid;
            _leftRenderer = new LeftSideGridRenderer(grid.Teams, minCursorTop);
            var countTours = grid.CountTours;

            if (grid.Tours[grid.CountTours].Games.Count != 1)
                countTours++;

            _rightRenderer = new RightSideGridRenderer(grid.Teams, minCursorTop, countTours);
        }

        public void StartRenderer()
        {
            int lastTour = _grid.Tours.Count;

            if (_grid.Tours[lastTour - 1].Games.Count == 1)
            {
                lastTour--;
            }
            for (int i = 0; i < lastTour; i++)
            {
                int lastgame = GetMiddleGame(_grid.Tours[i].Games.Count);
                _leftRenderer.Render(_grid.Tours[i].Games.GetRange(0, lastgame), i);
                _rightRenderer.Render(_grid.Tours[i].Games.GetRange(lastgame, _grid.Tours[i].Games.Count - lastgame), i);
            }

            _leftRenderer.PrintChampion(_grid.CountTours, _grid.Champion);
            CursorTop = _leftRenderer.MaxCursorTop + 1;
            Console.CursorTop = CursorTop;
        }

        private List<Team> MakeNameOneLength(List<Team> teams)
        {
            List<Team> result = new List<Team>();
            var nameLength = teams.Max(a => a.Name.Length);

            foreach (var team in teams)
            {
                string newTeamName = team.Name.PadLeft(nameLength);
                team.Name = newTeamName;
                result.Add(team);
            }

            return result;
        }

        private int GetMiddleGame(int countGames)
        {
            return countGames % 2 != 0 ? countGames / 2 + 1 : countGames / 2;
        }
    }
}
