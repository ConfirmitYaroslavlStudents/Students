using System;

namespace Tournament
{
    public static class Messenger
    {
        public static int MinChars = 2;
        public static int MaxChars = 8;

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
            string openingMessage = String.Format("Please, input players' nicknames.\r\nThey must contain only letters or digits and be {0} - {1} characters long.\r\nAll names must be different.", MinChars, MaxChars);
            Console.WriteLine(openingMessage);
            Console.WriteLine();

            int validNames = 0;
            while (validNames < players.Length)
            {
                string newName = Console.ReadLine();

                if (NameValidation(newName, players, validNames))
                        {
                    players[validNames] = newName;
                    validNames++;
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

        private static bool NameValidation(string newName, string[] names, int validNames)
        {
            if (newName.Length < MinChars && newName.Length > MaxChars)
            {
                return false;
            }

            foreach (char symbol in newName)
            {
                if (!char.IsLetterOrDigit(symbol))
                {
                    return false;
                }
            }

            for (int i = 0; i < validNames; i++)
            {
                if (names[i] == newName)
                {
                    return false;
                }
            }

            return true;
        }

        public static void Play(TournamentController tournament)
        {
            while (true)
            {
                Display(tournament);
                string command = Console.ReadLine();
                Execute(tournament, command);
            }
        }

        private static void Execute(TournamentController tournament, string command)
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
                        char[] separator = { ' ' };
                        string warning = "Invalid command";
                        string[] words = command.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        if (words.Length == 4 && words[0] == "play")
                        {
                            try
                            {
                                var matchCoords = new MatchInfo(words[1], words[2], words[3]);
                                tournament.PlayGame(matchCoords);
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

            Drawer.DrawVerticalTable(tournament);

            Console.WriteLine("Enter your command:");
        }

       
    }
}
