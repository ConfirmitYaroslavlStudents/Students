using System;
using System.Collections.Generic;
using FootballLeagueClassLibrary.Structure;

namespace Football_League.ConsoleManagement
{
    public static partial class ConsoleWorker
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
            if (match.PlayerTwo == null)
                return 1;
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
            int currentTreeStartIndex = 1;
            for (int currentLineIndex = 1; currentLineIndex < horizontalGrid.Count; currentLineIndex++)
            {
                if (horizontalGrid[currentLineIndex].Length == 0)
                {
                    currentTreeStartIndex = currentLineIndex + 1;
                    continue;
                }
                PrintSingleHorizontalLine(currentTreeStartIndex, horizontalGrid[currentLineIndex], horizontalGrid);
            }
            Console.WriteLine();
        }

        public static void PrintSingleHorizontalLine(int currentTreeStartIndex, string currentHorizontalGrid, List<string> horizontalGrid)
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
                    string name = ConsoleWorker.MakeName(currentHorizontalGrid, currentPositionInLine);
                    currentPositionInLine += name.Length;

                    bool isLastName = ConsoleWorker.CheckIfLastNameInLine(currentHorizontalGrid, currentPositionInLine);
                    ConsoleWorker.PrintColoredName(currentTreeStartIndex, currentPositionInLine, name, horizontalGrid,isLastName, currentHorizontalGrid);
                }
            }
        }
        public static void PrintVerticalGrid(List<string> verticalGrid)
        {
            Console.Clear();
            for (int i = 0; i < verticalGrid.Count - 2; i++)
            {
                PrintSingleVerticalLine(i, verticalGrid);
            }
        }
        public static void PrintSingleVerticalLine(int currentLineIndex, List<string> verticalGrid)
        {
            if (currentLineIndex % 3 != 0)
                Console.WriteLine(verticalGrid[currentLineIndex]);
            else
            {
                for (int j = 0; j < verticalGrid[currentLineIndex].Length; j++)
                {
                    if (verticalGrid[currentLineIndex][j] == ' ')
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        int firstNamePosition = j;
                        string firstName = ConsoleWorker.MakeName(verticalGrid[currentLineIndex], j);
                        j += firstName.Length;

                        bool onlyOnePlayerToPrint = CheckIfOnlyOnePlayerToPrint(j, currentLineIndex, verticalGrid, firstName, firstNamePosition);
                        if (onlyOnePlayerToPrint)
                        {
                            j--;
                            ConsoleWorker.PrintVerticalGridAutoWinner(firstName, verticalGrid,currentLineIndex);
                            continue;
                        }
                        
                        PrintTwoPlayers(currentLineIndex, verticalGrid, ref j, firstNamePosition, firstName);                        
                    }
                }
                Console.WriteLine();
            }
        }

        private static bool CheckIfOnlyOnePlayerToPrint(int currentPositionInLine, int currentLineIndex, List<string> verticalGrid, string firstName, int firstNamePosition)
        {
            bool isLastNameInLine = currentLineIndex + 3 >= verticalGrid.Count || currentPositionInLine >= verticalGrid[currentLineIndex + 3].Length;

            if (isLastNameInLine)
                return true;

            bool isFinalWinner = false;
            if (currentLineIndex + 3 < verticalGrid.Count &&
                firstName.Length + firstNamePosition < verticalGrid[currentLineIndex + 3].Length)
            {
                string nextRoundWinners = verticalGrid[currentLineIndex + 3];
                isFinalWinner = nextRoundWinners.Substring(firstNamePosition, firstName.Length).Contains(firstName);
            }
            if (isFinalWinner)
                return true;

            return false;
        }
        private static void PrintTwoPlayers(int currentLineIndex, List<string> verticalGrid, ref int j, int firstNamePosition, string firstName)
        {
            while (j < verticalGrid[currentLineIndex].Length && verticalGrid[currentLineIndex][j] == ' ')
                j++;

            int secondNamePosition = j;
            int secondNamePositionSaver = j;

            string secondName = ConsoleWorker.MakeName(verticalGrid[currentLineIndex], j);
            j += secondName.Length;

            if (currentLineIndex + 3 < verticalGrid.Count && verticalGrid[currentLineIndex + 3].Length <=
                secondNamePosition + secondName.Length - firstNamePosition)
                secondNamePosition = verticalGrid[currentLineIndex + 3].Length - secondName.Length;

            ConsoleWorker.PrintFirstAndSecondNames(currentLineIndex, verticalGrid, firstNamePosition, firstName, secondName, secondNamePosition, secondNamePositionSaver);
            j--;
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

