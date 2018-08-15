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
        private static readonly List<string> _players = new List<string>();

        static void Main()
        {
            _championship = FileManager.DownloadTournamentFromFile();

            var mainMenu = new List<MenuItem>
            {
                new MenuItem(Start, "Start"),
                new MenuItem(MenuAddPlayers, "Add players"),
                new MenuItem(ResetTournament, "Reset tournament"),
            };

            var mnMenu = new Menu(mainMenu, "Championship");
            mnMenu.Start();
        }

        private static void Start()
        {
            if (_championship == null)
            {
                if (_players.Count == 0)
                {
                    Console.WriteLine("At first add players, please.");
                    Thread.Sleep(1000);
                    return;
                }

                _championship = new Tournament(_players);
                FileManager.WriteTournamentInFile(_championship);
            }

            var tournamentMenuList = new List<MenuItem>
            {
                new MenuItem(EnterResults, "Enter match results"),
                new MenuItem(MenuGraphPrint, "Show Tournament Grid"),
            };
            var tournamentMenu = new Menu(tournamentMenuList, "Championship");
            tournamentMenu.Start();
        }

        static void EnterResults()
        {
            if (_championship.IndexOfRound >= _championship.TournamentRounds.Count)
            {
                Console.WriteLine("All matches are over.");
                Thread.Sleep(1000);
                return;
            }

            var resultsOfRound = new List<int[]>();

            foreach (var meeting in _championship.TournamentRounds[_championship.IndexOfRound].Meetings)
            {
                resultsOfRound.Add(UserInteractor.GetResultOfMatch(meeting));
            }

            _championship.CollectorResults(resultsOfRound);
            FileManager.WriteTournamentInFile(_championship);
        }

        static void MenuGraphPrint()
        {
            var menuGraph = new List<MenuItem>
            {
                new MenuItem(PaintVerticalGraph, "Show vertical version"),
                new MenuItem(PaintHorisontalGraph, "Show horisontal version"),
            };
            var graphMenu = new Menu(menuGraph, "Championship");
            graphMenu.Start();
        }

        static void PaintVerticalGraph()
        {
            Console.ForegroundColor = ConsoleColor.White;
            var vertical = new VerticalGraphPainter();
            vertical.PaintGraph(_championship);
            Console.ReadKey();
        }

        static void PaintHorisontalGraph()
        {
            Console.ForegroundColor = ConsoleColor.White;
            var horisontal = new HorisontalGraphPainter();
            horisontal.PaintGraph(_championship);
            Console.ReadKey();
        }


        private static void MenuAddPlayers()
        {
            var addPlayerMenuList = new List<MenuItem>
            {
                new MenuItem(AddPlayer, "Add player")
            };

            var addPlayerMenu = new Menu(addPlayerMenuList, "Championship");
            addPlayerMenu.Start();
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
            _players.Add(name);
        }

        private static void ResetTournament()
        {
            _championship = null;
            FileManager.RemoveFile();
        }
    }
}
