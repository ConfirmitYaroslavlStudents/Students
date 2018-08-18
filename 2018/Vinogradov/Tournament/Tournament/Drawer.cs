using System;
using System.Text;

namespace Tournament
{
    public static class Drawer
    {
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

        public static void DrawVerticalTable(TournamentController tournament)
        {
            for (int k = 0; k < tournament.Matches.Length; k++)
            {
                int columnSize = (int)Math.Pow(2, k);

                for (int i = 0; i < tournament.Matches[k].Length; i++)
                {
                    PrintMatchHorizontal(tournament.Players, tournament.Matches[k][i], columnSize);
                }

                Console.WriteLine();
                DrawVerticalLines(tournament.Matches[k].Length, columnSize);
            }

            string winnerName = "";

            if (tournament.Champion >= 0)
            {
                winnerName = tournament.Players[tournament.Champion];
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(CenterName(winnerName, (Messenger.MaxChars + 2) * (int)Math.Pow(2, tournament.Matches.Length)));
            Console.ResetColor();
        }

        private static void PrintMatchHorizontal(string[] names, Match currentMatch, int columnSize)
        {
            for (int j = 0; j < 2; j++)
            {
                int playerNumber = currentMatch.Opponents[j];
                string playerName = "";

                if (playerNumber >= 0)
                {
                    playerName = names[playerNumber];
                }

                if (currentMatch.Winner == j)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.Write(CenterName(playerName, (Messenger.MaxChars + 2) * columnSize));
                Console.ResetColor();
            }
        }

        private static void DrawVerticalLines(int matchesInTour, int columnSize)
        {
            for (int i = 0; i < matchesInTour * 2; i++)
            {
                Console.Write(CenterName("|", (Messenger.MaxChars + 2) * columnSize));
            }

            Console.WriteLine();

            for (int i = 0; i < matchesInTour; i++)
            {
                Console.Write(CenterName("|", (Messenger.MaxChars + 2) * columnSize * 2));
            }

            Console.WriteLine();
        }
    }
}
