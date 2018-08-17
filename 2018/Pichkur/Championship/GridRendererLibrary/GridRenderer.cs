using System;
using System.Collections.Generic;
using ChampionshipLibrary;

namespace GridRendererLibrary
{
    public abstract class GridRenderer
    {
        public int NameLength;
        public int Shift;
        public int MaxCursorTop = 0;
        public int MinCursorTop = 0;
        public int WidthOfFirstBranch = 3;
        public string DefaultTeamName = "";
        public List<Team> Teams = new List<Team>();

        public void Render(List<Game> games, int tourIndex)
        {
            for (int gameIndex = 0; gameIndex < games.Count; gameIndex++)
            {
                var currentGame = games[gameIndex];
                DrawBranch(tourIndex, gameIndex, currentGame);
            }
        }

        public void SetColor(Team winner, Team team)
        {
            Console.ForegroundColor = (ConsoleColor)LineColor.StandartColor;

            if (winner != null && winner.Equals(team))
            {
                Console.ForegroundColor = (ConsoleColor)LineColor.WinnerColor;
            }
        }

        public void SetCursorTop(int tourIndex, int gameIndex)
        {
            Console.CursorTop = gameIndex * (GetWidthOfBranch(tourIndex) + 1) + GetWidthOfBranch(tourIndex - 1) / 2 + MinCursorTop;
        }

        public int GetLengthOfBranch(int tourIndex)
        {
            return GetWidthOfBranch(tourIndex) / 2;
        }

        public int GetWidthOfBranch(int tourIndex)
        {
            if (tourIndex == 0)
            {
                return WidthOfFirstBranch;
            }

            if (tourIndex < 0)
            {
                return 0;
            }

            return GetWidthOfBranch(tourIndex - 1) * 2 + 1;
        }

        public void DrawBranch(int tourIndex, int gameIndex, Game game)
        {
            if (game == null)
                return;

            PrintUpLine(tourIndex, gameIndex, game);
            PrintMiddleLine(tourIndex, game);
            PrintDownLine(tourIndex, gameIndex, game);
        }

        public void PrintUpLine(int tourIndex, int gameIndex, Game game)
        {
            SetColor(game.Winner, game.FirstTeam);

            int y = GetCursorLeft(tourIndex);
            SetCursorTop(tourIndex, gameIndex);

            DrawHorizontalLine(y, game.FirstTeam);
            DrawVerticalLine(y, tourIndex);
        }

        public void PrintDownLine(int tourIndex, int gameIndex, Game game)
        {
            SetColor(game.Winner, game.SecondTeam);

            int y = GetCursorLeft(tourIndex);

            DrawVerticalLine(y, tourIndex);
            DrawHorizontalLine(y, game.SecondTeam);

            MaxCursorTop = Math.Max(MaxCursorTop, Console.CursorTop);
        }

        public void PrintChampion(int tour, Team winner)
        {
            if (winner == null)
                return;

            Console.ForegroundColor = (ConsoleColor)LineColor.WinnerColor;
            SetCursorTop(tour, 0);
            Console.CursorLeft = GetCursorLeft(tour) - NameLength + 1;
            Console.WriteLine(winner.Name);
            Console.ForegroundColor = (ConsoleColor)LineColor.StandartColor;
        }

        public abstract void PrintMiddleLine(int tourIndex,Game game);

        public abstract void DrawHorizontalLine(int cursorLeft, Team team);

        public abstract void DrawVerticalLine(int cursorLeft, int tourIndex);

        public abstract int GetCursorLeft(int tourIndex);
    }
}
