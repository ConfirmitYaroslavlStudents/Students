using System;
using System.Collections.Generic;
using TournamentLibrary;

namespace FootballTournament
{
    public class DoubleSidedTournamentGrid : TournamentGrid
    {
        private static SingleEliminationTournament _tournament;
        private static List<List<Game>> _virtualGrid;
        private static List<List<Game>> _leftSidedGrid;
        private static List<List<Game>> _rightSidedGrid;
        private static int _currentStage;
        private static int _gamesOnCurrentStage;
        private static int _sideLength;
        private static int _lastVerticalLineLength;
        private static int _gridVerticalLength = 0;
        private static Player _unknownPlayer = new Player("?");

        public static void Show(SingleEliminationTournament tournament)
        {
            Console.Clear();

            if (IsDegreeOfTwo(tournament.CountOfPlayers))
            {
                Console.WriteLine("TOURNAMENT GRID:\n");

                _tournament = tournament;
                _currentStage = tournament.WinnersGrid.Count - 1;
                _gamesOnCurrentStage = tournament.WinnersGrid[_currentStage].Count;

                CalculateStages(tournament.WinnersGrid);
                SplitGames();

                DrawLeftSidedGrid();
                DrawRightSidedGrid();
            }
            else
                Console.WriteLine("Double-sided grid available only for count of players which is degree of two!");
        }

        private static bool IsDegreeOfTwo(int value)
        {
            return value != 0 && (value & (value - 1)) == 0;
        }

        private static void CalculateStages(List<List<Game>> grid)
        {
            _virtualGrid = new List<List<Game>>();

            for (int i = 0; i < grid.Count; i++)
            {
                _virtualGrid.Add(new List<Game>());

                foreach (var game in grid[i])
                    _virtualGrid[i].Add(game);
            }

            while (_gamesOnCurrentStage > 1)
            {
                _virtualGrid.Add(new List<Game>());
                _currentStage++;
                _gamesOnCurrentStage = (int)Math.Ceiling((decimal)_gamesOnCurrentStage / 2);

                for (int i = 0; i < _gamesOnCurrentStage; i++)
                {
                    _virtualGrid[_currentStage].Add(new Game(_unknownPlayer, _unknownPlayer));
                }
            }
        }

        private static void SplitGames()
        {
            _leftSidedGrid = new List<List<Game>>();
            _rightSidedGrid = new List<List<Game>>();

            for (int numberOfStage = 0; numberOfStage < _virtualGrid.Count; numberOfStage++)
            {
                var stage = _virtualGrid[numberOfStage];
                var middleOfStage = (int)Math.Ceiling((decimal)stage.Count / 2);

                _leftSidedGrid.Add(new List<Game>());
                _rightSidedGrid.Add(new List<Game>());

                for (int numberOfGame = 0; numberOfGame < middleOfStage; numberOfGame++)
                {
                    _leftSidedGrid[numberOfStage].Add(stage[numberOfGame]);
                }

                for (int numberOfGame = middleOfStage; numberOfGame < stage.Count; numberOfGame++)
                {
                    _rightSidedGrid[numberOfStage].Add(stage[numberOfGame]);
                }
            }
        }

        private static void DrawLeftSidedGrid()
        {
            _gridVerticalLength = 0;
            var startX = 0;
            var startY = 2;
            var shiftY = 2;
            var lineLength = 1;

            for (int numberOfStage = 0; numberOfStage < _leftSidedGrid.Count; numberOfStage++)
            {
                var stage = _leftSidedGrid[numberOfStage];
                var shiftX = GetMaxLength(stage) + 4;

                for (int numberOfGame = 0; numberOfGame < stage.Count; numberOfGame++)
                {
                    var game = stage[numberOfGame];
                    var interval = GetMaxLength(stage) - game.Result().Length;

                    var x = startX + interval;
                    var y = startY + shiftY * numberOfGame;

                    DrawResult(x, y, game);

                    if (numberOfStage != _leftSidedGrid.Count - 1)
                        DrawLeftHorizontalLines();

                    if (numberOfGame % 2 == 0 && numberOfStage != _leftSidedGrid.Count - 1)
                    {
                        x = Console.CursorLeft - 1;
                        y++;

                        if (numberOfGame != stage.Count - 1)
                            DrawVerticalLines(x, y, lineLength);
                        else
                        {
                            _lastVerticalLineLength = (int)Math.Ceiling((decimal)lineLength / 2);
                            DrawVerticalLines(x, y, _lastVerticalLineLength);
                        }
                    }

                    if (_gridVerticalLength < y)
                        _gridVerticalLength = y;

                    _sideLength = x + GetMaxLength(stage) + 1;
                }

                startX += shiftX;
                startY += (int)Math.Pow(2, numberOfStage);
                shiftY *= 2;
                lineLength = lineLength * 2 + 1;
            }
        }

        private static void DrawRightSidedGrid()
        {
            _gridVerticalLength = 0;
            var startX = _sideLength + 3;
            var startY = Console.CursorTop - _lastVerticalLineLength;
            var shiftY = (int)Math.Pow(2, _rightSidedGrid.Count - 1);
            var lineLength = _lastVerticalLineLength * 2 - 1;
            var finishedStages = 0;

            for (int numberOfStage = _rightSidedGrid.Count - 2; numberOfStage >= 0; numberOfStage--)
            {
                var stage = _rightSidedGrid[numberOfStage];
                var shiftX = GetMaxLength(stage) + 4;

                for (int numberOfGame = 0; numberOfGame < stage.Count; numberOfGame++)
                {
                    var game = stage[numberOfGame];
                    var x = startX;
                    var y = startY + shiftY * numberOfGame;

                    DrawResult(x, y, game);

                    x = Console.CursorLeft - game.Result().Length - 3;

                    DrawRightHorizontalLines(x, y);

                    if (numberOfGame % 2 == 0 && numberOfStage != _rightSidedGrid.Count - 1)
                    {
                        x = Console.CursorLeft - 3;
                        y++;

                        if (numberOfStage != _rightSidedGrid.Count - 2)
                            DrawVerticalLines(x, y, lineLength);
                        else
                            DrawVerticalLines(x, y, _lastVerticalLineLength);
                    }

                    if (_gridVerticalLength < y)
                        _gridVerticalLength = y;
                }

                startX += shiftX;
                startY -= (int)Math.Pow(2, numberOfStage - 1);
                shiftY /= 2;
                lineLength /= 2;
                finishedStages++;
            }

            _sideLength = Console.CursorLeft;
            Console.SetCursorPosition(0, _gridVerticalLength + 3);
        }


        private static void DrawLeftHorizontalLines()
        {
            Console.Write(" --");
        }

        private static void DrawRightHorizontalLines(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("-- ");
        }
    }
}
