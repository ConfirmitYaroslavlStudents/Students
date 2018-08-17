using System;

namespace TournamentLibrary
{
    class Program
    {
        private static Drawer _drawer = new VerticalDrawer();
        private const string _playCommand = "win";
        private const string _verticalCommand = "vertical";
        private const string _horizontalCommand = "horizontal";
        private const string _exitCommand = "exit";


        static void Main(string[] args)
        {
            Starter starter = new ConsoleStarter();
            var tournament = starter.TryLoadTournament();

            if (tournament == null)
            {
                bool isDoubleElimination = starter.ReadDoubleEliminationFlag();
                int numberOfPlayers = starter.ReadNumberOfPlayers();
                string[] playerNames = starter.ReadNames(numberOfPlayers);
                tournament = new Tournament(playerNames, isDoubleElimination);
                SaveController.Save(tournament);
            }
            Play(tournament);
        }

        private static void Play(Tournament tournament)
        {
            while (true)
            {
                Display(tournament);
                string command = Console.ReadLine();
                ParseCommand(tournament, command);
                SaveController.Save(tournament);
            }
        }

        private static void Display(Tournament tournament)
        {
            Console.Clear();
            Console.WriteLine("Type \"{0} [name of player]\" to enter match results. The game, where this player currently is, will be played with them as a winner.", _playCommand);
            Console.WriteLine("Type \"{0}\" or \"{1}\" to change drawing style.", _verticalCommand, _horizontalCommand);
            Console.WriteLine("Type \"{0}\" to close the program.", _exitCommand);
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
            _drawer.DrawTable(tournament);
            Console.WriteLine("Enter your command:");
        }

        private static void ParseCommand(Tournament tournament, string command)
        {
            char[] separator = { ' ' };
            string[] words = command.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            string warning = "Invalid command";

            if (words.Length > 0)
            {
                switch (words[0])
                {
                    case "win":
                        {
                            if (words.Length > 1)
                            {
                                ParseGame(tournament, words[1], warning);
                            }
                            else
                            {
                                Console.WriteLine(warning);
                                Console.ReadKey();
                            }

                            break;
                        }
                    case "vertical":
                        {
                            _drawer = new VerticalDrawer();
                            break;
                        }
                    case "horizontal":
                        {
                            _drawer = new HorizontalDrawer();
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

        private static void ParseGame(Tournament tournament, string name, string warning)
        {
            if (tournament.PlayersCoords.ContainsKey(name))
            {
                try
                {
                    tournament.PlayGame(name);
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
        }
    }
}
