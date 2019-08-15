using System;
using System.Collections.Generic;

namespace TournamentLibrary
{
    public class VerticalDrawer : Drawer
    {
        public VerticalDrawer()
        {

        }

        public override void DrawTable(Tournament tournament)
        {
            List<string>[] mainGames = GetGridLines(tournament.Main);
            List<string>[] losersGames = null;

            if (tournament.DoubleElimination)
            {
                losersGames = GetGridLines(tournament.Losers);

                if (losersGames[0].Count > mainGames[0].Count)
                {
                    for (int i = 0; i < mainGames.Length; i++)
                    {
                        mainGames[i].Add(mainGames[i][mainGames[i].Count - 1]);
                    }
                }

                FillLines(mainGames, 0, mainGames.Length - 1);
                string name = string.Empty;

                if (tournament.Champion != null)
                {
                    name = tournament.Champion.Name;
                }

                mainGames[mainGames.Length - 1].Add(name);
                FillLines(losersGames, 0, losersGames.Length);
            }

            PrintGrids(mainGames, losersGames, tournament);
        }

        private void PrintGrids(List<string>[] mainGames, List<string>[] losersGames, Tournament tournament)
        {
            for (int k = 0; k < mainGames[0].Count; k++)
            {
                if (k > 0)
                {
                    PrintLines(mainGames, losersGames, k);
                }

                for (int i = 0; i < mainGames.Length; i++)
                {
                    PrintCell(tournament, false, mainGames, i, k);
                }

                if (tournament.DoubleElimination)
                {
                    for (int i = 0; i < losersGames.Length; i++)
                    {
                        PrintCell(tournament, true, losersGames, i, k);
                    }
                }

                Console.WriteLine();

                if (k < mainGames[0].Count - 1)
                {
                    PrintLines(mainGames, losersGames, k);
                }
            }
        }

        private void PrintCell(Tournament tournament, bool isLoserGrid, List<string>[] lines, int line, int column)
        {
            string cell = lines[line][column];

            if (cell != null)
            {
                if (line == lines.Length - 1 || GreenLight(tournament, isLoserGrid, line, column))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.Write(AlignName(cell, NameValidator.MaxChars));
                Console.ResetColor();
            }
            else
            {
                Console.Write(AlignName(string.Empty, NameValidator.MaxChars));
            }
        }

        private void PrintLines(List<string>[] mainCells, List<string>[] losersCells, int tour)
        {
            for (int i = 0; i < mainCells.Length; i++)
            {
                var cell = mainCells[i][tour];

                if (cell != null)
                {
                    Console.Write(AlignName("|", NameValidator.MaxChars));
                }
                else
                {
                    Console.Write(AlignName(string.Empty, NameValidator.MaxChars));
                }
            }

            if (losersCells != null)
            {
                for (int i = 0; i < losersCells.Length; i++)
                {
                    var cell = losersCells[i][tour];

                    if (cell != null)
                    {
                        Console.Write(AlignName("|", NameValidator.MaxChars));
                    }
                    else
                    {
                        Console.Write(AlignName(string.Empty, NameValidator.MaxChars));
                    }
                }
            }

            Console.WriteLine();
        }
    }
}
