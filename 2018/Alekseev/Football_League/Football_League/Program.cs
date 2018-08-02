using System;
using System.Collections.Generic;

namespace Football_League
{
    class Program
    {
        static List<Contestant> _currentPlayers = new List<Contestant>();
        static List<Match> _currentMatches;
        static Scoreboard _scoreboard = new Scoreboard();
        static void Main()
        {
            while (true)
            { 
                PrintMenu();
                bool successfulOperation = false;
                while (!successfulOperation)
                    successfulOperation = RunChoice() != 0;
            }


        }
        static public void PrintMenu()
        {
            Console.WriteLine("Welcome to Football League scoreboard Simulator 1.0\nChoose an option\n1.Create new players league" +
                "\n2.Choose winners\n3.Display scoreboard\n4.Exit");
        }
        static public int RunChoice()
        {
            var choice = Console.ReadLine();
            Console.Clear();
            switch (choice)
            {
                case "1":
                    {
                        CreateNewLeague();
                        break;
                    }
                case "2":
                {
                        if(!CheckPlayerNumber())
                            break;
                        ChooseWinners();
                        break;
                    }
                case "3":
                    {
                        _scoreboard.Print();
                        break;
                    }
                case "4":
                    {
                        Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        Console.Clear();
                        Console.WriteLine("Please, press the appropriate button according to the menu!");
                        PrintMenu();
                        return 0;
                    }
            }
            return 1;
        }

        public static void CreateNewLeague()
        {
            _scoreboard = new Scoreboard();
            List<Contestant> newPlayers = new List<Contestant>();

            Console.WriteLine("Type number of players: ");
            var count = int.Parse(Console.ReadLine());
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("Type new contestant's name");
                var newPlayer = new Contestant(Console.ReadLine(),_scoreboard.currentPlayerPosition);
                _scoreboard.currentPlayerPosition += newPlayer.Name.Length + 1;
                newPlayers.Add(newPlayer);

            }
            EndRound(newPlayers);            

        }

        public static void UpdateCurrentPlayers(List<Contestant> players)
        {
            _currentPlayers = new List<Contestant>();

            foreach (var player in players)
            {
                _currentPlayers.Add(player);
            }
        }

        public static void UpdateCurrentMatches()
        {
            _currentMatches = new List<Match>();
            int countMatches = _currentPlayers.Count / 2;

            for (int i = 0; i < countMatches; i++)
            {
                _currentMatches.Add(new Match(_currentPlayers[i * 2],_currentPlayers[i * 2 + 1]));
            }
        }

        public static void ChooseWinners()
        {
            for (int i = 0; i < _currentMatches.Count; i++)
            {
                Console.WriteLine("Type number of winner for the match:");
                _currentMatches[i].PrintPlayers();

                bool firstPlayerChosen = Console.ReadLine() == "1";
                if(firstPlayerChosen)
                    _currentMatches[i].SetWinner(1);
                else _currentMatches[i].SetWinner(2);
            }

            List<Contestant> winners = new List<Contestant>();

            for (int i = 0; i < _currentMatches.Count; i++)
                winners.Add(_currentMatches[i].winner);
            if (_currentPlayers.Count % 2 != 0)
                winners.Add(_currentPlayers[_currentPlayers.Count - 1]);
            
            EndRound(winners);
        }
        public static void EndRound(List<Contestant> players)
        { 
            Console.Clear();
            _scoreboard.AddResults(players);
            UpdateCurrentPlayers(players);
            UpdateCurrentMatches();
        }

        public static bool CheckPlayerNumber()
        {
            if (_currentPlayers.Count == 1)
            {
                Console.WriteLine("Competition's over! Either start a new league or watch the scoreboard!\n" +
                                  "Or you may exit the program.\nTo return to menu press Enter...");
                while (Console.ReadKey().Key != ConsoleKey.Enter)
                Console.Clear();
                return false;
            }
            if (_currentPlayers.Count == 0)
            {
                Console.WriteLine("No players yet! Start a league first!\nTo return to menu press Enter...");
                while (Console.ReadKey().Key != ConsoleKey.Enter)
                Console.Clear();
                return false;
            }
            return true;
        }
    }
}
