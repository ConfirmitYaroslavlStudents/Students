using System;

namespace Tournament
{
    public enum DrawType
    {
        Vertical, Horizontal
    }

    public static class Messenger
    {
        private static DrawType _style = DrawType.Vertical;

        public const int MinChars = 2;
        public const int MaxChars = 8;
        public const string PlayMain = "main";
        public const string PlayLosers = "losers";
        public const string PlayFinale = "finale";

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

        public static void Play(TournamentController tournament)
        {
            while (true)
            {
                Display(tournament);
                string command = Console.ReadLine();
                ParseCommand(tournament, command);
            }
        }

        private static void Display(TournamentController tournament)
        {
            Console.Clear();
            Console.WriteLine("Type \"play {type of match} {number of tour} {number of match} {number of winner}\" to enter match results.");
            Console.WriteLine("Type is one of 3 options: \"{0}\", \"{1}\" or \"{2}\". For finale you need only the number of winner.", PlayMain, PlayLosers, PlayFinale);
            Console.WriteLine("Tours and matches are counted from 1. Number of winner is either 1 or 2.");
            Console.WriteLine("Type \"horizontal\" or \"vertical\" to change drawing style.");
            Console.WriteLine("Type \"exit\" to close the program.");
            Console.WriteLine();
            
            if (tournament.DoubleElimination)
            {
                Console.WriteLine("Double Elimination");
            }
            else
            {
                Console.WriteLine("Single Elimination");
            }

            Console.WriteLine();

            if (_style == DrawType.Horizontal)
            {
                HorizontalDrawer.DrawTable(tournament);
            }
            else
            {
                VerticalDrawer.DrawTable(tournament);
            }
            Console.WriteLine("Enter your command:");
        }

        private static void ParseCommand(TournamentController tournament, string command)
        {
            char[] separator = { ' ' };
            string[] words = command.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            string warning = "Invalid command";

            if (words.Length > 0)
            {
                switch (words[0])
                {
                    case "play":
                        {
                            if (words.Length > 1)
                            {
                                ParsePlay(tournament, words);
                            }

                            break;
                        }
                    case "vertical":
                        {
                            _style = DrawType.Vertical;
                            break;
                        }
                    case "horizontal":
                        {
                            _style = DrawType.Horizontal;
                            break;
                        }
                    case "exit":
                        {
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine(warning);
                            Console.ReadKey();
                            break;
                        }
                }
            }
        }

        private static void ParsePlay(TournamentController tournament, string[] words)
        {
            string warning = "Invalid command parameters";

            try
            {
                switch (words[1])
                {
                    case PlayMain:
                        {
                            if (words.Length == 5)
                            {
                                var matchCoords = new MatchInfo(words[2], words[3], words[4]);
                                tournament.PlayGame(matchCoords, false);
                            }
                            else
                            {
                                Console.WriteLine(warning);
                                Console.ReadKey();
                            }
                            break;
                        }
                    case PlayLosers:
                        {
                            if (tournament.DoubleElimination && words.Length == 5)
                            {
                                var matchCoords = new MatchInfo(words[2], words[3], words[4]);
                                tournament.PlayGame(matchCoords, true);
                            }
                            else
                            {
                                Console.WriteLine(warning);
                                Console.ReadKey();
                            }

                            break;
                        }
                    case PlayFinale:
                        {
                            if (tournament.DoubleElimination && words.Length == 3)
                            {
                                tournament.PlayFinale(int.Parse(words[2]) - 1);
                            }
                            else
                            {
                                Console.WriteLine(warning);
                                Console.ReadKey();
                            }

                            break;
                        }
                    default:
                        {
                            Console.WriteLine(warning);
                            Console.ReadKey();
                            break;
                        }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine(warning);
                Console.ReadKey();
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
    }
}
