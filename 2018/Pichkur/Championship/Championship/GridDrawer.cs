using System;
using System.Collections.Generic;
using System.Linq;

namespace Championship
{
    class GridDrawer
    {
        private int _nameLength;
        private int _shift;
        private int _rightBorder = 50;
        private int _widthOfFirstBranch = 3;
        private int _maxCursorTop = 0;
        private List<Team> _team = new List<Team>();
        private readonly int _minCursorTop=0;

        public GridDrawer(List<Team> teams, int minCursorTop)
        {
            _team = new List<Team>(teams);
            _team = MakeNameOneLength(_team);
            _minCursorTop = minCursorTop;
        }

        public void DrawGrid(TournamentGrid grid, GridType type)
        {
            var allTeams = new List<Team>(_team);
            _rightBorder = 2 * (_nameLength + 3 * (grid.Tours.Count - 1) + 4);

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

            PrintChampion(grid.CountTours + 1, grid.Champion, type);
            Console.CursorTop = _maxCursorTop + 1;
            _team = allTeams;
        }

        private List<Team> MakeNameOneLength(List<Team> names)
        {
            List<Team> answer = new List<Team>();
            _nameLength = names.Max(a => a.Name.Length);
            _shift = _nameLength - 1;

            for (int i = 0; i < names.Count; i++)
            {
                if (names[i].Name.Length < _nameLength)
                {
                    names[i].Name = names[i].Name.PadLeft(_nameLength);
                }

                answer.Add(names[i]);
            }

            return answer;
        }

        private int GetMiddleGame(int countGames)
        {
            return countGames % 2 != 0 ? countGames / 2 + 1 : countGames / 2;
        }

        private void DrawBranch(bool isLeft, int i, int j, Game game)
        {
            if (game.FirstTeam.Name == null)
                return;

            PrintUpLine(isLeft, i, j, game);
            PrintMiddleLine(isLeft, i, j, game);
            PrintDownLine(isLeft, i, j, game);
        }

        private void PrintUpLine(bool isLeft, int i, int j, Game match)
        {
            SetColor(match.Winner, match.FirstTeam.Name);

            int y = GetCursorLeft(isLeft, i);
            SetCursorTop(i, j);

            DrawHorizontalLine(isLeft, y, match.FirstTeam);

            DrawVerticalLine(isLeft, y, i);
        }

        private void PrintMiddleLine(bool isLeft, int i, int j, Game match)
        {
            if (match.Winner==null)
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

        private void PrintDownLine(bool isLeft, int i, int j, Game match)
        {
            SetColor(match.Winner, match.SecondTeam.Name);

            int y = GetCursorLeft(isLeft, i);

            DrawVerticalLine(isLeft, y, i);

            DrawHorizontalLine(isLeft, y, match.SecondTeam);

            _maxCursorTop = Math.Max(_maxCursorTop, Console.CursorTop);
        }

        private void DrawHorizontalLine(bool isLeft, int cursorLeft, Team team)
        {
            if (_team.Contains(team))
            {
                PrintTeamName(isLeft, cursorLeft, team);
            }
            else
            {
                Console.CursorLeft = cursorLeft;
                Console.WriteLine("--");
            }
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

        private void SetColor(string winner, string team)
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
                return _nameLength + 3 * i;
            }
            else
            {
                return _rightBorder - _nameLength - 3 * i;
            }
        }

        private void PrintTeamName(bool isleft, int cursorleft, Team team)
        {
            if (isleft)
            {
                Console.CursorLeft = cursorleft - _shift;
                Console.WriteLine($"{team.Name}--");
            }
            else
            {
                Console.CursorLeft = cursorleft;
                Console.WriteLine($"--{team.Name}");
            }

            _team.Remove(team);
        }

        private void PrintChampion(int tour, string winner, GridType type)
        {
            if (type == GridType.StandardGrid)
                tour++;

            Console.ForegroundColor = (ConsoleColor)LineColor.WinnerColor;
            SetCursorTop(tour - 1, 0);
            Console.CursorLeft = GetCursorLeft(true, tour);
            Console.WriteLine(winner);
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

    public enum LineColor
    {
        StandartColor=ConsoleColor.Gray,
        WinnerColor=ConsoleColor.Green
    }
}
