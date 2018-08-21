using System.Collections.Generic;

namespace TournamentLibrary
{
    public class DataInput
    {
        private IDataManager _printer;

        public DataInput(IDataManager printer)
        {
            _printer = printer;
        }

        public int GetCountOfPlayers()
        {
            _printer.StartedNewTournament();
            _printer.EnterCountOfPlayers();

            var countOfPlayers = _printer.GetCountOfPlayers();

            return countOfPlayers;
        }

        public List<Player> GetPlayersList(int countOfPlayers)
        {
            _printer.EnterPlayerNames();

            var players = new List<Player>();
            var existingNames = new HashSet<string>();

            for (int i = 1; i <= countOfPlayers; i++)
            {
                _printer.EnterPlayerName(i);
                var name = _printer.GetPlayerName();

                while (existingNames.Contains(name) || name == " ")
                {
                    _printer.NameAlreadyExists();
                    name = _printer.GetPlayerName();
                }

                existingNames.Add(name);
                players.Add(new Player(name));
            }

            return players;
        }
    }
}
