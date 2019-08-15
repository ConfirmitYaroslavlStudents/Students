using System;
using System.Collections.Generic;
using System.Linq;
using ChampionshipLibrary;

namespace GridRendererLibrary
{
    class OneSideGridRenderer
    {
        public int CursorTop = 0;
        private LeftSideGridRenderer _renderer;
        private int _lastTour;
        private SingleEliminationGrid _grid;

        public OneSideGridRenderer(SingleEliminationGrid grid, int minCursorTop)
        {
            _renderer = new LeftSideGridRenderer(grid.Teams, minCursorTop);
            _lastTour = grid.Tours.Count;
            _grid = grid;
        }

        public void StartRenderer()
        {
            for (int i = 0; i < _lastTour; i++)
            {
                _renderer.Render(_grid.Tours[i].Games, i);
            }

            _renderer.PrintChampion(_grid.CountTours + 1, _grid.Champion);
            CursorTop = _renderer.MaxCursorTop + 1;
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
    }
}
