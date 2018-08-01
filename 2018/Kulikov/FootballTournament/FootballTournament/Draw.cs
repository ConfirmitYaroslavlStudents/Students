using System;
using System.Collections.Generic;
using System.Linq;

namespace DrawGrid
{
    class Draw
    {
        private int _nameLength;
        private int _shift;
        private int _rightBorder = 50;
        private int _emptyLineCount = 5;
        private List<Team> _team = new List<Team>();

        public Draw(List<Team> teams)
        {
            _team = new List<Team>(teams);
            _team= MakeNameOneLength(_team);
        }

        public void DrawGrid(TournamentGrid grid, bool type)
        {
            _rightBorder = 2 * (_nameLength * 2 + 3 * (grid.Tours.Count - 1) + 1);

            int lastTour;

            if (type)
            {
                lastTour = grid.Tours.Count;
            }
            else
            {
                lastTour = grid.Tours.Count - 1;
            }

            for (int i = 0; i < lastTour; i++)
            {
                int lastgame = grid.Tours[i].Games.Count / 2;

                if (grid.Tours[i].Games.Count % 2 != 0)
                    lastgame++;

                for (int j = 0; j < grid.Tours[i].Games.Count; j++)
                {
                    if (j < lastgame || type)
                        DrawBranch(true, i, j, grid.Tours[i].Games[j]);
                    else
                        DrawBranch(false, i, j - lastgame, grid.Tours[i].Games[j]);
                }
            }
            if (type)
                PrintChampion(grid.Tours.Count + 1, grid.Tours[grid.Tours.Count - 1].Games[0].Winner);
            else
                PrintChampion(grid.Tours.Count, grid.Tours[grid.Tours.Count - 1].Games[0].Winner);
        }

        private void DrawBranch(bool isLeft,int i,int j,Game game)
        {
            if (game.FirstTeam.Name == null)
                return;

            PrintUpLine(isLeft, i, j,game);
            PrintMiddleLine(isLeft, i, j);
            PrintDownLine(isLeft, i, j,game);
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

        public void DrawHorizontalLine(bool isLeft,int cursorLeft,Team team)
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

        public void DrawVerticalLine(bool isLeft,int cursorLeft, int i)
        {
            if (isLeft)
                cursorLeft += 2;

            for (int i1 = 0; i1 < GetLengthOfBranch(i) / 2; i1++)
            {
                Console.CursorLeft = cursorLeft;
                Console.WriteLine("|");
            }
        }

        public void SetColor(string winner,string team)
        {
            if (winner==null)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            if (winner.Equals(team))
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void SetCursorTop(int i,int j)
        {
            Console.CursorTop = j * (GetWidthOfBranch(i) + 1) + GetWidthOfBranch(i - 1) / 2;
        }

        public int GetCursorLeft(bool isleft,int i)
        {
            if (isleft)
                return _nameLength + 3 * i;
            else
                return _rightBorder - _nameLength - 3 * i;
        }

        public void PrintTeamName(bool isleft,int cursorleft,Team team)
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

        public void PrintChampion(int tour,string winner)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            SetCursorTop(tour - 1, 0);
            Console.CursorLeft = GetCursorLeft(true, tour);
            Console.WriteLine(winner);
            Console.WriteLine();
            SetCursorTop(tour, 0);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void PrintUpLine(bool left, int i, int j, Game match)
        {
            SetColor(match.Winner, match.FirstTeam.Name);

            int y = GetCursorLeft(left, i);
            SetCursorTop(i, j);

            DrawHorizontalLine(left, y, match.FirstTeam);

            DrawVerticalLine(left, y, i);
        }

        public void PrintMiddleLine(bool left,int i, int j)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            int y =GetCursorLeft(left,i);
            
            if (left)
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

        public void PrintDownLine(bool left,int i, int j, Game match)
        {
            SetColor(match.Winner, match.SecondTeam.Name);

            int y = GetCursorLeft(left, i);

            DrawVerticalLine(left, y, i);

            DrawHorizontalLine(left, y, match.SecondTeam);
        }

        private int GetWidthOfBranch(int i)
        {
            if (i ==0 )
                return _emptyLineCount;
            if (i < 0)
                return 0;
            return GetWidthOfBranch(i-1)*2+1;
        }

        private int GetLengthOfBranch(int i)
        {
            return GetWidthOfBranch(i) / 2 ;
        }
    }
}
