using System;
using System.Collections.Generic;
using System.Threading;
using Championship;
using MenuForConsole;

namespace ConsoleChampionship
{
    class Program
    {
        private static Tournament _championship;
        private static int _maxLengthPlayerName;

        static void Main()
        {
            _championship = new Tournament();
            var mainMenu = new List<MenuItem>
            {
                new MenuItem(Start, "Start"),
                new MenuItem(MenuAddPlayers, "Add players")
            };

            var mnMenu1 = new Menu(mainMenu, "Championship");
            mnMenu1.Start();
        }

        private static void MenuAddPlayers()
        {
            var addPlayerMenu = new List<MenuItem>
            {
                new MenuItem(AddPlayer, "Add player")
            };

            var apMenu1 = new Menu(addPlayerMenu, "Championship");
            apMenu1.Start();
        }

        private static void AddPlayer()
        {
            Console.Write("Write player name: ");
            var name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Empty name player.");
                Thread.Sleep(1000);
                return;
            }

            if (name.Length > _maxLengthPlayerName)
            {
                _maxLengthPlayerName = name.Length;
            }
            _championship.Players.Add(name);
        }

        private static void Start()
        {
            if (_championship.Players.Count == 0)
            {
                Console.WriteLine("First add players, please.");
                Thread.Sleep(1000);
                return;
            }

            CreateGame();

            var sumMenu = new List<MenuItem>
            {
                new MenuItem(EnterResults, "Enter match results"),
                new MenuItem(PrintTournamentGrid, "Show Tournament Grid"),
                new MenuItem(PrintResultOfMaches, "Show results of past matches")
            };
            var mnMenu2 = new Menu(sumMenu, "Championship");
            mnMenu2.Start();
        }

        static void CreateGame()
        {
            _championship.PlayerPlacement(_championship.Players);
        }

        static void EnterResults()
        {
            if (_championship.IndexRound >= _championship.Grid.Tournament.Count)
            {
                Console.WriteLine("All matches are over.");
                Thread.Sleep(1000);
                return;
            }

            Draftsman.PaintTournamentStage(_championship.Grid.Tournament[_championship.IndexRound]);

            _championship.CollectorResults();

            Console.Clear();
            Draftsman.PaintTournamentRound(_championship.Grid.Tournament[_championship.IndexRound - 1]);

            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
        }

        static void PrintTournamentGrid()
        {
            Draftsman.PaintGraf(_championship);
            Console.WriteLine();
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
        }

        static void PrintResultOfMaches()
        {
            if (_championship.IndexRound == 0)
            {
                Console.WriteLine("No data on matches.");
                Thread.Sleep(1000);
                return;
            }
            for (int i = 0; i <= _championship.IndexRound - 1; i++)
            {
                Draftsman.PaintTournamentRound(_championship.Grid.Tournament[i]);
            }
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
        }
    }
}
