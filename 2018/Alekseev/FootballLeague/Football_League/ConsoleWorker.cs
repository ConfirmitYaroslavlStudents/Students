using System;
using System.Collections.Generic;

namespace Football_League
{
    public static class ConsoleWorker
    {
        public static int ChooseLeagueType()
        {
            Console.WriteLine("Choose League Type:\n1. Single Elumination\n2. Double Elumination");
            return int.Parse(Console.ReadLine());
        }
        public static void Menu()
        {
            Console.WriteLine(
                "Welcome to Football League scoreboard Simulator 1.0\nChoose an option\n1.Create new players league" +
                "\n2.Choose winners\n3.Display scoreboard\n4.Load\n5.Exit");
        }
        public static string MenuChoice()
        {
            var choice = Console.ReadLine();
            Console.Clear();
            return choice;
        }
        public static void OnePlayerLeft()
        {
            Console.WriteLine("Competition's over! Either start a new league or watch the scoreboard!\n" +
                              "Or you may exit the program.\nTo return to menu press Enter...");
        }
        public static void NoPlayersLeft()
        {
            Console.WriteLine("No players yet! Start a league first!\nTo return to menu press Enter...");
            while (Console.ReadKey().Key != ConsoleKey.Enter)
                Console.Clear();
        }
        public static void IncorrectMenuChoice()
        {
            Console.Clear();
            Console.WriteLine("Please, press the appropriate button according to the menu!");
        }
        public static int GetNumberOfPlayers()
        {
            Console.WriteLine("Type number of players: ");
            return int.Parse(Console.ReadLine());
        }
        public static string GetPlayerName()
        {
            Console.WriteLine("Type new contestant's name");
            return Console.ReadLine();
        }

        public static int ChooseStyle()
        {
            Console.WriteLine("Type the number of the prefered respresentation style");
            Console.WriteLine("1. Vertical\n2. Horizontal");
            int res = int.Parse(Console.ReadLine());
            Console.Clear();
            return res;
        }
        public static int ChooseMatchWinner(Match match)
        {
            Console.WriteLine("Type number of winner for the match:");
            Console.WriteLine($"1. {match.PlayerOne.Name}\n2. {match.PlayerTwo.Name}\n");
            return int.Parse(Console.ReadLine());
        }

        public static int ChooseDrawingType()
        {
            Console.WriteLine("Type number of preferred drawing style:\n1. Vertical\n2. Horizontal");
            return int.Parse(Console.ReadLine());
        }

        public static void PrintHorizontalGrid(List<string> horizontalGrid)
        {
            Console.Clear();
            int currentGridPointer = 1;
            for (int i = 1; i < horizontalGrid.Count; i++)
            {
                if (horizontalGrid[i].Length == 0)
                {
                    currentGridPointer = i + 1;
                    continue;
                }
                PrintSingleHorizontalLine(ref currentGridPointer, horizontalGrid[i], horizontalGrid);
            }
            Console.WriteLine();
        }

        public static void PrintSingleHorizontalLine(ref int currentGridPointer, string currentHorizontalGrid, List<string> horizontalGrid)
        {
            Console.WriteLine();
            int currentPositionInLine = 0;

            while (currentPositionInLine < currentHorizontalGrid.Length)
            {
                if (!Char.IsLetterOrDigit(currentHorizontalGrid[currentPositionInLine]))
                {
                    Console.Write(currentHorizontalGrid[currentPositionInLine]);
                    currentPositionInLine++;
                }

                else
                {
                    string name = MakeName(currentHorizontalGrid, ref currentPositionInLine);

                    bool isLastName = CheckIfLastNameInLine(currentHorizontalGrid, currentPositionInLine, name);
                    if (isLastName)
                        continue;

                     PrintColoredName(currentGridPointer, currentPositionInLine, name, horizontalGrid);
                }
            }
        }

        private static void PrintColoredName(int currentGridPointer, int currentPositionInLine, string name, List<string> horizontalGrid)
        {
            bool isFound = false;
            for (int j = currentGridPointer; j < horizontalGrid.Count; j++)
            {
                if (horizontalGrid[j] == "")
                {
                    break;
                }
                if (horizontalGrid[j].Substring(currentPositionInLine + 1,
                    horizontalGrid[j].Length - (currentPositionInLine + 1)).Contains(name))
                {
                    isFound = true;
                    break;
                }
            }
            Console.ForegroundColor = isFound ? ConsoleColor.Green : ConsoleColor.Red;

            Console.Write(name);
            Console.ResetColor();
        }

