using System.Collections.Generic;

namespace TournamentLibrary
{
    public class DataInput
    {
        private IPrinter _printer;

        public DataInput(IPrinter printer)
        {
            _printer = printer;
        }

        public int GetCountOfPlayers()
        {
            _printer.StartedNewTournament(); 

            var countOfPlayers = _printer.EnterCountOfPlayers();

            return countOfPlayers;
        }

        public List<Player> GetPlayersList(int countOfPlayers)
        {
            _printer.EnterPlayerNames();

            var players = new List<Player>();
            var existingNames = new HashSet<string>();

            for (int i = 1; i <= countOfPlayers; i++)
            {
                var name = _printer.EnterPlayerName(i);

                while (existingNames.Contains(name) || name == " ")
                {
                    _printer.NameAlreadyExists();
                    name = _printer.EnterPlayerName(i);
                }

                existingNames.Add(name);
                players.Add(new Player(name));
            }

            return players;
        }
    }
}
