using System;
using System.Collections.Generic;

namespace FootballTournament
{
    public class TournamentGrid
    {
        private static Tournament _tournament;
        private static int _gridVerticalLength;

        public static void ShowSingleEliminationGrid(Tournament tournament)
        {
            _tournament = tournament;

            Console.Clear();

            if (_tournament.TournamentMode == TournamentMode.SingleElimination)
                Console.WriteLine("TOURNAMENT GRID:\n");
            else
                Console.WriteLine("WINNERS GRID:\n");

            DrawGrid(_tournament.WinnersGrid, Console.CursorTop);
        }

        public static void ShowDoubleEliminationGrid(Tournament tournament)
        {
            ShowSingleEliminationGrid(tournament);
            Console.WriteLine("LOSERS GRID:\n");
            DrawGrid(tournament.LosersGrid, Console.CursorTop);

            if (tournament.IsFinished)
            {
                Console.WriteLine("\nGRAND FINAL:");
                DrawResult(Console.CursorLeft, Console.CursorTop, tournament.GrandFinal);
                Console.WriteLine();
                Console.WriteLine($"\n{tournament.Champion.Name} is a champion!");
            }

        }

        private static void DrawGrid(List<List<Game>> grid, int startY)
        {
            _gridVerticalLength = 0;
            var startX = 0;
            var shiftY = 2;
            var lineLength = 1;

            for (int numberOfStage = 0; numberOfStage < grid.Count; numberOfStage++)
            {
                var stage = grid[numberOfStage];
                var shiftX = GetMaxLength(stage) + 4;

                for (int numberOfGame = 0; numberOfGame < stage.Count; numberOfGame++)
                {
                    var game = stage[numberOfGame];
                    var interval = GetMaxLength(stage) - game.Result().Length;

                    var x = startX + interval;
                    var y = startY + shiftY * numberOfGame;

                    DrawResult(x, y, game);

                    if (numberOfGame % 2 != 0 || numberOfGame != stage.Count - 1)
                        DrawHorizontalLines();

                    if (numberOfGame % 2 == 0 && numberOfGame != stage.Count - 1)
                    {
                        x = Console.CursorLeft - 1;
                        y++;

                        DrawVerticalLines(x, y, lineLength);
                    }

                    if (_gridVerticalLength < y)
                        _gridVerticalLength = y;
                }

                startX += shiftX;
                startY += (int)Math.Pow(2, numberOfStage);
                shiftY *= 2;
                lineLength = lineLength * 2 + 1;
            }

            Console.SetCursorPosition(0, _gridVerticalLength + 3);
        }

        private static void DrawResult(int x, int y, Game game)
        {
            Console.SetCursorPosition(x, y);

            if(!game.IsPlayed)
                Console.Write(game.Result());
            else
            {
                Console.ForegroundColor = ChangeColor(game, game.FirstPlayer);
                Console.Write($"{game.FirstPlayer.Name} ");
                Console.ResetColor();
                Console.Write($"{game.FirstPlayerScore}:{game.SecondPlayerScore}");
                Console.ForegroundColor = ChangeColor(game, game.SecondPlayer);
                Console.Write($" {game.SecondPlayer.Name}");
                Console.ResetColor();
            }
        }

        private static ConsoleColor ChangeColor(Game game, Player player)
        {
            if (player == game.Winner)
                return ConsoleColor.Green;
            else
                return ConsoleColor.Gray;
        }

        private static void DrawHorizontalLines()
        {
            Console.Write(" --");
        }

        private static void DrawVerticalLines(int x, int y, int lineLength)
        {
            for (int currentLength = 1; currentLength <= lineLength; currentLength++)
            {
                Console.SetCursorPosition(x, y);
                Console.Write('|');
                y++;
            }
        }

        private static int GetMaxLength(List<Game> stage)
        {
            int maxLength = -1;

            for (int i = 0; i < stage.Count; i++)
            {
                if (stage[i].Result().Length > maxLength)
                    maxLength = stage[i].Result().Length;
            }

            return maxLength;
        }
    }
}
