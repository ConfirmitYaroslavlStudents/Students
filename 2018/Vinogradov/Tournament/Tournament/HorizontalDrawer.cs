using System;
using System.Collections.Generic;
using System.Text;

namespace Tournament
{
    public static class HorizontalDrawer
    {
        private const int EmptyCellCode = -2;

        public static void DrawTable(TournamentController tournament)
        {
            List<int>[] mainLines = GetGridLines(tournament.Main);
            List<int>[] losersLines = null;

            if (tournament.DoubleElimination)
            {
                losersLines = GetGridLines(tournament.Losers);

                if (losersLines.Length > mainLines.Length)
                {
                    mainLines[(mainLines.Length / 2) - 1].Add(tournament.Main.Winner);
                    mainLines[mainLines.Length - 1].Add(EmptyCellCode);
                }

                mainLines[mainLines.Length - 1].Add(EmptyCellCode);
                mainLines[mainLines.Length - 1].Add(tournament.Champion);
            }

            PrintGrid(mainLines, tournament, false);

            if (tournament.DoubleElimination)
            {
                PrintGrid(losersLines, tournament, true);
            }
        }

        private static List<int>[] GetGridLines(Grid grid)
        {
            int tableHeight = (int)Math.Pow(2, grid.Matches.Length + 1);
            List<int>[] lines = new List<int>[tableHeight];

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = new List<int>();
            }

            int gapSize = 1;
            int currentLine = 0;

            for (int k = 0; k < grid.Matches.Length; k++)
            {
                for (int i = 0; i < grid.Matches[k].Length; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        FillLines(lines, currentLine, gapSize - 1);
                        currentLine += gapSize - 1;
                        lines[currentLine].Add(grid.Matches[k][i].Opponents[j]);
                        currentLine++;
                        FillLines(lines, currentLine, gapSize);
                        currentLine += gapSize;
                    }
                }

                FillLines(lines, currentLine, tableHeight - currentLine);
                currentLine = 0;
                gapSize *= 2;
            }

            lines[(tableHeight / 2) - 1].Add(grid.Winner);
            return lines;
        }

        private static void FillLines(List<int>[] lines, int position, int gapSize)
        {
            for (int i = 0; i < gapSize; i++)
            {
                lines[position + i].Add(EmptyCellCode);
            }
        }

        private static void PrintGrid(List<int>[] lines, TournamentController tournament, bool loserGrid)
        {
            for (int k = 0; k < lines.Length; k++)
            {
                for (int i = 0; i < lines[k].Count; i++)
                {
                    PrintCell(tournament, loserGrid, lines, k, i);
                }

                Console.WriteLine();
            }
        }

        private static bool GreenLight(TournamentController tournament, bool loserGrid, int line, int column)
        {
            Grid grid = tournament.Main;

            if (loserGrid)
            {
                grid = tournament.Losers;
            }

            if (column < grid.Matches.Length)
            {
                int matchNumber = line / (int)Math.Pow(2, column + 2);
                int playerInPair = (line % (int)Math.Pow(2, column + 2)) / (int)Math.Pow(2, column + 1);

                if (grid.Matches[column][matchNumber].Winner == playerInPair)
                {
                    return true;
                }
            }
            else if (!(tournament.DoubleElimination ^ grid.Winner == tournament.Champion))
            {
                return true;
            }

            return false;
        }

        private static void PrintCell(TournamentController tournament, bool loserGrid, List<int>[] lines, int line, int column)
        {
            int cell = lines[line][column];

            if (cell != EmptyCellCode)
            {
                if (column > 0)
                {
                    Console.Write('—');
                }
                else
                {
                    Console.Write(' ');
                }

                string playerName = "";

                if (cell > -1)
                {
                    playerName = tournament.Players[cell];
                }

                if (line == lines.Length - 1 || GreenLight(tournament, loserGrid, line, column))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.Write(CenterName(playerName, Messenger.MaxChars));
                Console.ResetColor();
                int gridLength = tournament.Main.Matches.Length;

                if (loserGrid)
                {
                    gridLength = tournament.Losers.Matches.Length;
                }

                if (column < gridLength)
                {
                    Console.Write('—');
                }
                else
                {
                    Console.Write(' ');
                }
            }
            else
            {
                Console.Write(CenterName("", (Messenger.MaxChars + 2)));
            }
        }

        private static string CenterName(string name, int size)
        {
            StringBuilder refactored = new StringBuilder(size);
            int firstGap = (size - name.Length) / 2;

            for (int i = 0; i < firstGap; i++)
            {
                refactored.Append(' ');
            }

            refactored.Append(name);
            int secondGap = size - name.Length - firstGap;

            for (int i = 0; i < secondGap; i++)
            {
                refactored.Append(' ');
            }

            return refactored.ToString();
        }
    }
}
