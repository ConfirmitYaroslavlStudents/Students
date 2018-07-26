using System;
using System.Collections.Generic;

namespace FootballTournament
{
    public static class DataInput
    {
        public static string ChooseTournamentMode()
        {
            Console.WriteLine("Choose tournament mode: ");
            Console.WriteLine("se - start new tournament with single elimination mode");
            Console.WriteLine("de - start new tournament with double elimination mode\n");

            var tournamentMode = "";
            while (tournamentMode != "SE" && tournamentMode != "DE")
            {
                switch (Console.ReadLine())
                {
                    case "se":
                        tournamentMode = "SE";
                        break;
                    case "de":
                        tournamentMode = "DE";
                        break;
                }
            }
            return tournamentMode;
        }

        public static int GetCountOfPlayers()
        {
            Console.WriteLine("\nNew tournament started!");
            Console.Write("\nEnter count of players: ");
            var countOfPlayers = int.Parse(Console.ReadLine());
            return countOfPlayers;
        }

        public static List<Player> GetPlayersList(int countOfPlayers)
        {
            Console.WriteLine("\nEnter names of players: ");

            var players = new List<Player>();
            var names = new HashSet<string>();
            for (int i = 1; i <= countOfPlayers; i++)
            {
                Console.Write("{0}. ", i);
                var name = Console.ReadLine();
                while (names.Contains(name))
                {
                    Console.WriteLine("Player with this name already exists. Try again: ");
                    Console.Write("{0}. ", i);
                    name = Console.ReadLine();
                }
                names.Add(name);
                players.Add(new Player(name));
            }
            return players;
        }
    }
}
