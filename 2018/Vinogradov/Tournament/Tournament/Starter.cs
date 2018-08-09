using System;

namespace Tournament
{
    public static class Starter
    {
        public const int MinChars = 2;
        public const int MaxChars = 8;

        public static Tournament TryLoadTournament()
        {
            string openingMessage = "Do you want to try to load a saved game? Type \"yes\" or \"no\".";
            Console.WriteLine(openingMessage);
            Console.WriteLine();

            while (true)
            {
                string input = Console.ReadLine();

                switch (input)
                {
                    case "yes":
                        {
                            Console.Clear();
                            var tournament = SaveController.Load();

                            if (tournament == null)
                            {
                                Console.WriteLine("Could not load the tournament");
                            }

                            return tournament;
                        }
                    case "no":
                        {
                            Console.Clear();
                            return null;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine(openingMessage);
                            Console.WriteLine();
                            break;
                        }
                }
            }
        }

        public static bool ReadDoubleEliminationFlag()
        {
            string openingMessage = "Please, input game type.\r\nInput \"single\" or \"double\" for single or double elimination respectively.";
            Console.WriteLine(openingMessage);
            Console.WriteLine();

            while (true)
            {
                string input = Console.ReadLine();

                switch (input)
                {
                    case "single":
                        {
                            Console.Clear();
                            return false;
                        }
                    case "double":
                        {
                            Console.Clear();
                            return true;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine(openingMessage);
                            Console.WriteLine();
                            break;
                        }
                }
            }
        }

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
    }
}
