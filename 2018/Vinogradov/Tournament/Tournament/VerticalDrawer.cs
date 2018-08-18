using System;
using System.Text;

namespace Tournament
{
    public static class VerticalDrawer
    {
        public static void DrawTable(TournamentController tournament)
        {
            int mainGridWidth = (int)Math.Pow(2, tournament.Main.Matches.Length) * (Messenger.MaxChars + 2);
            int columnSize = 1;
            int tourCount = 0;
            columnSize = 1;

            for (tourCount = 0; tourCount < tournament.Main.Matches.Length; tourCount++)
            {
                int lineLength = columnSize * (Messenger.MaxChars + 2) * tournament.Main.Matches[tourCount].Length * 2;
                PrintMatches(tournament, columnSize, tourCount, mainGridWidth - lineLength);
                Console.WriteLine();
                int additionalLines = 0;

                if (tournament.DoubleElimination)
                {
                    additionalLines = tournament.Losers.Matches[tourCount].Length;
                }

                DrawLines(tournament.Main.Matches[tourCount].Length, columnSize, additionalLines, mainGridWidth - lineLength);
                columnSize *= 2;
            }

            string winnerName = PrintMainWinner(tournament, columnSize);

            if (tournament.DoubleElimination)
            {
                PrintEndForDE(tournament, columnSize, tourCount, winnerName);
            }

            Console.WriteLine();
        }

        private static void PrintMatches(TournamentController tournament, int columnSize, int tourCount, int fillerLength)
        {
            for (int i = 0; i < tournament.Main.Matches[tourCount].Length; i++)
            {
                PrintMatch(tournament.Players, tournament.Main.Matches[tourCount][i], columnSize);
            }

            FillLine(fillerLength);

            if (tournament.DoubleElimination)
            {
                for (int i = 0; i < tournament.Losers.Matches[tourCount].Length; i++)
                {
                    PrintMatch(tournament.Players, tournament.Losers.Matches[tourCount][i], columnSize);
                }
            }
        }

        private static void FillLine(int neededWidth)
        {
            for (int i = 0; i < neededWidth; i++)
            {
                Console.Write(' ');
            }
        }

        private static void PrintMatch(string[] names, Match currentMatch, int columnSize)
        {
            for (int j = 0; j < 2; j++)
            {
                int playerNumber = currentMatch.Opponents[j];
                string playerName = "";

                if (playerNumber > -1)
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

        private static string PrintMainWinner(TournamentController tournament, int columnSize)
        {
            string winnerName = "";

            if (tournament.Main.Winner > -1)
            {
                winnerName = tournament.Players[tournament.Main.Winner];
            }

            if (!(tournament.DoubleElimination ^ tournament.Champion == tournament.Main.Winner))
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.Write(CenterName(winnerName, (Messenger.MaxChars + 2) * columnSize));
            Console.ResetColor();
            return winnerName;
        }

        private static void PrintEndForDE(TournamentController tournament, int columnSize, int tour, string mainGridWinner)
        {
            int losersColumnWidthMultiplier = 1;

            if (tour < tournament.Losers.Matches.Length)
            {
                losersColumnWidthMultiplier++;
                PrintMatch(tournament.Players, tournament.Losers.Matches[tour][0], columnSize);
                Console.WriteLine();
                Console.Write(CenterName("|", (Messenger.MaxChars + 2) * columnSize));
                Console.Write(CenterName("|", (Messenger.MaxChars + 2) * columnSize));
                Console.Write(CenterName("|", (Messenger.MaxChars + 2) * columnSize));
                Console.WriteLine();
                Console.Write(CenterName("|", (Messenger.MaxChars + 2) * columnSize));
                Console.Write(CenterName("|", (Messenger.MaxChars + 2) * columnSize * losersColumnWidthMultiplier));
                Console.WriteLine();

                if (tournament.Champion == tournament.Main.Winner)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.Write(CenterName(mainGridWinner, (Messenger.MaxChars + 2) * columnSize));
                Console.ResetColor();
            }

            string winnerName = "";

            if (tournament.Losers.Winner > -1)
            {
                winnerName = tournament.Players[tournament.Losers.Winner];
            }

            if (tournament.Champion == tournament.Losers.Winner)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.Write(CenterName(winnerName, (Messenger.MaxChars + 2) * columnSize * losersColumnWidthMultiplier));
            Console.ResetColor();
            Console.WriteLine();
            Console.Write(CenterName("|", (Messenger.MaxChars + 2) * columnSize));
            Console.Write(CenterName("|", (Messenger.MaxChars + 2) * columnSize * losersColumnWidthMultiplier));
            Console.WriteLine();
            Console.WriteLine(CenterName("|", (Messenger.MaxChars + 2) * columnSize * (losersColumnWidthMultiplier + 1)));
            winnerName = "";

            if (tournament.Champion > -1)
            {
                winnerName = tournament.Players[tournament.Champion];
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(CenterName(winnerName, (Messenger.MaxChars + 2) * columnSize * (losersColumnWidthMultiplier + 1)));
            Console.ResetColor();
        }

        private static void DrawLines(int matchesInTour, int columnSize, int matchesInLosersTour, int fillerLength)
        {
            for (int i = 0; i < matchesInTour * 2; i++)
            {
                Console.Write(CenterName("|", (Messenger.MaxChars + 2) * columnSize));
            }

            FillLine(fillerLength);

            for (int i = 0; i < matchesInLosersTour * 2; i++)
            {
                Console.Write(CenterName("|", (Messenger.MaxChars + 2) * columnSize));
            }

            Console.WriteLine();

            for (int i = 0; i < matchesInTour; i++)
            {
                Console.Write(CenterName("|", (Messenger.MaxChars + 2) * columnSize * 2));
            }

            FillLine(fillerLength);

            for (int i = 0; i < matchesInLosersTour; i++)
            {
                Console.Write(CenterName("|", (Messenger.MaxChars + 2) * columnSize * 2));
            }

            Console.WriteLine();
        }
    }
}
