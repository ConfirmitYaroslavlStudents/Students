using System;
using System.Collections.Generic;

namespace Football_League
{
    public static partial class ConsoleWorker
    {
        private static string MakeName(string line, int j)
        {
            string name = "";
            while (j < line.Length && char.IsLetterOrDigit(line[j]))
            {
                name += line[j];
                j++;
            }

            return name;
        }
        private static void PrintColoredName(int currentGridPointer, int currentPositionInLine, string name, List<string> horizontalGrid, bool isLastName, string currentHorizontalGrid)
        {
            if (isLastName)
            {
                int longestLineLength = 0;
                foreach (var line in horizontalGrid)
                    if (line.Length > longestLineLength && line != currentHorizontalGrid)
                        longestLineLength = line.Length;

                if (longestLineLength > currentHorizontalGrid.Length)
                    Console.ForegroundColor = ConsoleColor.Red;
                if (longestLineLength < currentHorizontalGrid.Length)
                    Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(name);
                Console.ResetColor();
                return;
            }

            bool isNameFoundInNextRound = false;
            for (int j = currentGridPointer; j < horizontalGrid.Count; j++)
            {
                if (horizontalGrid[j] == "")
                    break;

                isNameFoundInNextRound = horizontalGrid[j].Substring(currentPositionInLine + 1,
                    horizontalGrid[j].Length - (currentPositionInLine + 1)).Contains(name);
                if (isNameFoundInNextRound)
                    break;
            }
            Console.ForegroundColor = isNameFoundInNextRound ? ConsoleColor.Green : ConsoleColor.Red;
            Console.Write(name);
            Console.ResetColor();
        }
        private static bool CheckIfLastNameInLine(string currentHorizontalGrid, int currentPositionInLine)
        {
            bool isLastName = false;
            for (int j = currentPositionInLine; j < currentHorizontalGrid.Length; j++)
            {
                if (j >= currentHorizontalGrid.Length - 2)
                {
                    isLastName = true;
                    break;
                }
                if (char.IsLetterOrDigit(currentHorizontalGrid[j]) || ((currentHorizontalGrid[j] == '|')
                                                                       && j < currentHorizontalGrid.Length - 2))
                    break;
            }
            return isLastName;
        }
        private static void PrintFirstAndSecondNames(int currentLineIndex, List<string> verticalGrid, int firstNamePosition, string firstName, string secondName, int secondNamePosition, int secondNamePositionSaver)
        {
            if (currentLineIndex + 3 < verticalGrid.Count && verticalGrid[currentLineIndex + 3]
                    .Substring(firstNamePosition,
                        secondNamePosition + secondName.Length - firstNamePosition)
                    .Contains(firstName))
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
                Console.ForegroundColor = ConsoleColor.Red;
            if (currentLineIndex + 3 >= verticalGrid.Count)
                Console.ResetColor();

            Console.Write(firstName);

            while (firstNamePosition + firstName.Length != secondNamePositionSaver)
            {
                Console.Write(" ");
                firstNamePosition++;
            }

            switch (Console.ForegroundColor)
            {
                case ConsoleColor.Red:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;

                case ConsoleColor.Green:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }

            Console.Write(secondName);
            Console.ResetColor();
        }
        private static void PrintVerticalGridAutoWinner(string firstName, List<string> verticalGrid, int currentLinePosition)
        {
            if (currentLinePosition + 3 >= verticalGrid.Count ||
                verticalGrid[currentLinePosition + 3].Contains(firstName))
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;
            if (currentLinePosition + 3 >= verticalGrid.Count)
                Console.ResetColor();
            Console.Write(firstName);
            Console.ResetColor();
        }
    }
}
