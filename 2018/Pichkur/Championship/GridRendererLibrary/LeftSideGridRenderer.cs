using System;
using System.Collections.Generic;
using System.Linq;
using ChampionshipLibrary;

namespace GridRendererLibrary
{
    public class LeftSideGridRenderer : GridRenderer
    {
        public LeftSideGridRenderer(List<Team> teams, int minCursorTop)
        {
            Teams = new List<Team>(teams);
            MinCursorTop = minCursorTop;
            NameLength = teams.Max(a => a.Name.Length);
            Shift = NameLength - 1;
            DefaultTeamName = DefaultTeamName.PadLeft(NameLength);
        }

        public override void DrawHorizontalLine(int cursorLeft, Team team)
        {
            Console.CursorLeft = Math.Max(cursorLeft - Shift, 0);

            if (team == null)
                Console.WriteLine($"{DefaultTeamName}--");
            else
                Console.WriteLine($"{team.Name}--");

            Teams.Remove(team);
        }

        public override void DrawVerticalLine(int cursorLeft, int tourIndex)
        {
            cursorLeft += 2;

            for (int j = 0; j < GetLengthOfBranch(tourIndex) / 2; j++)
            {
                Console.CursorLeft = cursorLeft;
                Console.WriteLine("|");
            }
        }

        public override int GetCursorLeft(int tourIndex)
        {
            return (NameLength + 4) * tourIndex + NameLength;
        }

        public override void PrintMiddleLine(int tourIndex, Game game)
        {
            int y = GetCursorLeft(tourIndex);
            Console.CursorLeft = y + 2;
            Console.WriteLine("|--");
        }
    }
}
