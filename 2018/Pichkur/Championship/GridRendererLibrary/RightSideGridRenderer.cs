using System;
using System.Collections.Generic;
using System.Linq;

namespace Championship
{
    class RightSideGridRenderer : GridRenderer
    {
        public int RightBorder = 0;

        public RightSideGridRenderer(List<Team> teams, int minCursorTop, int countTours)
        {
            Teams = new List<Team>(teams);
            MinCursorTop = minCursorTop;
            NameLength = teams.Max(a => a.Name.Length);
            Shift = NameLength - 1;
            var oneTourLength = NameLength + 4;
            RightBorder = 2 * oneTourLength * countTours + 1;
            DefaultTeamName = DefaultTeamName.PadLeft(NameLength);
        }

        public override void DrawHorizontalLine(int cursorLeft, Team team)
        {
            Console.CursorLeft = cursorLeft;

            if (team == null)
                Console.WriteLine($"--{DefaultTeamName}");
            else
                Console.WriteLine($"--{team.Name}");

            Teams.Remove(team);
        }

        public override void DrawVerticalLine(int cursorLeft, int i)
        {
            for (int j = 0; j < GetLengthOfBranch(i) / 2; j++)
            {
                Console.CursorLeft = cursorLeft;
                Console.WriteLine("|");
            }
        }

        public override int GetCursorLeft(int tourIndex)
        {
            return RightBorder - (NameLength + 4) * tourIndex;
        }

        public override void PrintMiddleLine(int tourIndex, Game game)
        {
            int y = GetCursorLeft(tourIndex);
            Console.CursorLeft = y - 2;
            Console.WriteLine("--|");
        }
    }
}
