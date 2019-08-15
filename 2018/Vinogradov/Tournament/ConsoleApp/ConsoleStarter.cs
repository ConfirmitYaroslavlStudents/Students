using System;
using System.Collections.Generic;

namespace TournamentLibrary
{
    public class ConsoleStarter
    {

        public ConsoleStarter()
        {
        }

        public Tournament TryLoadTournament()
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

        public bool ReadDoubleEliminationFlag()
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

        public int ReadNumberOfPlayers()
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

        public string[] ReadNames(int numberOfPlayers)
        {
            List<string> names = new List<string>();
            string openingMessage = String.Format("Please, input players' nicknames.\r\nThey must contain only letters or digits and be {0} - {1} characters long.\r\nAll names must be different.", NameValidator.MinChars, NameValidator.MaxChars);
            Console.WriteLine(openingMessage);
            Console.WriteLine();

            while (names.Count < numberOfPlayers)
            {
                string newName = Console.ReadLine();

                if (NameValidator.Validate(newName, names))
                {
                    names.Add(newName);
                }

                Console.Clear();
                Console.WriteLine(openingMessage);

                if (names.Count > 0)
                {
                    Console.WriteLine("Current players:");

                    foreach (var name in names)
                    {
                        Console.WriteLine("\t{0}", name);
                    }
                }
                Console.WriteLine();
            }

            return names.ToArray();
        }
    }
}
