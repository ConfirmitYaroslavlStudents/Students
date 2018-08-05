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

                var tournamentMenuList = new List<MenuItem>
                {
                    new MenuItem(CreateSingeEliminationTournament, "SingleElimination"),
                    new MenuItem(CreateDoubleEliminationTournament, "DoubleElimination"),
                };
                var tournamentMenu = new Menu(tournamentMenuList, "Championship");
                tournamentMenu.Start();
            }
            else
            {
                MenuTournament();
            }
        }

        private static void CreateDoubleEliminationTournament()
        {
            _championship = new DoubleEliminationTournament(_players);
            FileManager.WriteTournamentInFile(_championship);
            MenuTournament();
        }

        private static void CreateSingeEliminationTournament()
        {
            _championship = new SingleElimitationTournament(_players);
            FileManager.WriteTournamentInFile(_championship);
            MenuTournament();
        }

        static void MenuTournament()
        {
            var tournamentMenuList = new List<MenuItem>
            {
                new MenuItem(EnterResults, "Enter match results"),
                new MenuItem(MenuGraphPrint, "Show Tournament Grid"),
            };
            var tournamentMenu = new Menu(tournamentMenuList, "Championship");
            tournamentMenu.Start();
        }

        private static void MenuGraphPrint()
        {
            var menuGraph = new List<MenuItem>
            {
                new MenuItem(PaintVerticalGraph, "Show vertical version"),
                new MenuItem(PaintHorisontalGraph, "Show horisontal version"),
                new MenuItem(PaintOnTwoSideGraph, "Show on two side version")
            };
            var graphMenu = new Menu(menuGraph, "Championship");
            graphMenu.Start();
        }

        private static void PaintVerticalGraph()
        {
            Console.ForegroundColor = ConsoleColor.White;
            var vertical = new VerticalGraphPainter();
            vertical.PaintGraph(Program._championship);
            Console.ReadKey();
        }

        private static void PaintHorisontalGraph()
        {
            Console.ForegroundColor = ConsoleColor.White;
            var horisontal = new HorisontalGraphPainter();
            horisontal.PaintGraph(_championship);
            Console.ReadKey();
        }

        private static void PaintOnTwoSideGraph()
        {
            Console.ForegroundColor = ConsoleColor.White;
            var onTwoSide = new OnTwoSidesGraphPainter();
            onTwoSide.PaintGraph(_championship);
            Console.ReadKey();
        }

        static void EnterResults()
        {
            var tournament = _championship.GetTournamentToPrint();

            if (_championship.GetIndexOfRound() >= tournament.Count)
            {
                Console.WriteLine("All matches are over.");
                Thread.Sleep(1000);
                return;
            }
            //Косяк с финальным раундом в дабле
            var meeting = tournament[_championship.GetIndexOfRound()].Meetings[_championship.GetIndexOfMatch()];
            _championship.CollectorResults(UserInteractor.GetResultOfMatch(meeting));

            FileManager.WriteTournamentInFile(_championship);
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
