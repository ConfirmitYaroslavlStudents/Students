using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
                "\n2.Choose winners\n3.Display scoreboard\n4.Exit");
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
        public static int ChooseMatchWinner(Match match)
        {
            Console.WriteLine("Type number of winner for the match:");
            Console.WriteLine("1. {0}\n2. {1}\n", match.PlayerOne, match.PlayerTwo);
            return int.Parse(Console.ReadLine());
        }
        public static void PrintScoreboard(List<string> matchResultsDisplayGrid1,
            List<string> matchResultsDisplayGrid2)
        {
            for (int i = 0; i < matchResultsDisplayGrid1.Count; i++)
            {
                if (i % 3 == 0 && i + 3 < matchResultsDisplayGrid1.Count)
                {
                    PrintColoredNames(matchResultsDisplayGrid1, i);

                    if (i >= 3 && i < matchResultsDisplayGrid2.Count)
                    {
                        Console.Write("          ");
                        PrintColoredNames(matchResultsDisplayGrid2, i - 3);
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.Write(matchResultsDisplayGrid1[i]);
                    if (i - 3 >= 0 && i - 3 < matchResultsDisplayGrid2.Count)
                        Console.Write("          " + matchResultsDisplayGrid2[i - 3]);
                    Console.WriteLine();
                }
            }

            Console.WriteLine("To continue press Enter...");
                while (Console.ReadKey().Key != ConsoleKey.Enter)
                    continue;
                Console.Clear();
        }
        private static void PrintColoredNames(List<string> matchResultsDisplayGrid, int i)
        {
            char[] seps =
            {
                ' '
            };
            foreach (var name in matchResultsDisplayGrid[i]
                                    .Split(seps, StringSplitOptions.RemoveEmptyEntries))
            {
                if (Regex.IsMatch(matchResultsDisplayGrid[i + 3], @"" + name + @"[\s]?"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(name + " ");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(name + " ");
                    Console.ResetColor();
                }
            }
        }
    }
}