        private static bool CheckIfLastNameInLine(string currentHorizontalGrid, int currentPositionInLine, string name)
        {
            bool isLastName = false;
            for (int j = currentPositionInLine; j < currentHorizontalGrid.Length; j++)
            {
                if (j >= currentHorizontalGrid.Length - 2)
                {
                    isLastName = true;
                    Console.Write(name);
                    break;
                }
                if (char.IsLetterOrDigit(currentHorizontalGrid[j]))
                    break;
                if(currentHorizontalGrid[j] == '-')
                    continue;
                if ((currentHorizontalGrid[j] == '|')
                    && j < currentHorizontalGrid.Length - 2)
                    break;
            }
            return isLastName;
        }

        private static string MakeName(string currentHorizontalGrid, ref int currentPositionInLine)
        {
            string name = "";
            while (currentPositionInLine < currentHorizontalGrid.Length &&
                                       Char.IsLetterOrDigit(currentHorizontalGrid[currentPositionInLine]))
            {
                name += currentHorizontalGrid[currentPositionInLine];
                currentPositionInLine++;
            }

            return name;
        }

        public static void PrintVerticalGrid(List<string> verticalGrid)
        {
            Console.Clear();
            for (int i = 0; i < verticalGrid.Count - 2; i++)
            {
                PrintSingleVerticalLine(i, verticalGrid);
            }
        }

        public static void PrintSingleVerticalLine(int currentLine, List<string> verticalGrid)
        {
            if (currentLine % 3 != 0)
                Console.WriteLine(verticalGrid[currentLine]);
            else
            {
                for (int j = 0; j < verticalGrid[currentLine].Length; j++)
                {
                    if (verticalGrid[currentLine][j] == ' ')
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        int firstNamePosition = j;
                        string firstName = MakeFirstName(verticalGrid[currentLine], ref j);
                        string secondName = "";

                        if (j >= verticalGrid[currentLine].Length)
                        {
                            PrintVerticalGridAutoWinner(firstName);
                            break;
                        }

                        if (currentLine + 3 < verticalGrid.Count && verticalGrid[currentLine + 3]
                                .Substring(firstNamePosition, firstName.Length).Contains(firstName))
                        {
                            PrintVerticalGridAutoWinner(firstName);
                            j--;
                            continue;
                        }

                        while (j < verticalGrid[currentLine].Length && verticalGrid[currentLine][j] == ' ')
                            j++;

                        int secondNamePosition = j;
                        int secondNamePositionSaver = j;

                        secondName = MakeSecondName(verticalGrid[currentLine], ref j);

                        if (currentLine + 3 < verticalGrid.Count && verticalGrid[currentLine + 3].Length <=
                            secondNamePosition + secondName.Length - firstNamePosition)
                            secondNamePosition = verticalGrid[currentLine + 3].Length - secondName.Length;

                        PrintFirstAndSecodNames(currentLine, verticalGrid, firstNamePosition, firstName, secondName, secondNamePosition, secondNamePositionSaver);
                        j--;
                    }
                }
                Console.WriteLine();
            }
        }

        private static void PrintFirstAndSecodNames(int currentLine, List<string> verticalGrid,  int firstNamePosition, string firstName, string secondName, int secondNamePosition, int secondNamePositionSaver)
        {
            if (currentLine + 3 < verticalGrid.Count && verticalGrid[currentLine + 3]
                    .Substring(firstNamePosition,
                        secondNamePosition + secondName.Length - firstNamePosition)
                    .Contains(firstName))
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
                Console.ForegroundColor = ConsoleColor.Red;

            secondNamePosition = secondNamePositionSaver;
            if (currentLine + 3 >= verticalGrid.Count)
                Console.ResetColor();

            Console.Write(firstName);
            while (firstNamePosition + firstName.Length != secondNamePosition)
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

        private static string MakeSecondName(string line, ref int j)
        {
            string secondName = "";
            while (j < line.Length && line[j] != ' ')
            {
                secondName += line[j];
                j++;
            }

            return secondName;
        }

        private static string MakeFirstName(string line, ref int j)
        {
            string firstName = "";
            while (j < line.Length && line[j] != ' ')
            {
                firstName += line[j];
                j++;
            }

            return firstName;
        }

        private static void PrintVerticalGridAutoWinner(string firstName)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(firstName);
            Console.ResetColor();
        }
        public static int SaveProcessQuestion()
        {
            Console.Clear();
            Console.WriteLine("Save Process?\n1. Yes\n2. No");
            return int.Parse(Console.ReadLine());
        }

        public static void Saved()
        {
            Console.WriteLine("Session saved!");
        }

        public static void Loaded()
        {
            Console.WriteLine("Session loaded!");
        }

        public static void FileNotFoundError()
        {
            Console.Clear();
            Console.WriteLine("Cannot locate your save file for this type of tournament! Please start a new tournament to create a save file");
        }
    }
}

