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
            int currentGridPointer = 0;
            for (int i = 1; i < horizontalGrid.Count; i++)
            {
                Console.WriteLine();
                int currentPositionInLine = 0;
                if (horizontalGrid[i].Length == 0)
                {
                    currentGridPointer = i + 1;
                    continue;
                }
                while (currentPositionInLine < horizontalGrid[i].Length)
                {
                    if (!Char.IsLetterOrDigit(horizontalGrid[i][currentPositionInLine]))
                    {
                        Console.Write(horizontalGrid[i][currentPositionInLine]);
                        currentPositionInLine++;
                    }
                    else
                    {
                        string name = "";
                        while (currentPositionInLine < horizontalGrid[i].Length &&
                               Char.IsLetterOrDigit(horizontalGrid[i][currentPositionInLine]))
                        {
                            name += horizontalGrid[i][currentPositionInLine];
                            currentPositionInLine++;
                        }
                        bool isLastName = false;
                        for (int j = currentPositionInLine; j < horizontalGrid[i].Length; j++)
                        {
                            if ((horizontalGrid[i][j] == '|' || char.IsLetterOrDigit(horizontalGrid[i][j]) || horizontalGrid[i][j] == '-')
                                && j < horizontalGrid[i].Length - 2)
                                break;
                            if (j == horizontalGrid[i].Length - 2)
                            {
                                isLastName = true;
                                Console.Write(name);
                                break;
                            }
                        }

                        if (isLastName)
                            continue;

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
                }
            }
            Console.WriteLine();
        }

        public static void PrintVerticalGrid(List<string> verticalGrid)
        {
            Console.Clear();
            for (int i = 0; i < verticalGrid.Count - 2; i++)
            {
                if (i % 3 == 0)
                {
                    for (int j = 0; j < verticalGrid[i].Length; j++)
                    {
                        if (verticalGrid[i][j] == ' ')
                        {
                            Console.Write(" ");
                        }
                        else
                        {
                            string firstName = "";
                            string secondName = "";
                            int firstNamePosition = j;
                            while (j < verticalGrid[i].Length && verticalGrid[i][j] != ' ')
                            {
                                firstName += verticalGrid[i][j];
                                j++;
                            }

                            if (j >= verticalGrid[i].Length)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(firstName);
                                Console.ResetColor();
                                break;
                            }
                            if (i + 3 < verticalGrid.Count && verticalGrid[i + 3]
                                    .Substring(firstNamePosition, firstName.Length).Contains(firstName))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(firstName);
                                Console.ResetColor();
                                j--;
                                continue;
                            }

                            while (j < verticalGrid[i].Length && verticalGrid[i][j] == ' ')
                                j++;
                            int secondNamePosition = j;

                            int secondNamePositionSaver = j;

                            while (j < verticalGrid[i].Length && verticalGrid[i][j] != ' ')
                            {
                                secondName += verticalGrid[i][j];
                                j++;
                            }

                            if (i + 3 < verticalGrid.Count && verticalGrid[i + 3].Length <=
                                secondNamePosition + secondName.Length - firstNamePosition)
                                secondNamePosition = verticalGrid[i + 3].Length - secondName.Length;

                            if (i + 3 < verticalGrid.Count && verticalGrid[i + 3]
                                    .Substring(firstNamePosition,
                                        secondNamePosition + secondName.Length - firstNamePosition)
                                    .Contains(firstName))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            else
                                Console.ForegroundColor = ConsoleColor.Red;

                            secondNamePosition = secondNamePositionSaver;
                            if (i + 3 >= verticalGrid.Count)
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
                            j--;
                        }
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(verticalGrid[i]);
                }
            }
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

