using System;
using System.Collections.Generic;
using System.Linq;

namespace Championship
{
    class GridDrawer
    {
        private int _nameLength;
        private int _shift;
        private int _rightBorder = 0;
        private int _widthOfFirstBranch = 3;
        public int _maxCursorTop = 0;
        private List<Team> _team = new List<Team>();
        private readonly int _minCursorTop=0;
        private string _defoltTeamName="";

        public GridDrawer(List<Team> teams, int minCursorTop)
        {
            _team = new List<Team>(teams);
            _team = MakeNameOneLength(_team);
            _minCursorTop = minCursorTop;
        }

        public void DrawGrid(SingleEliminationGrid grid, GridType type)
        {
            var allTeams = new List<Team>(_team);
            var oneTourLength = _nameLength + 4;
            var countTours = grid.CountTours;

            if (grid.Tours[grid.CountTours].Games.Count != 1)
                countTours++;

            _rightBorder = 2 * oneTourLength * countTours+1;

            int lastTour = grid.Tours.Count;

            if (type == GridType.DoubleGrid && grid.Tours[lastTour - 1].Games.Count == 1)
            {
                lastTour--;
            }

            for (int i = 0; i < lastTour; i++)
            {
                int lastgame = GetMiddleGame(grid.Tours[i].Games.Count);

                for (int j = 0; j < grid.Tours[i].Games.Count; j++)
                {
                    var currentGame = grid.Tours[i].Games[j];

                    if (j < lastgame || type == GridType.StandardGrid)
                    {
                        DrawBranch(true, i, j, currentGame);
                    }
                    else
                    {
                        DrawBranch(false, i, j - lastgame, currentGame);
                    }
                }
            }

            PrintChampion(grid.CountTours+1, grid.Champion, type);
            Console.CursorTop = _maxCursorTop + 1;
            _team = allTeams;
        }

        private List<Team> MakeNameOneLength(List<Team> teams)
        {
            List<Team> result = new List<Team>();
            _nameLength = teams.Max(a => a.Name.Length);
            _shift = _nameLength-1;
            _defoltTeamName = _defoltTeamName.PadLeft(_nameLength);

            foreach (var team in teams)
            {
                string newTeamName = team.Name.PadLeft(_nameLength);
                team.Name = newTeamName;
                result.Add(team);
            }
            
            return result;
        }

        private int GetMiddleGame(int countGames)
        {
            return countGames % 2 != 0 ? countGames / 2 + 1 : countGames / 2;
        }

        private void DrawBranch(bool isLeft, int i, int j, Game game)
        {
            if (game == null)
                return;

            PrintUpLine(isLeft, i, j, game);
            PrintMiddleLine(isLeft, i, j, game);
            PrintDownLine(isLeft, i, j, game);
        }

        private void PrintUpLine(bool isLeft, int i, int j, Game game)
        {
            SetColor(game.Winner, game.FirstTeam);

            int y = GetCursorLeft(isLeft, i);
            SetCursorTop(i, j);

            DrawHorizontalLine(isLeft, y, game.FirstTeam);
            DrawVerticalLine(isLeft, y, i);
        }

        private void PrintMiddleLine(bool isLeft, int i, int j, Game game)
        {
            if (game.Winner==null)
            {
                Console.ForegroundColor = (ConsoleColor)LineColor.StandartColor;
            }
            else
            {
                Console.ForegroundColor = (ConsoleColor)LineColor.WinnerColor;
            }

            int y = GetCursorLeft(isLeft, i);

            if (isLeft)
            {
                Console.CursorLeft = y + 2;
                Console.WriteLine("|--");
            }
            else
            {
                Console.CursorLeft = y - 2;
                Console.WriteLine("--|");
            }
        }

        private void PrintDownLine(bool isLeft, int i, int j, Game game)
        {
            SetColor(game.Winner, game.SecondTeam);

            int y = GetCursorLeft(isLeft, i);

            DrawVerticalLine(isLeft, y, i);
            DrawHorizontalLine(isLeft, y, game.SecondTeam);

            _maxCursorTop = Math.Max(_maxCursorTop, Console.CursorTop);
        }

        private void DrawHorizontalLine(bool isLeft, int cursorLeft, Team team)
        {
            PrintTeamName(isLeft, cursorLeft, team);
        }

        private void DrawVerticalLine(bool isLeft, int cursorLeft, int i)
        {
            if (isLeft)
            {
                cursorLeft += 2;
            }

            for (int j = 0; j < GetLengthOfBranch(i) / 2; j++)
            {
                Console.CursorLeft = cursorLeft;
                Console.WriteLine("|");
            }
        }

        private void SetColor(Team winner, Team team)
        {
            Console.ForegroundColor = (ConsoleColor)LineColor.StandartColor;

            if (winner != null && winner.Equals(team))
            {
                Console.ForegroundColor = (ConsoleColor)LineColor.WinnerColor;
            }
        }

        private void SetCursorTop(int i, int j)
        {
            Console.CursorTop = j * (GetWidthOfBranch(i) + 1) + GetWidthOfBranch(i - 1) / 2 + _minCursorTop;
        }

        private int GetCursorLeft(bool isleft, int i)
        {
            if (isleft)
            {
                return (_nameLength + 4) * i+_nameLength;
            }
            else
            {
                return _rightBorder - (_nameLength+ 4) * i;
            }
        }

        private void PrintTeamName(bool isleft, int cursorleft, Team team)
        {
            if (isleft)
            {
                Console.CursorLeft = Math.Max(cursorleft - _shift, 0);

                if (team == null)
                    Console.WriteLine($"{_defoltTeamName}--");
                else
                    Console.WriteLine($"{team.Name}--");
            }
            else
            {
                Console.CursorLeft = cursorleft;

                if (team == null)
                    Console.WriteLine($"--{_defoltTeamName}");
                else
                    Console.WriteLine($"--{team.Name}");
            }

            _team.Remove(team);
        }

        private void PrintChampion(int tour, Team winner, GridType type)
        {
            if (winner == null)
                return;

            if (type == GridType.StandardGrid)
                tour++;

            Console.ForegroundColor = (ConsoleColor)LineColor.WinnerColor;
            SetCursorTop(tour - 1, 0);
            Console.CursorLeft = GetCursorLeft(true, tour - 1) - _nameLength + 1;
            Console.WriteLine(winner.Name);
            Console.ForegroundColor = (ConsoleColor)LineColor.StandartColor;
        }

        private int GetWidthOfBranch(int i)
        {
            if (i == 0)
            {
                return _widthOfFirstBranch;
            }

            if (i < 0)
            {
                return 0;
            }

            return GetWidthOfBranch(i - 1) * 2 + 1;
        }

        private int GetLengthOfBranch(int i)
        {
            return GetWidthOfBranch(i) / 2;
        }
    }

    public enum GridType
    {
        StandardGrid,
        DoubleGrid
    }
}
