using System;
using System.Collections.Generic;

namespace TournamentLibrary
{
    public class HorizontalDrawer : Drawer
    {
        public HorizontalDrawer()
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
                mainGames[mainGames.Length - 1].Add(tournament.Champion);
                FillLines(losersGames, 0, losersGames.Length);
            }

            PrintGrid(mainGames, tournament, false);

            if (tournament.DoubleElimination)
            {
                PrintGrid(losersGames, tournament, true);
            }
        }

        private void PrintGrid(List<string>[] lines, Tournament tournament, bool isLoserGrid)
        {
            for (int k = 0; k < lines.Length; k++)
            {
                for (int i = 0; i < lines[k].Count; i++)
                {
                    PrintCell(tournament, isLoserGrid, lines, k, i);
                }

                Console.WriteLine();
            }
        }

        private void PrintCell(Tournament tournament, bool isLoserGrid, List<string>[] lines, int line, int column)
        {
            string cell = lines[line][column];

            if (cell != null)
            {
                if (column > 0)
                {
                    Console.Write('—');
                }
                else
                {
                    Console.Write(' ');
                }

                if (line == lines.Length - 1 || GreenLight(tournament, isLoserGrid, line, column))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.Write(CenterName(cell, Starter.MaxChars));
                Console.ResetColor();
                int gridLength = tournament.Main.Matches.Length;

                if (isLoserGrid)
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
                Console.Write(CenterName(string.Empty, (Starter.MaxChars + 2)));
            }
        }
    }
}
