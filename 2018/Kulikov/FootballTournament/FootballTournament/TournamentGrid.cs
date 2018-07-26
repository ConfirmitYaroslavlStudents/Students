using System;
using System.Collections.Generic;

namespace FootballTournament
{
    public class TournamentGrid
    {
        private static int _gridVerticalLength;

        public static void ShowSingleEliminationGrid(List<List<Game>> winnersGrid, string tournamentMode)
        {
            Console.Clear();

            if (tournamentMode == "SE")
                Console.WriteLine("TOURNAMENT GRID:\n");
            else
                Console.WriteLine("WINNERS GRID:\n");

            DrawGrid(winnersGrid);
        }

        private static void DrawGrid(List<List<Game>> grid)
        {
            var start_Y = Console.CursorTop;
            DrawResults(grid, start_Y);
            DrawVerticalLines(grid, start_Y + 1);
            Console.SetCursorPosition(0, _gridVerticalLength + 3);
        }

        private static void DrawResults(List<List<Game>> grid, int start_Y)
        {
            _gridVerticalLength = 0;
            var start_X = 0;
            var shift_Y = 2;

            for (int i = 0; i < grid.Count; i++)
            {
                var stage = grid[i];
                var shift_X = GetMaxLength(stage) + 4;

                if (i == grid.Count - 1 && stage.Count == 1)
                {
                    Player checkLoser = grid[i][0].DetectLoser();

                    if (checkLoser == null)
                        break;
                }

                for (int j = 0; j < stage.Count; j++)
                {
                    var game = stage[j];
                    var interval = GetMaxLength(stage) - game.ToString().Length;

                    var x = start_X + interval;
                    var y = start_Y + shift_Y * j;

                    Console.SetCursorPosition(x, y);
                    Console.Write(game.ToString());

                    if (stage.Count > 1)
                        Console.Write(" --");

                    if (_gridVerticalLength < y)
                        _gridVerticalLength = y;
                }

                start_X += shift_X;
                start_Y += (int)Math.Pow(2, i);
                shift_Y *= 2;
            }
        }

        private static void DrawVerticalLines(List<List<Game>> grid, int start_Y)
        {
            var start_X = -2;
            var shift_Y = 4;
            var lineLength = 1;

            for (int i = 0; i < grid.Count; i++)
            {
                var stage = grid[i];
                var y = start_Y;
                var shift_X = GetMaxLength(stage) + 4;
                start_X += shift_X;

                for (int j = 0; j < stage.Count / 2; j++)
                {
                    for (int k = 1; k <= lineLength; k++)
                    {
                        Console.SetCursorPosition(start_X, y);
                        Console.Write('|');
                        if (k != lineLength)
                            y++;
                    }

                    y += shift_Y;
                }

                start_Y += (int)Math.Pow(2, i);
                shift_Y += (i + 1) * 2;
                lineLength = lineLength * 2 + 1;
            }
        }

        public static void ShowDoubleEliminationGrid(List<List<Game>> winnersGrid, List<List<Game>> losersGrid)
        {
            ShowSingleEliminationGrid(winnersGrid, "DE");
            Console.WriteLine("LOSERS GRID:\n");
            DrawGrid(losersGrid);
        }

        private static int GetMaxLength(List<Game> stage)
        {
            int maxLength = -1;

            for (int i = 0; i < stage.Count; i++)
            {
                if (stage[i].ToString().Length > maxLength)
                    maxLength = stage[i].ToString().Length;
            }

            return maxLength;
        }
    }
}
