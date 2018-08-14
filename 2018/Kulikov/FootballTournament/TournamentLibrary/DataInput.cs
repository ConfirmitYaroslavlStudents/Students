using System.Collections.Generic;

namespace TournamentLibrary
{
    public static class DataInput
    {
        private static IViewer _viewer = Viewer.GetViewer();

        public static int GetCountOfPlayers()
        {
            _viewer.StartedNewTournament(); 

            var countOfPlayers = _viewer.EnterCountOfPlayers();

            return countOfPlayers;
        }

        public static List<Player> GetPlayersList(int countOfPlayers)
        {
            _viewer.EnterPlayerNames();

            var players = new List<Player>();
            var existingNames = new HashSet<string>();

            for (int i = 1; i <= countOfPlayers; i++)
            {
                var name = _viewer.EnterPlayerName(i);

                while (existingNames.Contains(name) || name == " ")
                {
                    _viewer.NameAlreadyExists();
                    name = _viewer.EnterPlayerName(i);
                }

                existingNames.Add(name);
                players.Add(new Player(name));
            }

            return players;
        }
    }
}
