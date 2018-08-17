using System;

namespace TournamentLibrary
{
    public class ConsoleStarter : Starter
    {

        public ConsoleStarter()
        {
        }

        public override Tournament TryLoadTournament()
        {
            const string doLoad = "yes";
            const string dontLoad = "no";
            string openingMessage = string.Format("Do you want to try to load a saved game? It's stored in the Type \"{0}\" or \"{1}\".", doLoad, dontLoad);
            Console.WriteLine(openingMessage);
            Console.WriteLine();

            while (true)
            {
                string input = Console.ReadLine();

                switch (input)
                {
                    case doLoad:
                        {
                            Console.Clear();
                            var tournament = SaveController.Load();

                            if (tournament == null)
                            {
                                Console.WriteLine("Unable to load the tournament");
                            }

                            return tournament;
                        }
                    case dontLoad:
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

        public override bool ReadDoubleEliminationFlag()
        {
            const string singleType = "single";
            const string doubleType = "double";
            string openingMessage = string.Format("Please, input game type.\r\nInput \"{0}\" or \"{1}\" for single or double elimination respectively.", singleType, doubleType);
            Console.WriteLine(openingMessage);
            Console.WriteLine();

            while (true)
            {
                string input = Console.ReadLine();

                switch (input)
                {
                    case singleType:
                        {
                            Console.Clear();
                            return false;
                        }
                    case doubleType:
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

        public override int ReadNumberOfPlayers()
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

        public override string[] ReadNames(int numberOfPlayers)
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
    }
}
