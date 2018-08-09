using System;
using System.Collections.Generic;

namespace FootballTournament
{
    public static class DataInput
    {
        public static int GetCountOfPlayers()
        {
            ConsoleWorker.StartedNewTournament(); 

            var countOfPlayers = ConsoleWorker.EnterCountOfPlayers();

            return countOfPlayers;
        }

        public static List<Player> GetPlayersList(int countOfPlayers)
        {
            ConsoleWorker.EnterPlayerNames();

            var players = new List<Player>();
            var existingNames = new HashSet<string>();

            for (int i = 1; i <= countOfPlayers; i++)
            {
                var name = ConsoleWorker.EnterPlayerName(existingNames, i);
                existingNames.Add(name);
                players.Add(new Player(name));
            }

            return players;
        }
    }
}
