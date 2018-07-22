using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament
{
    public class Messenger
    {
        public static int minChars = 2;
        public static int maxChars = 8;

        public static int ReadNumberOfPlayers()
        {
            string openingMessage = "Please, input number of players.\r\nThere must be at least 2 for the tournament to start.";
            Console.WriteLine(openingMessage);
            Console.WriteLine();
            int number;

            while ((!int.TryParse(Console.ReadLine(), out number)) || number < 2)
            {
                Console.Clear();
                Console.WriteLine(openingMessage);
                Console.WriteLine();
            }

            Console.Clear();
            return number;
        }

        public static string[] ReadNames(int numberOfPlayers)
        {
            string[] players = new string[numberOfPlayers];
            string openingMessage = String.Format("Please, input players' nicknames.\r\nThey must contain only letters or digits and be {0} - {1} characters long.\r\nAll names must be different.", minChars, maxChars);
            Console.WriteLine(openingMessage);
            Console.WriteLine();

            int validNames = 0;
            while (validNames < players.Length)
            {
                string newName = Console.ReadLine();
                if (newName.Length >= minChars && newName.Length <= maxChars)
                {
                    bool allCharsValid = true;
                    foreach (char symbol in newName)
                    {
                        if (!char.IsLetterOrDigit(symbol))
                        {
                            allCharsValid = false;
                            break;
                        }
                    }
                    if (allCharsValid)
                    {
                        bool notMatching = true;
                        for (int i = 0; i < validNames; i++)
                        {
                            if (players[i] == newName)
                            {
                                notMatching = false;
                                break;
                            }
                        }
                        if (notMatching)
                        {
                            players[validNames] = newName;
                            validNames++;
                        }
                    }
                }

                Console.Clear();
                Console.WriteLine(openingMessage);
                if (validNames > 0)
                {
                    Console.WriteLine("Current players:");
                    for (int i = 0; i < validNames; i++)
                    {
                        Console.WriteLine("\t{0}", players[i]);
                    }
                }
                Console.WriteLine();
            }

            return players;
        }

        public static void Play(TournamentController tournament)
        {
            while (true)
            {
                Display(tournament);
                string command = Console.ReadLine();
                Execute(ref tournament, command);
            }
        }

        private static void Execute(ref TournamentController tournament, string command)
        {
            switch (command)
            {
                case "exit":
                    {
                        Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        string warning = "Invalid command";
                        string[] words = command.Split(' ');
                        if (words.Length == 4 && words[0] == "play")
                        {
                            try
                            {
                                int tour = int.Parse(words[1]);
                                int match = int.Parse(words[2]);
                                int winner = int.Parse(words[3]);
                                tournament.PlayGame(new MatchInfo(tour - 1, match - 1, winner - 1));
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine(warning);
                                Console.ReadKey();
                            }
                            catch (ArgumentException e)
                            {
                                Console.WriteLine(e.Message);
                                Console.ReadKey();
                            }
                            catch (InvalidOperationException e)
                            {
                                Console.WriteLine(e.Message);
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.WriteLine(warning);
                            Console.ReadKey();
                        }
                        break;
                    }
            }
        }

        private static void Display(TournamentController tournament)
        {
            Console.Clear();
            Console.WriteLine("Type \"play {number of tour} {number of match} {number of winner}\" to enter match results.\r\nTours and matches are counted from 1. Number of winner is either 1 or 2.");
            Console.WriteLine("Type \"exit\" to close the program.");
            Console.WriteLine();

            DrawTable(tournament);

            Console.WriteLine("Enter your command:");
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

        private static void DrawTable(TournamentController tournament)
        {
            string playerName;

            for (int k = 0; k < tournament.Matches.Length; k++)
            {
                for (int i = 0; i < tournament.Matches[k].Length; i++)
                {
                    var currentMatch = tournament.Matches[k][i];

                    for (int j = 0; j < 2; j++)
                    {
                        int playerNumber = currentMatch.Opponents[j];
                        playerName = "";

                        if (playerNumber >= 0)
                        {
                            playerName = tournament.Players[playerNumber];
                        }

                        if (currentMatch.Winner == j)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }

                        Console.Write(CenterName(playerName, (maxChars + 2) * (int)Math.Pow(2, k)));
                        Console.ResetColor();
                    }

                }

                Console.WriteLine();

                for (int i = 0; i < tournament.Matches[k].Length * 2; i++)
                {
                    Console.Write(CenterName("|", (maxChars + 2) * (int)Math.Pow(2, k)));
                }

                Console.WriteLine();

                for (int i = 0; i < tournament.Matches[k].Length; i++)
                {
                    Console.Write(CenterName("|", (maxChars + 2) * (int)Math.Pow(2, k + 1)));
                }

                Console.WriteLine();
            }

            playerName = "";

            if (tournament.Champion >= 0)
            {
                playerName = tournament.Players[tournament.Champion];
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(CenterName(playerName, (maxChars + 2) * (int)Math.Pow(2, tournament.Matches.Length)));
            Console.ResetColor();
        }
    }
}
